using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public static Player playerInstance;
    private List<Transform> segments = new List<Transform>();
    public Transform segmentPrefab;
    public Vector2 direction = Vector2.right;
    private Vector2 input;
    public int initialSize = 4;
    public GameManager gameManager;
    
    public Transform checkPoint;
    public Transform checkPoint2;
    public static Vector2 lastCheckPoint;
    public BoxCollider2D col;
    bool tempActive;

    

    private void Start()
    {
        ResetState();
        for (int i = 0; i < initialSize - 1; i++)
        {
            Grow();
        }
        
    }

    private void Awake()
    {
       playerInstance = this;
       col = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        // Only allow turning up or down while moving in the x-axis
        if (direction.x != 0f)
        {
            if (SwipeManager.swipeUp)
            {
                input = Vector2.up;
            }
            else if (SwipeManager.swipeDown)
            {
                input = Vector2.down;
            }
        }
        // Only allow turning left or right while moving in the y-axis
        else if (direction.y != 0f)
        {
            if (SwipeManager.swipeRight)
            {
                input = Vector2.right;
            }
            else if (SwipeManager.swipeLeft)
            {
                input = Vector2.left;
            }
        }

        
    }

    private void FixedUpdate()
    {
        
        // Set the new direction based on the input
        if (input != Vector2.zero)
        {
            direction = input;
        }

        // Set each segment's position to be the same as the one it follows. We
        // must do this in reverse order so the position is set to the previous
        // position, otherwise they will all be stacked on top of each other.
        for (int i = segments.Count - 1; i > 0; i--)
        {
            segments[i].position = segments[i - 1].position;
        }

        // Move the snake in the direction it is facing
        // Round the values to ensure it aligns to the grid
        float x = Mathf.Round(transform.position.x) + direction.x;
        float y = Mathf.Round(transform.position.y) + direction.y;

        transform.position = new Vector2(x, y);
    }

    void DestroyAllSegments()
    {
        foreach (Transform segment in segments)
        {
            segment.gameObject.GetComponent<SpriteRenderer>().enabled = false;
        }

    }

    public void AppearAllSegments()
    {
        foreach (Transform segment in segments)
        {
            segment.gameObject.GetComponent<SpriteRenderer>().enabled = true;
        }
    }

        
    public void Grow()
    {
        Transform segment = Instantiate(segmentPrefab);
        segment.position = segments[segments.Count - 1].position;
        segments.Add(segment);
        
    }

    public void ResetState()
    {
        direction = Vector2.right;
        transform.position = Vector3.zero;

        // Start at 1 to skip destroying the head
        for (int i = 1; i < segments.Count; i++)
        {
            Destroy(segments[i].gameObject);
        }

        // Clear the list but add back this as the head
        segments.Clear();
        segments.Add(transform);

        // -1 since the head is already in the list
       
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Food"))
        {
            Grow();
            AudioManager.instace.PlaySFX(0);
            
        }
        else if (other.gameObject.CompareTag("Obstacle"))
        {
            //ResetState();
            Score.lives--;
            PlayerPrefs.SetInt("Lives", Score.lives);
            AudioManager.instace.PlaySFX(1);
            DestroyAllSegments();
            //Player.playerInstance.enabled = false;
            col.enabled = false;
            
            if (Score.lives>0)
            {
                RestartAgain();
                //GameManager.instance.RespawnPlayer();
            }
            else
            {
                GameManager.instance.RewardVideoforLives();
            }
            //gameManager.GameOver();
            //GoogleAdMobController.adsManager.ShowInterstitialAd();


        }
    }

    public void RestartAgain()
    {
        StartCoroutine(ReSpawanTime(2f));
    }

    IEnumerator ReSpawanTime(float time)
    {
        yield return new WaitForSeconds(time);
        this.transform.position = lastCheckPoint;
        //Player.playerInstance.enabled = true;
        col.enabled = true;
        AppearAllSegments();


    }
    
        
}//end

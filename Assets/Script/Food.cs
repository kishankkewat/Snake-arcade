using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{
    public static Food FoodInstance;
    public Collider2D gridArea;

    private void Start()
    {
       RandomizePosition();
    }

    private void Awake()
    {
        FoodInstance = this;
    }
    public void RandomizePosition()
    {
        Bounds bounds = gridArea.bounds;

        // Pick a random position inside the bounds
        float x = Random.Range(bounds.min.x, bounds.max.x);
        float y = Random.Range(bounds.min.y, bounds.max.y);

        // Round the values to ensure it aligns with the grid
        x = Mathf.Round(x);
        y = Mathf.Round(y);

        transform.position = new Vector2(x, y);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        RandomizePosition();
        GameManager.score++;
        if (GameManager.score > PlayerPrefs.GetInt("HighScore"))
        {
            PlayerPrefs.SetInt("HighScore", GameManager.score);
           // GameManager.instance.highScoreText.text = ("HighScore", GameManager.score).ToString();
        }
    }

    
}

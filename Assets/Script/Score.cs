using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{

    public static Score Instance;

    public static int lives =1;
    public static int diamonds=1;
    public bool reset;

    private void Awake()
    {
        lives = PlayerPrefs.GetInt("Lives", 0);
        diamonds = PlayerPrefs.GetInt("Diamonds", 0);
        
    }

    private void Start()
    {
        if (Instance != null)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }

        if (reset)
        {
            PlayerPrefs.SetInt("Lives", 1);
            PlayerPrefs.SetInt("Diamonds", 0);
        }

       
    }

    private void Update()
    {
        if (PlayerPrefs.GetInt("Lives", 0) <= 0)
        {
            PlayerPrefs.SetInt("Lives", 1);
        }

        if (PlayerPrefs.GetInt("Diamonds", 0) <= 0||diamonds<=0)
        {
            PlayerPrefs.SetInt("Diamonds", 0);
            diamonds = 0;
        }
    }

    public void ShowRewardedDiamonds()
    {
        diamonds += 1;
        PlayerPrefs.SetInt("Diamonds", +diamonds);
    }

    public void ShowRewardedLives(int Lives)
    {
        Time.timeScale = 1;
        Lives += lives;
        Player.playerInstance.enabled = true;
        Player.playerInstance.transform.position = Player.lastCheckPoint;
        PlayerPrefs.SetInt("Lives", +lives);
        GameManager.instance.DeActivateRewardvideoPannel();
    }

    public void GiveLivesUsingDiamonds()
    {
        diamonds--;
        if (diamonds>=0)
        {
            Time.timeScale = 1;
            Player.playerInstance.RestartAgain();
             lives++;
            PlayerPrefs.SetInt("Lives", +lives);
           // Player.playerInstance.transform.position = Player.lastCheckPoint;
          //  GameManager.instance.DeActivateRewardvideoPannel(); 
            // GameManager.instance.RewardForNewLives = false;
        }
        else
        {
            Debug.Log("you dont have enough diamonds");
            GameManager.instance.checkDiamondsPannel.SetActive(true);
        }
    }

    public void Purchased10Diamonds()
    {
        diamonds += 10;
        PlayerPrefs.SetInt("Diamonds", +diamonds);
    }

    public void Purchase50Hearts()
    {
        lives += 50;
        PlayerPrefs.SetInt("Lives", +lives);
    }

    public void Purchase5Hearts()
    {
        lives += 5;
        PlayerPrefs.SetInt("Lives", +lives);
    }

}

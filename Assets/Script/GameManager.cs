using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Networking;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameObject gameOverPanel;
    public GameObject player;
    
    public GameObject pauseButton;
    public GameObject pauseMenu;
    [SerializeField] GameObject InAppPurchasePannel;
    public Text livesText;
    public Text scoreText,lastScoreText;
    public Text highScoreText;
    public static int score;
    //public static int lives=1;
    public GameObject rewardVideoPannel;

    [Header("CHeck Internet Connection")]
    public GameObject checkConnectionPannel;
    public GameObject checkDiamondsPannel;
    public GameObject cutBtn;
    public Text diamondsText;

    public static Vector2 lastCheckPoint;

    private void Start()
    {
        gameOverPanel.SetActive(false);
        player.SetActive(true);
        pauseMenu.SetActive(false);
        pauseButton.SetActive(true);
        Time.timeScale = 1;
        rewardVideoPannel.SetActive(false);
        checkConnectionPannel.SetActive(false);
        checkDiamondsPannel.SetActive(false);
        //RewardForNewLives = false;
    }

    private void Awake()
    {
        instance = this;
        score = 0;

    }

    private void Update()
    {
        highScoreText.text = "HighScore : " + PlayerPrefs.GetInt("HighScore", 0);
        livesText.text = Score.lives.ToString();
        scoreText.text = score.ToString();
        lastScoreText.text = "Score : " + score;
        diamondsText.text = Score.diamonds.ToString();
 
    }

    public void RewardVideoPlay()
    {
        StartCoroutine(CheckConnection());
    }


    IEnumerator CheckConnection()
    {
        UnityWebRequest request = new UnityWebRequest("https://www.google.com");
        yield return request.SendWebRequest();

        if (request.error == null)
        {
          // GameObject.Find("AdManagerForLives").GetComponent<GoogleAdMobController>().ShowRewardedAd();
           //RewardForNewLives = false;
        }
        else
        {
            checkConnectionPannel.SetActive(true);
            rewardVideoPannel.SetActive(false);
        }
    }

    public void DeActivateCheckConnectionPannel()
    {
        checkConnectionPannel.SetActive(false);
        rewardVideoPannel.SetActive(true);
        //cutBtn.SetActive(false);
    }    

    public void DeActivateCheckDiamondsPannel()
    {
        checkDiamondsPannel.SetActive(false);
        rewardVideoPannel.SetActive(true);
    }

    public void GameOver()
    {
        //adManager.RequestAndLoadInterstitialAd();
        gameOverPanel.SetActive(true);
        player.SetActive(false);
        pauseMenu.SetActive(false);
        pauseButton.SetActive(false);

    }

    public void ActivateIn_App_Purchase()
    {
        InAppPurchasePannel.gameObject.SetActive(true);
        
    }

    public void DeActiveIn_App_Purchase()
    {
        InAppPurchasePannel.gameObject.SetActive(false);
        
    }

    public void RewardVideoforLives()
    {
        //pannel for reward video
        rewardVideoPannel.SetActive(true);
        Player.playerInstance.enabled = false;
        //Player.playerInstance.col.enabled = true;
        //RewardForNewLives = true;
        
    }

    public void DeActivateRewardvideoPannel()
    {
        rewardVideoPannel.SetActive(false);
        Player.playerInstance.enabled = true;
        
        
    }

    public void Pause()
    {
        Time.timeScale = 0;
        pauseButton.SetActive(false);
        pauseMenu.SetActive(true);
    }
   
    public void Resume()
    {
        Time.timeScale = 1;
        pauseButton.SetActive(true);
        pauseMenu.SetActive(false);

    }

    public void Quit()
    {
        Application.Quit();
    }

    public void MainMenu()
    {
        {
            AudioManager.instace.bg.GetComponent<AudioSource>().volume = 0.75f;
            SceneManager.LoadScene("MainMenu");
        }
    }

    public void Restart()
    {
        SceneManager.LoadScene("Level-1");
    }

    public void RespawnPlayer()
    {
        StartCoroutine(PlayerRespawn());
    }

    IEnumerator PlayerRespawn()
    {
        yield return new WaitForSeconds(2f);
        Player.playerInstance.transform.position = Player.lastCheckPoint;
        Player.playerInstance.col.enabled = true;
        Player.playerInstance.AppearAllSegments();
    }

}//end
    

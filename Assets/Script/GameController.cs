using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Networking;

public class GameController : MonoBehaviour
{
    [SerializeField] Text connectionErrorText;
    [SerializeField] Text loadingText;
    [SerializeField] Button playButton;
    [SerializeField] Button tryAgainButton;
    public GameObject shopBtn;
    public GameObject shopPannel;

    [SerializeField] Text livesText;
    [SerializeField] Text diamondsText;
    

    private void Start()
    {
         StartCoroutine(CheckInternetConnection());
        // GameObject.Find("AdManagerForLives").GetComponent<GoogleAdMobController>().RequestBannerAd();
        shopPannel.SetActive(false);
    }

    private void Update()
    {
        livesText.text = Score.lives.ToString();
        diamondsText.text = Score.diamonds.ToString();
    }

    public void Reload()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void StrartScene()
    {
        AudioManager.instace.bg.GetComponent<AudioSource>().volume = 0.2f;
        SceneManager.LoadScene("Level-1");
    }

    public  void Quit()
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

    public void ActivateShopPannel()
    {
        shopPannel.SetActive(true);
    }
    public void DeActivateShopPannel()
    {
        shopPannel.SetActive(false);
    }

    IEnumerator CheckInternetConnection()
    {
        UnityWebRequest request = new UnityWebRequest("https://www.google.com");
        yield return request.SendWebRequest();

        if (request.error!=null)
        {
            loadingText.gameObject.SetActive(false);
            connectionErrorText.gameObject.SetActive(true);
            tryAgainButton.gameObject.SetActive(true);
        }
        else
        {
            loadingText.gameObject.SetActive(false);
            playButton.gameObject.SetActive(true);
            
        }
    }

    public void TryAgain()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}

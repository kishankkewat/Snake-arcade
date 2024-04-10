using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountdownController : MonoBehaviour
{
    public int countdownTime;
    public Text countdownDisplay;

    private void Start()
    {
        StartCoroutine(CountdownToStart());
        Player.playerInstance.enabled = false;
        Player.playerInstance.transform.localScale = new Vector3(0, 0, 0);
        Food.FoodInstance.transform.localScale = new Vector3(0, 0, 0);
    }

    IEnumerator CountdownToStart()
    {
        while(countdownTime > 0)
        {
            countdownDisplay.text = countdownTime.ToString();

            yield return new WaitForSeconds(1f);

            countdownTime--;
        }
        
        countdownDisplay.text = "GO!";
		
		/* Call the code to "begin" your game here.
		 * For example, mine allows the player to start
		 * moving and starts the in game timer.
         */
		// GameController.instance.BeginGame();

        yield return new WaitForSeconds(1f);
        Player.playerInstance.enabled = true;
        Player.playerInstance.transform.localScale = new Vector3(1, 1, 1);
        Food.FoodInstance.transform.localScale = new Vector3(1, 1, 1);
        countdownDisplay.gameObject.SetActive(false);
    }
}

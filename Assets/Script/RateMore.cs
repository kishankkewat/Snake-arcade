using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RateMore : MonoBehaviour
{
	public void Rate()
	{
		// "market" works for android  (iOS: put your app link
		Application.OpenURL("market://details?id=com.ROSNI.SnakeArcade");
	}

	public void More()
	{
		// Android  ,(iOS: put you app store link)
		Application.OpenURL("https://play.google.com/store/apps/dev?id=6020600436757655983");
	}

	public void Feedback()
	{
		Application.OpenURL("mailto:rosnitechnologies@gmail.com");
	}
}

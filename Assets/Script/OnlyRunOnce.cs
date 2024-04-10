using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnlyRunOnce : MonoBehaviour
{
    private void Awake()
    {
        if (!PlayerPrefs.HasKey("FirstTime") || PlayerPrefs.GetInt("FirstTime") != 2)
        {
            PlayerPrefs.SetInt("FirstTime", 2);
            PlayerPrefs.SetInt("Lives", 3);
        }
        
    }
}

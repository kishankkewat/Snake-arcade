using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instace;
    public AudioSource[] audios;
    public AudioSource bg;
    

    private void Start()
    {
        if (instace!=null)
        {
            DestroyImmediate(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
            instace = this;
        }

        bg.volume = 0.75f;
    }

    
    public void PlaySFX(int playSFX)
    {
        audios[playSFX].Play();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public AudioSource audioSource;
    public bool resetSaves;
    public int skinIndex;

    bool playingMusic;

    // Start is called before the first frame update
    void Start()
    {
        if (GameObject.FindGameObjectsWithTag("GameManager").Length > 1)
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(audioSource);
        DontDestroyOnLoad(this.gameObject);
        
        if (!playingMusic)
        {
            PlayMusic();
            playingMusic = true;
        }
    }

    void Update()
    {
        if (resetSaves)
        {
            ResetSaveDatas();
        }

    }

    void ResetSaveDatas()
    {
        PlayerPrefs.SetInt("Finished", 0);
        PlayerPrefs.SetInt("Completed", 0);
        PlayerPrefs.SetInt("ViewedControls", 0);

        PlayerPrefs.SetInt("10", 0);
        PlayerPrefs.SetInt("11", 0);
        PlayerPrefs.SetInt("12", 0);
        PlayerPrefs.SetInt("13", 0);

        PlayerPrefs.SetInt("20", 0);
        PlayerPrefs.SetInt("21", 0);
        PlayerPrefs.SetInt("22", 0);
        PlayerPrefs.SetInt("23", 0);

        PlayerPrefs.SetInt("30", 0);
        PlayerPrefs.SetInt("31", 0);
        PlayerPrefs.SetInt("32", 0);
        PlayerPrefs.SetInt("33", 0);

        PlayerPrefs.SetInt("40", 0);
        PlayerPrefs.SetInt("41", 0);
        PlayerPrefs.SetInt("42", 0);
        PlayerPrefs.SetInt("43", 0);

        PlayerPrefs.SetInt("50", 0);
        PlayerPrefs.SetInt("51", 0);
        PlayerPrefs.SetInt("52", 0);
        PlayerPrefs.SetInt("53", 0);
    }

    public void PlayMusic()
    {
        if (!audioSource.isPlaying)
        {
            audioSource.Play();
        }
    }

    void OnApplicationQuit()
    {
        //Reset Value
        PlayerPrefs.SetInt("MenuIndex", 0);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelMenuUIPopulator : MonoBehaviour
{
    [Header("Level 1")]
    public GameObject UI10;
    public GameObject UI11;
    public GameObject UI12;
    public GameObject UI13;

    [Header("Level 2")]
    public GameObject UI20;
    public GameObject UI21;
    public GameObject UI22;
    public GameObject UI23;

    [Header("Level 3")]
    public GameObject UI30;
    public GameObject UI31;
    public GameObject UI32;
    public GameObject UI33;

    [Header("Level 4")]
    public GameObject UI40;
    public GameObject UI41;
    public GameObject UI42;
    public GameObject UI43;

    [Header("Level 5")]
    public GameObject UI50;
    public GameObject UI51;
    public GameObject UI52;
    public GameObject UI53;

    // Start is called before the first frame update
    void Start()
    {
        DisplayLevelStatuses();
    }
    
    void DisplayLevelStatuses()
    {
        //Level 1
        if (PlayerPrefs.GetInt("10", 0) == 1)
        {
            UI10.SetActive(true);
        }
        if (PlayerPrefs.GetInt("11", 0) == 1)
        {
            UI11.SetActive(true);
        }
        if (PlayerPrefs.GetInt("12", 0) == 1)
        {
            UI12.SetActive(true);
        }
        if (PlayerPrefs.GetInt("13", 0) == 1)
        {
            UI13.SetActive(true);
        }

        //Level 2
        if (PlayerPrefs.GetInt("20", 0) == 1)
        {
            UI20.SetActive(true);
        }
        if (PlayerPrefs.GetInt("21", 0) == 1)
        {
            UI21.SetActive(true);
        }
        if (PlayerPrefs.GetInt("22", 0) == 1)
        {
            UI22.SetActive(true);
        }
        if (PlayerPrefs.GetInt("23", 0) == 1)
        {
            UI23.SetActive(true);
        }

        //Level 3
        if (PlayerPrefs.GetInt("30", 0) == 1)
        {
            UI30.SetActive(true);
        }
        if (PlayerPrefs.GetInt("31", 0) == 1)
        {
            UI31.SetActive(true);
        }
        if (PlayerPrefs.GetInt("32", 0) == 1)
        {
            UI32.SetActive(true);
        }
        if (PlayerPrefs.GetInt("33", 0) == 1)
        {
            UI33.SetActive(true);
        }

        //Level 4
        if (PlayerPrefs.GetInt("40", 0) == 1)
        {
            UI40.SetActive(true);
        }
        if (PlayerPrefs.GetInt("41", 0) == 1)
        {
            UI41.SetActive(true);
        }
        if (PlayerPrefs.GetInt("42", 0) == 1)
        {
            UI42.SetActive(true);
        }
        if (PlayerPrefs.GetInt("43", 0) == 1)
        {
            UI43.SetActive(true);
        }

        //Level 5
        if (PlayerPrefs.GetInt("50", 0) == 1)
        {
            UI50.SetActive(true);
        }
        if (PlayerPrefs.GetInt("51", 0) == 1)
        {
            UI51.SetActive(true);
        }
        if (PlayerPrefs.GetInt("52", 0) == 1)
        {
            UI52.SetActive(true);
        }
        if (PlayerPrefs.GetInt("53", 0) == 1)
        {
            UI53.SetActive(true);
        }
    }
}

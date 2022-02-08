using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EndUIFiller : MonoBehaviour
{
    [Header("Save Data")]
    public string levelWinKey;
    public string coinSaveKey;
    public string iceCreamSaveKey;
    public string timeAttackSaveKey;

    [Header("UI")]
    public GameObject playGUI;
    public GameObject endGUI;
    public TextMeshProUGUI coinCount;
    public GameObject coinTick;
    public TextMeshProUGUI timeText;
    public GameObject timeTick;
    public GameObject coloredIceCream;
    public GameObject iceCreamTick;

    CoinPickup coinPickup;
    IceCreamPickUp iceCreamPickUp;
    LevelTimer levelTimer;
    LevelManager levelManager;

    // Start is called before the first frame update
    void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        coinPickup = player.GetComponent<CoinPickup>();
        iceCreamPickUp = player.GetComponent<IceCreamPickUp>();
        levelTimer = player.GetComponent<LevelTimer>();
        levelManager = GameObject.FindGameObjectWithTag("Level Manager").GetComponent<LevelManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void FillSaves()
    {
        //Save Stuff
        PlayerPrefs.SetInt(levelWinKey, 1);

        if (levelManager.currentCoinCount == levelManager.coinCountInLevel)
        {
            PlayerPrefs.SetInt(coinSaveKey, 1);
        }
        if (levelTimer.GetLevelTime() <= levelManager.timeAttackTime)
        {
            PlayerPrefs.SetInt(timeAttackSaveKey, 1);
        }
        if (levelManager.pickedUpIceCream)
        {
            PlayerPrefs.SetInt(iceCreamSaveKey, 1);
        }
    }

    public void OpenEndUI()
    {
        //UI Stuff
        playGUI.SetActive(false);
        endGUI.SetActive(true);
        coinCount.text = levelManager.currentCoinCount + "/" + levelManager.coinCountInLevel;
        if (levelManager.currentCoinCount == levelManager.coinCountInLevel)
        {
            coinTick.SetActive(true);
        }

        timeText.text = levelTimer.GetLevelTime().ToString("F1") + "/" + levelManager.timeAttackTime;
        if (levelTimer.GetLevelTime() <= levelManager.timeAttackTime)
        {
            timeTick.SetActive(true);
        }

        if (levelManager.pickedUpIceCream)
        {
            coloredIceCream.SetActive(true);
            iceCreamTick.SetActive(true);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CoinCounter : MonoBehaviour
{
    public TextMeshProUGUI textMesh;

    LevelManager levelManager;
    CoinPickup coinPickup;
    PlayerState playerState;

    // Start is called before the first frame update
    void Start()
    {
        levelManager = GameObject.FindGameObjectWithTag("Level Manager").GetComponent<LevelManager>();
        coinPickup = GameObject.FindGameObjectWithTag("Player").GetComponent<CoinPickup>();
        playerState = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerState>();

    }

    // Update is called once per frame
    void Update()
    {
        if (!playerState.win)
        {
            textMesh.text = levelManager.currentCoinCount + "/" + levelManager.coinCountInLevel;
        }
    }
}

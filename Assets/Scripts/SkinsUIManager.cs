using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinsUIManager : MonoBehaviour
{
    GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }

    public void UseSnowmanSkin()
    {
        gameManager.skinIndex = 0;
    }

    public void UseDuckSkin()
    {
        gameManager.skinIndex = 1;
    }

    public void UseCatSkin()
    {
        gameManager.skinIndex = 2;
    }
}

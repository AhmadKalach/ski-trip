using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinSelector : MonoBehaviour
{
    public GameObject snowManSkin;
    public GameObject duckSkin;
    public GameObject catSkin;

    // Start is called before the first frame update
    void Start()
    {
        GameManager gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();

        if (gameManager.skinIndex == 0)
        {
            snowManSkin.SetActive(true);
            duckSkin.SetActive(false);
            catSkin.SetActive(false);
        }
        else if (gameManager.skinIndex == 1)
        {
            snowManSkin.SetActive(false);
            duckSkin.SetActive(true);
            catSkin.SetActive(false);
        }
        else
        {
            snowManSkin.SetActive(false);
            duckSkin.SetActive(false);
            catSkin.SetActive(true);
        }
    }
}

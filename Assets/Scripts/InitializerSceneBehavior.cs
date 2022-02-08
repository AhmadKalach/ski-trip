using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitializerSceneBehavior : MonoBehaviour
{
    public UIManager sceneManager;

    // Start is called before the first frame update
    void Start()
    {
        //Reset Value
        PlayerPrefs.SetInt("MenuIndex", 0);
        sceneManager.LevelMenu();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

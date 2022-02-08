using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelTimer : MonoBehaviour
{
    public float startTime;

    LevelManager levelManager;

    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.time;
        levelManager = GameObject.FindGameObjectWithTag("Level Manager").GetComponent<LevelManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!levelManager.started)
        {
            startTime = Time.time;
        }
    }

    public float GetLevelTime()
    {
        return Time.time - startTime;
    }
}

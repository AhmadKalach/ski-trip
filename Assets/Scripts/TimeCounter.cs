using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimeCounter : MonoBehaviour
{
    public TextMeshProUGUI timeText;

    LevelTimer timer;

    // Start is called before the first frame update
    void Start()
    {
        timer = GameObject.FindGameObjectWithTag("Player").GetComponent<LevelTimer>();
    }

    // Update is called once per frame
    void Update()
    {
        timeText.text = "Time: " + timer.GetLevelTime().ToString("F1");
    }
}

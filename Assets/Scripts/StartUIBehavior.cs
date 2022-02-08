using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class StartUIBehavior : MonoBehaviour
{
    public RectTransform startMenu;
    public RectTransform skinsMenu;
    public RectTransform levelMenu;
    public RectTransform controlsMenu;
    public RectTransform finishedMenu;
    public RectTransform completeMenu;

    [Header("Configuration")]
    public float moveTime;
    public float xMove = 900;

    public RectTransform currMenu;
    int currIndex;

    // Start is called before the first frame update
    void Start()
    {
        currIndex = PlayerPrefs.GetInt("MenuIndex", 0);
        startMenu.anchoredPosition = new Vector2(xMove, startMenu.anchoredPosition.y);
        controlsMenu.anchoredPosition = new Vector2(xMove, skinsMenu.anchoredPosition.y);
        skinsMenu.anchoredPosition = new Vector2(xMove, skinsMenu.anchoredPosition.y);
        levelMenu.anchoredPosition = new Vector2(xMove, levelMenu.anchoredPosition.y);
        finishedMenu.anchoredPosition = new Vector2(xMove, levelMenu.anchoredPosition.y);
        completeMenu.anchoredPosition = new Vector2(xMove, levelMenu.anchoredPosition.y);

        GetCurrentMenu();
        currMenu.anchoredPosition = new Vector2(0, currMenu.anchoredPosition.y);
    }

    void GetCurrentMenu()
    {
        bool completeCondition = GetCompleteCondition() && !GetPlayerPrefsBoolValue("Completed", 0);
        bool finishCondition = GetFinishCondition() && !GetPlayerPrefsBoolValue("Finished", 0);
        if (completeCondition)
        {
            currMenu = completeMenu;
            PlayerPrefs.SetInt("Completed", 1);
            PlayerPrefs.SetInt("Finished", 1);
        }
        else if (finishCondition)
        {
            currMenu = finishedMenu;
            PlayerPrefs.SetInt("Finished", 1);
        }
        else if (currIndex == 0)
        {
            currMenu = startMenu;
        }
        else if (currIndex == 1)
        {
            currMenu = controlsMenu;
        }
        else if (currIndex == 2)
        {
            currMenu = skinsMenu;
        }
        else
        {
            currMenu = levelMenu;
        }
    }

    bool GetFinishCondition()
    {
        return GetPlayerPrefsBoolValue("10", 0) &&
            GetPlayerPrefsBoolValue("20", 0) &&
            GetPlayerPrefsBoolValue("30", 0) &&
            GetPlayerPrefsBoolValue("40", 0) &&
            GetPlayerPrefsBoolValue("50", 0);
    }

    bool GetCompleteCondition()
    {
        bool toReturn = GetPlayerPrefsBoolValue("10", 0) &&
        GetPlayerPrefsBoolValue("11", 0) &&
        GetPlayerPrefsBoolValue("12", 0) &&
        GetPlayerPrefsBoolValue("13", 0) &&
        GetPlayerPrefsBoolValue("20", 0) &&
        GetPlayerPrefsBoolValue("21", 0) &&
        GetPlayerPrefsBoolValue("22", 0) &&
        GetPlayerPrefsBoolValue("23", 0) &&
        GetPlayerPrefsBoolValue("30", 0) &&
        GetPlayerPrefsBoolValue("31", 0) &&
        GetPlayerPrefsBoolValue("32", 0) &&
        GetPlayerPrefsBoolValue("33", 0) &&
        GetPlayerPrefsBoolValue("40", 0) &&
        GetPlayerPrefsBoolValue("41", 0) &&
        GetPlayerPrefsBoolValue("42", 0) &&
        GetPlayerPrefsBoolValue("43", 0) &&
        GetPlayerPrefsBoolValue("50", 0) &&
        GetPlayerPrefsBoolValue("51", 0) &&
        GetPlayerPrefsBoolValue("52", 0) &&
        GetPlayerPrefsBoolValue("53", 0);
        return toReturn;
    }

    bool GetPlayerPrefsBoolValue(string s, int def)
    {
        return PlayerPrefs.GetInt(s, def) == 1;
    }

    public void GoToStart(int dir)
    {
        if (dir > 0)
        {
            currMenu.DOAnchorPosX(-xMove, moveTime);
            currMenu = startMenu;
            startMenu.anchoredPosition = new Vector2(xMove, currMenu.anchoredPosition.y);
            startMenu.DOAnchorPosX(0, moveTime);
            PlayerPrefs.SetInt("MenuIndex", 0);
        }
        else
        {
            currMenu.DOAnchorPosX(xMove, moveTime);
            currMenu = startMenu;
            startMenu.anchoredPosition = new Vector2(-xMove, currMenu.anchoredPosition.y);
            startMenu.DOAnchorPosX(0, moveTime);
            PlayerPrefs.SetInt("MenuIndex", 0);
        }
    }

    public void GoToControls(int dir)
    {
        PlayerPrefs.SetInt("ViewedControls", 1);
        if (dir > 0)
        {
            currMenu.DOAnchorPosX(-xMove, moveTime);
            currMenu = controlsMenu;
            controlsMenu.anchoredPosition = new Vector2(xMove, currMenu.anchoredPosition.y);
            controlsMenu.DOAnchorPosX(0, moveTime);
            PlayerPrefs.SetInt("MenuIndex", 1);
        }
        else
        {
            currMenu.DOAnchorPosX(xMove, moveTime);
            currMenu = skinsMenu;
            skinsMenu.anchoredPosition = new Vector2(-xMove, currMenu.anchoredPosition.y);
            skinsMenu.DOAnchorPosX(0, moveTime);
            PlayerPrefs.SetInt("MenuIndex", 1);
        }
    }

    public void GoToCharacterSelect(int dir)
    {
        if (PlayerPrefs.GetInt("ViewedControls", 0) == 0)
        {
            PlayerPrefs.SetInt("ViewedControls", 1);
            GoToControls(dir);
        }
        else if (dir > 0)
        {
            currMenu.DOAnchorPosX(-xMove, moveTime);
            currMenu = skinsMenu;
            skinsMenu.anchoredPosition = new Vector2(xMove, currMenu.anchoredPosition.y);
            skinsMenu.DOAnchorPosX(0, moveTime);
            PlayerPrefs.SetInt("MenuIndex", 2);
        }
        else
        {
            currMenu.DOAnchorPosX(xMove, moveTime);
            currMenu = skinsMenu;
            skinsMenu.anchoredPosition = new Vector2(-xMove, currMenu.anchoredPosition.y);
            skinsMenu.DOAnchorPosX(0, moveTime);
            PlayerPrefs.SetInt("MenuIndex", 2);
        }
    }

    public void GoToLevelSelect(int dir)
    {
        if (dir > 0)
        {
            currMenu.DOAnchorPosX(-xMove, moveTime);
            currMenu = levelMenu;
            levelMenu.anchoredPosition = new Vector2(xMove, currMenu.anchoredPosition.y);
            levelMenu.DOAnchorPosX(0, moveTime);
            PlayerPrefs.SetInt("MenuIndex", 3);
        }
        else
        {
            currMenu.DOAnchorPosX(xMove, moveTime);
            currMenu = levelMenu;
            levelMenu.anchoredPosition = new Vector2(-xMove, currMenu.anchoredPosition.y);
            levelMenu.DOAnchorPosX(0, moveTime);
            PlayerPrefs.SetInt("MenuIndex", 3);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class UIManager : MonoBehaviour
{
    public bool animateStart;
    public bool animateEnd;
    public RectTransform transitionAnimationRect;
    public float transitionTime;
    public float xMove;

    private void Start()
    {
        StartCoroutine(WaitThenClearScreenAndStart(0.25f));
    }

    IEnumerator WaitThenClearScreenAndStart(float time)
    {
        if (animateStart)
        {
            transitionAnimationRect.anchoredPosition = Vector2.zero;
        }

        yield return new WaitForSeconds(time);

        if (animateStart)
        {
            Sequence sequence = DOTween.Sequence();
            sequence.Append(transitionAnimationRect.DOAnchorPosX(-xMove, transitionTime));
            sequence.AppendCallback(() => GameObject.FindGameObjectWithTag("Level Manager").GetComponent<LevelManager>().started = true);
        }
    }

    public void RestartLevel()
    {
        CoverScreenAndEnd(SceneManager.GetActiveScene().buildIndex);
    }

    public void LevelOne()
    {
        CoverScreenAndEnd("Level 1");
    }

    public void LevelTwo()
    {
        CoverScreenAndEnd("Level 2");
    }

    public void LevelThree()
    {
        CoverScreenAndEnd("Level 3");
    }

    public void LevelFour()
    {
        CoverScreenAndEnd("Level 4");
    }

    public void LevelFive()
    {
        CoverScreenAndEnd("Level 5");
    }

    public void LevelMenu()
    {
        CoverScreenAndEnd("Level Menu");
    }

    void CoverScreenAndEnd(string sceneName)
    {
        if (animateEnd)
        {
            transitionAnimationRect.anchoredPosition = new Vector2(xMove, 0);
            Sequence sequence = DOTween.Sequence();
            sequence.Append(transitionAnimationRect.DOAnchorPosX(0, transitionTime));
            sequence.AppendCallback(() => SceneManager.LoadScene(sceneName));
        }
        else
        {
            SceneManager.LoadScene(sceneName);
        }
    }

    void CoverScreenAndEnd(int sceneIndex)
    {
        if (animateEnd)
        {
            transitionAnimationRect.anchoredPosition = new Vector2(xMove, 0);
            Sequence sequence = DOTween.Sequence();
            sequence.Append(transitionAnimationRect.DOAnchorPosX(0, transitionTime));
            sequence.AppendCallback(() => SceneManager.LoadScene(sceneIndex));
        }
        else
        {
            SceneManager.LoadScene(sceneIndex);
        }
    }
}

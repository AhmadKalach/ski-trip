using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EndUIAnimation : MonoBehaviour
{
    public float startY;
    public float animationTime;
    
    private void OnEnable()
    {
        RectTransform rectTransform = GetComponent<RectTransform>();
        rectTransform.anchoredPosition = new Vector2(0, 500);
        rectTransform.DOAnchorPosY(0, animationTime);
    }
}

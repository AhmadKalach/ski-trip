using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SquashAndStretch : MonoBehaviour
{
    public GameObject pivot;
    public Vector3 stretchStregth;
    public float stretchTime;
    public Vector3 squashStrength;
    public float squashTime;
    public Vector3 jumpSquashStrength;
    
    Movement playerMovement;
    Sequence sequence;

    // Start is called before the first frame update
    void Start()
    {
        playerMovement = GetComponent<Movement>();
    }

    public void Squash()
    {
        sequence.Kill();
        sequence = DOTween.Sequence();

        sequence.Append(pivot.transform.DOScale(squashStrength, squashTime / 2));
        sequence.Append(pivot.transform.DOScale(new Vector3(1, 1, 1), squashTime / 2));
    }

    public void JumpSquash()
    {
        sequence.Kill();
        sequence = DOTween.Sequence();
        sequence = sequence.Append(pivot.transform.DOScale(jumpSquashStrength, playerMovement.jumpChargeTime));
    }

    public void Stretch()
    {
        sequence.Kill();
        sequence = DOTween.Sequence();
        pivot.transform.DOScale(stretchStregth, stretchTime);
    }
}

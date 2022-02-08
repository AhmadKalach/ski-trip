using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MenuCameraShake : MonoBehaviour
{
    public float distance;
    public float speed;

    Vector3 startPos;
    bool moving;

    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(!moving)
        {
            moving = true;
            Vector3 newPos = startPos + new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0).normalized * distance;

            Sequence sequence = DOTween.Sequence();

            sequence.Append(transform.DOMove(newPos, Vector3.Distance(transform.position, newPos) / speed).SetEase(Ease.Linear));
            sequence.AppendCallback(() => moving = false);
        }
    }
}

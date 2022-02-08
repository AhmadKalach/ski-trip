using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class WindBehavior : MonoBehaviour
{
    public float effectorSpeed;
    public float effectorIncreaseTime;
    public float effectorDecreaseTime;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            IncreaseEffectorGradually(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            DecreaseEffectorGradually(other.gameObject);
        }
    }

    void IncreaseEffectorGradually(GameObject player)
    {
        DOTween.To(() => player.GetComponent<Movement>().effector, x => player.GetComponent<Movement>().effector = x, transform.forward * effectorSpeed, effectorIncreaseTime);
    }

    void DecreaseEffectorGradually(GameObject player)
    {
        DOTween.To(() => player.GetComponent<Movement>().effector, x => player.GetComponent<Movement>().effector = x, Vector3.zero, effectorDecreaseTime);
    }
}

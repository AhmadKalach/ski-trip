using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveUpAndDown : MonoBehaviour
{
    public float oscillationTime;
    public float oscillationDistance;

    float initialY;

    // Start is called before the first frame update
    void Start()
    {
        initialY = transform.position.y;    
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(transform.position.x, initialY + Mathf.Cos(Time.time * Mathf.PI / oscillationTime) * oscillationDistance, transform.position.z);
    }
}

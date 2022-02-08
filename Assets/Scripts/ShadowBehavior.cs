using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowBehavior : MonoBehaviour
{
    public GameObject player;
    public LayerMask groundLayer;
    public float raycastMaxDistance;

    Vector3 initialScale;

    // Start is called before the first frame update
    void Start()
    {
        initialScale = transform.localScale;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        RaycastHit hit;
        Physics.Raycast(player.transform.position, Vector3.down, out hit, raycastMaxDistance, groundLayer);
        if (hit.transform != null)
        {
            transform.localScale = initialScale;
            transform.position = new Vector3(transform.position.x, hit.point.y, transform.position.z);
            transform.up = hit.normal;
        }
        else
        {
            transform.localScale = Vector3.zero;
        }
    }
}

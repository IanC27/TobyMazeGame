using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public Transform target;

    
    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 loc = transform.position;
        loc.x = target.position.x;
        loc.y = target.position.y;
        transform.position = loc;
    }
}

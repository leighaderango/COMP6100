using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public float smoothSpeed = 0.125f;

    // Update is called once per frame
    void LateUpdate()
    {
        if(target != null) {
            Vector3 desiredPos = new Vector3(target.position.x, target.position.y, transform.position.z);
            Vector3 smoothedPos = Vector3.Lerp(new Vector3(transform.position.x, transform.position.y, transform.position.z), desiredPos, smoothSpeed);
            transform.position = new Vector3(smoothedPos.x, smoothedPos.y, smoothedPos.z);
        }
    }
}

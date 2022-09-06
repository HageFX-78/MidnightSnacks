using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform playerLocation;
    void FixedUpdate()
    {
        transform.position = new Vector3(playerLocation.position.x, playerLocation.position.y, transform.position.z);
    }
}

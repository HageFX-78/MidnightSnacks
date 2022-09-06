using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShake : MonoBehaviour
{
    [SerializeField]float shakeDuration = 0f;
    [SerializeField] float shakeMagnitude = 0.3f;
    [SerializeField] float dampingSpeed = 1.0f;
    public bool isShaking;
    public bool isFollowCam;
    Vector3 currentPos;

    void Start()
    {
        isShaking = false;
    }
    void OnEnable()
    {
        currentPos = transform.localPosition;
    }
    // Update is called once per frame
    void Update()
    {
        if (isFollowCam && !isShaking)
        {
            currentPos = transform.localPosition;
        }


        if (shakeDuration > 0)
        {
            isShaking = true;
            transform.localPosition = currentPos + Random.insideUnitSphere * shakeMagnitude;

            shakeDuration -= Time.deltaTime * dampingSpeed;
        }
        else
        {
            shakeDuration = 0f;
            isShaking = false;
            if(!isFollowCam)
            {
                transform.localPosition = currentPos;
            }
            
        }
    }
    public void ShakeScreen(float amt)
    {
        shakeDuration = amt;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TPEntityR1 : MonoBehaviour
{
    // Start is called before the first frame update
    public string thislocation;
    public Transform tolocation;
    public bool isLockedDoor;

    bool playerDetected;
    GameObject entityRef;
    private void Start()
    {
        playerDetected = false;
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Mouse0)&& playerDetected)
        {
            if(isLockedDoor)
            {
                if(entityRef.name!="Himeno")
                {
                    Debug.Log("Female only");
                    transform.GetChild(1).gameObject.SetActive(true);
                }
                else
                {
                    entityRef.GetComponentInParent<Transform>().position = tolocation.transform.position;
                    FindObjectOfType<AudioManager>().plyAudio("door");
                }
            }
            else
            {
                entityRef.GetComponentInParent<Transform>().position = tolocation.transform.position;
                FindObjectOfType<AudioManager>().plyAudio("door");
            }
        }
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.CompareTag("Player"))
        {
            playerDetected = true;
            entityRef = collision.gameObject;
            transform.GetChild(0).gameObject.SetActive(true);
        }

    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerDetected = false;
            transform.GetChild(0).gameObject.SetActive(false);
        }
        if (isLockedDoor)
        {
            transform.GetChild(1).gameObject.SetActive(false);
        }
    }
}

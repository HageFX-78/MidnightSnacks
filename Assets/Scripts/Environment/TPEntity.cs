using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TPEntity : MonoBehaviour
{
    CamManager camManager;
    // Start is called before the first frame update
    public string thislocation;
    public Transform tolocation;
    public bool thisEnabled;
    private void Start()
    {
        camManager = CamManager.camInstance;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player")&&thisEnabled)
        {
            tolocation.GetComponentInParent<TPEntity>().thisEnabled = false;
            collision.GetComponentInParent<Transform>().position = tolocation.transform.position;
            if(thislocation =="road")
            {
                camManager.SwitchToWalkway();
            }
            else if(thislocation =="walkway")
            {
                camManager.SwithToRoad();    
            }
            thisEnabled = false;

        }
        
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        thisEnabled = true;
    }
}

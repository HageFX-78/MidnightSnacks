using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScanScript : MonoBehaviour
{
    CommonBehaviour parentRef;

    // Start is called before the first frame update
    void Start()
    {
        parentRef = gameObject.GetComponentInParent<CommonBehaviour>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.CompareTag("Player"))
        {
            
            if (collision.name=="Player")
            {
                parentRef.playerDetected = true;
                parentRef.alert("spotted");
                FindObjectOfType<AudioManager>().plyAudio("spotted");
            }
            else if (collision.name=="Officer")
            {
                parentRef.suspicious();
                //Debug.Log("Suspicious");
                FindObjectOfType<AudioManager>().plyAudio("spotted");
            }
            //Else wont suspect a thing
        }
        if(collision.CompareTag("Blood"))
        {
            parentRef.bloodDetected = true;
            parentRef.alert("blood");
            //Debug.Log("Immeidate Dead end");
        }
    }
}

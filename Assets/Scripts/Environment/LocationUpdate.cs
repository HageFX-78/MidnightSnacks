using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocationUpdate : MonoBehaviour
{
    [SerializeField] string locationName;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            collision.GetComponent<PlayerActions>().currentLocation = locationName;
        }
        else if(collision.CompareTag("Food"))
        {
            collision.GetComponent<CommonBehaviour>().currentLocation = locationName;
        }
    }
}

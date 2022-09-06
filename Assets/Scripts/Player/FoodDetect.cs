using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodDetect : MonoBehaviour
{
    PlayerActions player;
    private void Start()
    {
        player = this.GetComponentInParent<PlayerActions>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Food"))
        {
            player.foodInRange = true;
            player.detectedFoodID = collision.gameObject;
            player.DetectedTargetAnim = collision.GetComponent<Animator>().runtimeAnimatorController;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Food"))
        {
            player.foodInRange = false;
            player.detectedFoodID = null;
        }
    }
}

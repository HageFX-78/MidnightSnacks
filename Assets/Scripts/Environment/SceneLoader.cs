using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    //Scene loader for when player enters a specific collision box for next scene


    public PlayerActions plyerRef;
    bool playerDetected;
    public string toScene;
    // Start is called before the first frame update
    private void Start()
    {
        playerDetected = false;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && playerDetected)
        {
            FindObjectOfType<AudioManager>().plyAudio("door");
            playerDetected = false;
            PlayerStats.faceRight = plyerRef.faceRight;
            PlayerStats.morphedState = plyerRef.morphedState;
            PlayerStats.foodID = plyerRef.eatenFoodID.name;
            PlayerStats.animController = plyerRef.anim.runtimeAnimatorController;           
            SceneManager.LoadScene(toScene);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            playerDetected = true;
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
    }
}

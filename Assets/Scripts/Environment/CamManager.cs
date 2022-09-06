using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamManager : MonoBehaviour
{
    public Camera cam1;
    public Camera cam2;
    public static CamManager camInstance;
    // Start is called before the first frame update
    private void Awake()
    {
        camInstance = this;
        PlayerStats.animController = null;
        PlayerStats.faceRight = true;
        PlayerStats.foodID = "";
        PlayerStats.morphedState = false;
    }
    void Start()
    {
        
        FindObjectOfType<AudioManager>().plyAudio("startarea");
        cam1.enabled =true;
        cam2.enabled =false;
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void SwitchToWalkway()
    {
        cam2.enabled = true;
    }
    public void SwithToRoad()
    {
        cam2.enabled = false;
        
    }
}

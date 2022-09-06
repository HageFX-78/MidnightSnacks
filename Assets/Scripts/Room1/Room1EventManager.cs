using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class Room1EventManager : MonoBehaviour
{
    public GameObject player;
    private GameObject takeshi, himeko, himeno, sato;

    public TextMeshProUGUI headline, failReason;
    public GameObject endingScreen;
    public Transform event1TPLocation;
    public Transform momTP1;
    public Transform momTP2;
    GameObject[] npcList;

    bool lureCooldown;
    private void Start()
    {
        FindObjectOfType<AudioManager>().plyAudio("bgmroom1");
        npcList = GameObject.FindGameObjectsWithTag("Food");
        foreach (GameObject obj in npcList)//for easier referencing here with names
        {
            string chara = obj.name;
            if (chara == "Himeno")
            {
                himeno = obj;
            }
            else if(chara=="Himeko")
            {
                himeko = obj;
            }
            else if(chara=="Takeshi")
            {
                takeshi = obj;
            }
            else if(chara=="Sato")
            {
                sato = obj;
            }
        }
    }
    public void lureMother(string loc)
    {
        if(!lureCooldown)
        {
            FindObjectOfType<AudioManager>().plyAudio("fscream");
            Transform hTrans = himeno.transform;
            if(loc=="toilet")
            {
                hTrans.position = momTP1.position;
            }
            else if(loc=="bigRoom")
            {
                hTrans.position = momTP2.position;
                hTrans.localScale = new Vector3(hTrans.localScale.x * -1, 1, 1);
            }
            
            //hTrans.localScale = new Vector3(hTrans.localScale.x * -1, 1, 1);
            lureCooldown = true;
            Invoke("disabledLureCooldown", 1f);
        }
        
        
    }
    void disabledLureCooldown()
    {
        lureCooldown = false;
    }
    public bool vicinityCheck(GameObject target)
    {
        string targetLoc = target.GetComponent<CommonBehaviour>().currentLocation;
        for(int x=0;x<npcList.Length;x++)
        {
            GameObject current = npcList[x];
            if(current!=target&&current.activeSelf)
            {
                //Debug.Log(current.name);


                /*STOPS WORKING AFTER I ADDED AUDIO FOR SOME GODDAMN REASON
                 
                if(current.GetComponent<CommonBehaviour>().currentLocation == targetLoc)
                {
                    Invoke("triggerAlert", 4f);//Only alert after player finishes consumption so it doesnt cut abruptly
                    return true;
                }
               //*/
                
            }
        }
        return false;
    }
    public void eventFlag1()
    {
        string plyLoc = player.GetComponent<PlayerActions>().currentLocation;

        //Hard coded
        //Sato NPC would always be in living room unless this code runs so it should be fine.. i believe...
        if (!himeno.activeSelf&&!takeshi.activeSelf&&plyLoc!="living")
        {
            if(plyLoc!="bigRoom")
            {
                sato.transform.position = event1TPLocation.position + (new Vector3(6f,0,0));

            }
            else
            {
                sato.transform.position = event1TPLocation.position;
            }    
            
        }
        if(!himeno.activeSelf && !takeshi.activeSelf&&!himeko.activeSelf && !sato.activeSelf)
        {
            triggerAlert("win");
        }
    }
    public void triggerAlert()
    {
        triggerAlert("eating");
    }
    public void triggerAlert(string reason)
    {
        if(reason=="eatiing")
        {
            headline.text = "Man-Eating Monster Caught!!";
            failReason.text = "Someone saw you feasting...";
        }
        else if (reason == "spotted")
        {
            headline.text = "Weird Monster Caught!!";
            failReason.text = "Someone spotted your true form...";
        }
        else if(reason=="blood")
        {
            headline.text = "Man-Eating Monster Caught!!";
            failReason.text = "Someone saw bloodstains...";
        }      
        else if (reason=="suspicious")
        {
            headline.text = "Suspicious Man Caught";
            failReason.text = "You barged into someone house...";
        }
        else if(reason=="win")
        {
            headline.text = "Family of 4 went missing";
            failReason.text = "Full Course Meal, Thanks for playing!";
        }
        Invoke("GameOver", 2f);
        //Debug.LogWarning("PLAYER REPORTED");
    }

    public void GameOver()
    {
        Time.timeScale = 0f;
        FindObjectOfType<AudioManager>().plyAudio("gameover");
        endingScreen.SetActive(true);
    }
}

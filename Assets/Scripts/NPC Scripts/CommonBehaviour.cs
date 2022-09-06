using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommonBehaviour : MonoBehaviour
{
    [SerializeField] bool faceRight;
    public bool playerDetected, bloodDetected;//This may seem useless but it's an alternative to the scanning in ScanScript if i implemented it in time that is
    public string currentLocation;
    float symbolOffset;
    private string reportReason;
    Animator anim;
    Vector3 initialPos, currentPos;
    void Start()
    {
        playerDetected = false;
        bloodDetected = false;
        anim = gameObject.GetComponent<Animator>();
        initialPos = transform.localPosition;


        if(!faceRight)
        {
            transform.localScale = new Vector3(transform.localScale.x * -1, 1, 1);
        }
        symbolOffset = (gameObject.name == "Takeshi") ? 2.0f : 3.0f;
    }
    private void OnDisable()
    {
        
        
        CancelInvoke();
    }
    private void Update()
    {
        currentPos = transform.localPosition;
        if (initialPos==currentPos)
        {
            anim.SetBool("isMoving", false);
            
        }
        else
        {
            anim.SetBool("isMoving", true);
            initialPos = currentPos; 
        }
    }
    public void suspicious()
    {
        if(transform.childCount<=1)//1 to exclude scan area
        {
            GameObject eff = Instantiate((GameObject)Resources.Load("Suspicious"), transform.localPosition + (new Vector3(0, symbolOffset, 0)), Quaternion.identity);
            eff.transform.parent = gameObject.transform;
        }
        reportReason = "suspicious";
        Invoke("delayedTriggerAlert", 7f);
    }
    public void alert(string reason)
    {
        if (transform.childCount <= 1)//1 to exclude scan area
        {
            GameObject eff = Instantiate((GameObject)Resources.Load("Alert"), transform.localPosition + (new Vector3(0, symbolOffset, 0)), Quaternion.identity);
            eff.transform.parent = gameObject.transform;
        }
        
        reportReason = reason;
        delayedTriggerAlert();//Not actually delayed
    }

    public void delayedTriggerAlert()
    {
        FindObjectOfType<Room1EventManager>().triggerAlert(reportReason);
    }
}

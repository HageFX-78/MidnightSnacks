using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActions : MonoBehaviour
{
    [Header("References")]
    public Animator anim;
    public ScreenShake camRef;//Direct reference cuz there's multiple cams
    public GameObject bloodPrefab;
    public Room1EventManager eventManager;

    [Header("States")]
    public bool moveAllow;
    public bool faceRight;
    public bool morphedState;
    public bool foodInRange;
    public float eatTime = 0.25f;//0.25 for now
    bool morphAllow;
    public string currentLocation;

    [Header("Animator Controller References and Storage")]
    public RuntimeAnimatorController original;
    public RuntimeAnimatorController DetectedTargetAnim;
    public GameObject detectedFoodID;
    public GameObject eatenFoodID;
    public RuntimeAnimatorController EatenTargetAnim;

    [SerializeField] float movespeed;

    void Start()
    {
        morphAllow = true;
        morphedState = PlayerStats.morphedState;
        faceRight = PlayerStats.faceRight;
        if (faceRight)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }

        if (PlayerStats.foodID != "")
        {
            //Find referene within scene instead as i cant reference objects from previous scene - runs one time so it's fine right
            GameObject storedTarget = GameObject.Find(PlayerStats.foodID);
            eatenFoodID = storedTarget;
            EatenTargetAnim = storedTarget.GetComponent<Animator>().runtimeAnimatorController;
        }
        if (morphedState)
        {
            gameObject.name = eatenFoodID.name;
            anim.runtimeAnimatorController = PlayerStats.animController;
        }
        else
        {
            anim.runtimeAnimatorController = original;
        }


        moveAllow = true;
        foodInRange = false;

    }

    void Update()
    {
        //Animation update
        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        if (move == Vector3.zero)
        {
            anim.SetBool("isMoving", false);
        }
        else
        {
            anim.SetBool("isMoving", true);
        }

    }

    void FixedUpdate()
    {

        if (moveAllow)//Movement cooldown and prevent other acts
        {
            //Movement
            if (Input.GetKey(KeyCode.D))
            {
                transform.position += new Vector3(movespeed * Time.fixedDeltaTime, 0, 0);
                if (!faceRight)
                {
                    flip();
                }

            }
            else if (Input.GetKey(KeyCode.A))
            {
                transform.position += new Vector3(-movespeed * Time.fixedDeltaTime, 0, 0);
                if (faceRight)
                {
                    flip();
                }
            }
            if (Input.GetKey(KeyCode.W))
            {
                transform.position += new Vector3(0, movespeed * Time.fixedDeltaTime, 0);
            }
            else if (Input.GetKey(KeyCode.S))
            {
                transform.position += new Vector3(0, -movespeed * Time.fixedDeltaTime, 0);
            }

            //Eat action
            if (Input.GetKey(KeyCode.E))
            {
                if (morphedState)
                {
                    if (gameObject.name == "Takeshi" && currentLocation != "")
                    {
                        eventManager.lureMother(currentLocation);
                    }
                    else
                    {
                        Debug.Log("No effect");
                        //play error sound maybe if i have time left...
                    }
                }
                else
                {
                    if (!anim.GetBool("isEating") && foodInRange)
                    {

                        if (currentLocation != "")
                        {
                            if (eventManager.vicinityCheck(detectedFoodID))
                            {
                                Debug.Log("True");
                            }
                        }

                        anim.SetBool("isEating", true);
                        FindObjectOfType<AudioManager>().plyAudio("eat");
                        FindObjectOfType<AudioManager>().plyAudio("crunch");
                        Invoke("killFood", 0.9f);
                        Invoke("morph", 3f);
                        moveAllow = false;
                    }
                }


            }
            //Manual Morph

            if (Input.GetKey(KeyCode.Mouse1) && eatenFoodID && morphAllow)
            {
                //Debug.Log(eatenFoodID.name);
                if (morphedState == false)
                {
                    morph();
                    morphAllow = false;
                    Invoke("morphCooldown", 1f);
                }
                else
                {
                    //return to original
                    anim.runtimeAnimatorController = original;
                    gameObject.name = "Player";
                    morphedState = false;
                    morphAllow = false;
                    Invoke("morphCooldown", 1f);
                }

            }
        }
    }

    void flip()
    {
        faceRight = !faceRight;
        transform.localScale = new Vector3(transform.localScale.x * -1, 1, 1);
    }
    void morphCooldown()
    {
        morphAllow = true;
    }
    public void killFood()
    {

        //PLaysound code here too remember

        eatenFoodID = detectedFoodID;
        EatenTargetAnim = DetectedTargetAnim;

        Vector3 foodPos = detectedFoodID.GetComponent<Transform>().localPosition;
        foodPos = new Vector3(foodPos.x, foodPos.y - 1f, foodPos.z);
        Instantiate(bloodPrefab, foodPos, Quaternion.identity);
        if (detectedFoodID.name == "Sato"||detectedFoodID.name == "Officer")
        {
            FindObjectOfType<AudioManager>().plyAudio("mscream");
        }
        else
        {
            FindObjectOfType<AudioManager>().plyAudio("fscream");
        }
        detectedFoodID.SetActive(false);

        //Functions of other scripts
        
        camRef.ShakeScreen(eatTime);
        if(currentLocation != "")
        {
            eventManager.eventFlag1();
        }
        
    }
    public void morph()
    {
        anim.SetBool("isEating", false);
        
        anim.runtimeAnimatorController = EatenTargetAnim;
        gameObject.name = eatenFoodID.name;
        moveAllow = true;
        morphedState = true;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gkAnimationControl : MonoBehaviour
{
    Animator animator;
    int isTCHash, isTLCHash, isTRCHash, isBCHash, isBRCHash, isBLCHash;
    private AnimationStateControl kicking;
    private bool thisInput;
    bool s1, s2, s3, s4, s5, s6;
    private MeshCollider meshCollider;
    public int direction;
    bool gkActivated;

    private bool moveStarts;
    private RespawnTrigger respawnTrigger;


    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        isTCHash = Animator.StringToHash("isTC");
        isTLCHash = Animator.StringToHash("isTLC");
        isTRCHash = Animator.StringToHash("isTRC");
        isBCHash = Animator.StringToHash("isBC");
        isBLCHash = Animator.StringToHash("isBLC");
        isBRCHash = Animator.StringToHash("isBRC");

        //Will use this to activate goalkeeper animation once kicker takes the shot
        kicking = GameObject.Find("Kicker").GetComponent<AnimationStateControl>();
    }

    // Update is called once per frame
    void Update()
    {
        /*s1 = kicking.k11;
        s2 = kicking.k22;
        s3 = kicking.k33;
        s4 = kicking.k44;
        s5 = kicking.k55;
        s6 = kicking.k66;*/
        gkActivated = kicking.activateGK;
 
        if (gkActivated)
        {
            //Take a random number between 1 and 7 (7 exclusive) and then perform the animation assigned to that number
            direction = Random.Range(1, 7);
            gkActivated = false;
            kicking.activateGK = false;
            if (direction == 1)
            {
                Debug.Log(direction);
                Debug.Log("Starting GK");
                GameObject.Find("TLC").GetComponent<BoxCollider>().enabled = true;
                Invoke("triggerTLC", 2.5f);
            }
            else if (direction == 2)
            {

                Debug.Log("Starting GK");
                GameObject.Find("TRC").GetComponent<BoxCollider>().enabled = true;
                Invoke("triggerTRC", 2.7f);
            }
            else if (direction == 3)
            {
                Debug.Log("Starting GK");
                GameObject.Find("BLC").GetComponent<BoxCollider>().enabled = true;
                Invoke("triggerBLC", 2.5f);
            }
            else if (direction == 4)
            {
                Debug.Log("Starting GK");
                GameObject.Find("BRC").GetComponent<BoxCollider>().enabled = true;
                Invoke("triggerBRC", 2.5f);
            }
            else if (direction == 5)
            {
                Debug.Log("Starting GK");
                Invoke("triggerTC", 3.0f);
                GameObject.Find("TC").GetComponent<BoxCollider>().enabled = true;
            }
            else if (direction == 6)
            {
                Debug.Log("Starting GK");
                Invoke("triggerBC", 2.5f);
                GameObject.Find("BC").GetComponent<BoxCollider>().enabled = true;
            }
        }
    }

    void triggerTLC()
    {
        animator.SetBool(isTLCHash, true);

        Invoke("triggerGK", 0.5f);
    }

    void triggerTRC()
    {
        animator.SetBool(isTRCHash, true);

        Invoke("triggerGK", 0.5f);
    }

    void triggerBLC()
    {
        animator.SetBool(isBLCHash, true);

        Invoke("triggerGK", 0.5f);
    }

    void triggerBRC()
    {
        animator.SetBool(isBRCHash, true);

        Invoke("triggerGK", 0.5f);
    }
    void triggerTC()
    {
        animator.SetBool(isTCHash, true);

        Invoke("triggerGK", 0.5f);
    }

    void triggerBC()
    {
        animator.SetBool(isBCHash, true);

        Invoke("triggerGK", 1.2f);
    }
    void triggerGK()
    {
        animator.SetBool(isTLCHash, false);
        animator.SetBool(isTRCHash, false);
        animator.SetBool(isBLCHash, false);
        animator.SetBool(isBRCHash, false);
        animator.SetBool(isTCHash, false);
        animator.SetBool(isBCHash, false);
        /*s1 = false;
        s2 = false;
        s3 = false;
        s4 = false;
        s5 = false;
        s6 = false;*/
        Invoke("turnOffCollider", 0.75f);
        gkActivated = false;
        kicking.activateGK = false;
    }
    void turnOffCollider()
    {
        GameObject.Find("TLC").GetComponent<BoxCollider>().enabled = false;
        GameObject.Find("TRC").GetComponent<BoxCollider>().enabled = false;
        GameObject.Find("BLC").GetComponent<BoxCollider>().enabled = false;
        GameObject.Find("BRC").GetComponent<BoxCollider>().enabled = false;
        GameObject.Find("TC").GetComponent<BoxCollider>().enabled = false;
        GameObject.Find("BC").GetComponent<BoxCollider>().enabled = false;
    }
}
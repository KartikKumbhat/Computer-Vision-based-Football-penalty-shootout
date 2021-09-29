using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

public class AnimationStateControl : MonoBehaviour
{

    Animator animator;
    int isPrepHash;
    int isKickHash;
    int isWalkBackHash;
    public bool input;
    private bool k1, k2, k3, k4, k5, k6;
    public bool k11, k22, k33, k44, k55, k66;
    public bool activateGK = false;

    Thread receiveThread;
    UdpClient client;
    int port;


    void Start()
    {

        port = 5065;
        InitUDP();

        animator = gameObject.GetComponent<Animator>();
        isPrepHash = Animator.StringToHash("isPrep");
        isKickHash = Animator.StringToHash("isKick");
        isWalkBackHash = Animator.StringToHash("isWalkBack");
        Debug.Log(animator);
    }
    private void InitUDP()
    {
        Debug.Log("UDP Initialized");

        receiveThread = new Thread(new ThreadStart(ReceiveData)); 
        receiveThread.IsBackground = true;
        receiveThread.Start();

    }

    // 4. Receive Data
    private void ReceiveData()
    {
        client = new UdpClient(port);
        while (true)
        {
            try
            {
                IPEndPoint anyIP = new IPEndPoint(IPAddress.Parse("0.0.0.0"), port);
                byte[] data = client.Receive(ref anyIP);

                string text = Encoding.UTF8.GetString(data);
                Debug.Log(text);

                if (text == "TLC")
                {
                    k1 = true;
                }
                else if(text == "TRC")
                {
                    k2 = true;
                }
                else if(text == "BLC")
                {
                    k3 = true;
                }
                else if(text == "BRC")
                {
                    k4 = true;
                }
                /*else if(text == "TC")
                {
                    k5 = true;
                }*/
    
            }
            catch (System.Exception e)
            {
                print(e.ToString());
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        bool isKick = animator.GetBool("isKick");
        /*k1 = Input.GetKeyDown(KeyCode.Keypad1);
        k2 = Input.GetKeyDown(KeyCode.Keypad2);
        k3 = Input.GetKeyDown(KeyCode.Keypad3);
        k4 = Input.GetKeyDown(KeyCode.Keypad4);*/
        k5 = Input.GetKeyDown(KeyCode.Keypad5);
        k6 = Input.GetKeyDown(KeyCode.Keypad6);

        if (k1)
        {
            k1 = false;
            k11 = true;
            animator.SetBool(isPrepHash, true);
            Invoke("setFalse", .5f);
            activateGK = true;
        }
        else if (k2)
        {
            k2 = false;
            k22 = true;
            animator.SetBool(isPrepHash, true);
            Invoke("setFalse", 1.09f);
            activateGK = true;
        }
        else if (k3)
        {
            k3 = false;
            k33 = true;
            animator.SetBool(isPrepHash, true);
            Invoke("setFalse", 1.09f);
            activateGK = true;
        }
        else if (k4)
        {
            k4 = false;
            k44 = true;
            animator.SetBool(isPrepHash, true);
            Invoke("setFalse", 1.09f);
            activateGK = true;
        }
        else if (k5)
        {
            k5 = false;
            k55 = true;
            animator.SetBool(isPrepHash, true);
            Invoke("setFalse", 1.09f);
            activateGK = true;
        }
        else if (k6)
        {
            k66 = true;
            animator.SetBool(isPrepHash, true);
            Invoke("setFalse", 1.09f);
            activateGK = true;
        }
    }
    void setFalse()
    {
        animator.SetBool(isPrepHash, false);
        animator.SetBool(isKickHash, false);
    }
}
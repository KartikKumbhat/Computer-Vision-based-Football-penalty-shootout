using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kicking : MonoBehaviour
{
    [SerializeField] public Transform tlc;
    [SerializeField] public Transform trc;
    [SerializeField] public Transform blc;
    [SerializeField] public Transform brc;
    [SerializeField] public Transform tc;
    [SerializeField] public Transform bc;
    float force = 75;
    private AnimationStateControl keyInput;
    public bool thisInput;
    bool k1, k2, k3, k4, k5, k6;
//    bool tlc, trc, blc, brc, tc, bc;
    // Start is called before the first frame update

    void Start()
    {
        //To retrieve which shot direction is set to true in AnimationStateControl script
        keyInput = GameObject.Find("Kicker").GetComponent<AnimationStateControl>();
    }

    // Update is called once per frame
    void Update()
    {
        k1 = keyInput.k11;
        k2 = keyInput.k22;
        k3 = keyInput.k33;
        k4 = keyInput.k44;
        k5 = keyInput.k55;
        k6 = keyInput.k66;
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.name);
        if (other.CompareTag("Ball"))
        {
            Debug.Log("Collision detected");
            if (k1)
            {
                Vector3 dir = tlc.position - transform.position;
                other.GetComponent<Rigidbody>().velocity = dir.normalized * force + new Vector3(0, 6f, 0);
                Debug.Log("Kicker kicked at top left corner");
                keyInput.k11 = false;
                k1 = false;
            }
            else if (k2)
            {
                Vector3 dir = trc.position - transform.position;
                other.GetComponent<Rigidbody>().velocity = dir.normalized * force + new Vector3(0, 6f, 0);
                Debug.Log("Kicker kicked at top right corner");
                keyInput.k22 = false;
                k2 = false;
            }
            else if (k3)
            {
                Vector3 dir = blc.position - transform.position;
                other.GetComponent<Rigidbody>().velocity = dir.normalized * force + new Vector3(0, 0, 0);
                Debug.Log("Kicker kicked at bottom left corner");
                keyInput.k33 = false;
                k3 = false;
            }
            else if (k4)
            {
                Vector3 dir = brc.position - transform.position;
                other.GetComponent<Rigidbody>().velocity = dir.normalized * force + new Vector3(0, 0, 2f);
                Debug.Log("Kicker kicked at bottom right corner");
                keyInput.k44 = false;
                k4 = false;
            }
            else if (k5)
            {
                Vector3 dir = tc.position - transform.position;
                other.GetComponent<Rigidbody>().velocity = dir.normalized * force + new Vector3(0, 5f, 0);
                Debug.Log("Kicker kicked at top center");
                keyInput.k55 = false;
                k5 = false;
            }
            else if (k6)
            {
                Vector3 dir = bc.position - transform.position;
                other.GetComponent<Rigidbody>().velocity = dir.normalized * force + new Vector3(0, 3f, 0);
                Debug.Log("Kicker kicked at bottom center");
                keyInput.k66 = false;
                k6 = false;
            }
        }
    }
}
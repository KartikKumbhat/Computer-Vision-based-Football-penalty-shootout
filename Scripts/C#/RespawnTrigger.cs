using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnTrigger : MonoBehaviour
{
    //When the ball passes this point which is a mesh-less rectangle in scene
    //destroy the ball game object in 2 seconds
    //instantiate another ball game object in 3 seconds

    ///bool collision;
    [SerializeField] private GameObject footballPrefab;
    //[SerializeField] private Transform football;
    [SerializeField] private Vector3 respawnPoint;
    public bool moveStarts;

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Ball"))
            {
            moveStarts = true;
            Destroy(other.gameObject, 2);
            Invoke("ballRespawn", 3);
            Debug.Log("Respawning in 3 seconds");
            }
    }
    void ballRespawn()
    {
        GameObject football = Instantiate(footballPrefab, respawnPoint, Quaternion.identity);
        Debug.Log("Ball respawned");
    }
}
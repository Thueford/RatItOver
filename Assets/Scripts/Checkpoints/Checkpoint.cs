using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public GameObject cc;
    private bool activated = true;

    void OnTriggerEnter(Collider other) {
        Debug.Log("Collision " + other.tag);
        if (activated) {
            if (other.CompareTag("Player")) {
                activated = false;
                Debug.Log("Collision with Player");
                cc.GetComponent<CheckpointController>().ActivateCheckpoint(gameObject);
                Debug.Log("lastCheckPointPosition:");
            }  
        }
        
    }
}

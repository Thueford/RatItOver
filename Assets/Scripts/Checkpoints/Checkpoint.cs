using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public int id;
    private bool activated = false;

    Animator anim;

    void Start() {
        Debug.Log("Checkpoint init");
        anim = GetComponent<Animator>();
        anim.enabled = false;
    }
    public void activateCheckpoint() 
    {
        Debug.Log("Activate Checkpoint");
        activated = true;
        anim.enabled = true;
    }

    public bool isActive() {
        return activated;
    }
}

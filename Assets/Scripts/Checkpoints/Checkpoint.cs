using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public int id;
    private bool activated = false;
    Animation anim;

    void Start()
    {
    }
    
    public void activateCheckpoint() {
        activated = true;
    }

    public bool isActive() {
        return activated;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointController : MonoBehaviour
{
    

    public List<GameObject> checkpoints = new List<GameObject>();
    public bool Activated = false;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Checkpoint Controller init!");
        checkpoints = new List<GameObject>(GameObject.FindGameObjectsWithTag("Checkpoint"));
    }

    // Speichert aktuellen Checkpoint
    public void SetCheckpoint(GameObject checkpoint) 
    {
        PlayerPrefs.SetInt("currentCheckpoint", checkpoints.IndexOf(checkpoint));
    }

    public void ActivateCheckpoint(GameObject checkpoint)
    {
        SetCheckpoint(checkpoint);
    }

    public GameObject GetCheckpoint() 
    {
        int checkpointId = PlayerPrefs.GetInt("currentCheckpoint", -1);
        if (checkpointId == -1) 
        {
            return null;
        }
        else
        {
            return checkpoints[checkpointId];
        }
    }

    public void ResetToCheckpoint() 
    {
        GameObject checkpoint = GetCheckpoint();
        if (checkpoint != null)
        {
            PlayerPhysics player = FindObjectOfType<PlayerPhysics>();
            player.transform.position = checkpoint.transform.position;
        }
    }
}



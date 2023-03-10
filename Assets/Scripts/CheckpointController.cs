using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointController : MonoBehaviour
{
    public List<GameObject> checkpoints = new List<GameObject>();

    // Speichert aktuellen Checkpoint
    public void SetCheckpoint(GameObject checkpoint) 
    {
        PlayerPref.SetInt("currentCheckpoint", checkpoint.id);
    }

    public GameObject GetCheckpoint() 
    {
        int checkpointId = PlayerPref.GetInt("currentCheckpoint", -1);
        if (checkpointId == -1) 
        {
            return null;
        }
        else
        {
            return GameObject.Find(checkpointId);
        }
    }

    public void ResetToCheckpoint() 
    {
        GameObject checkpoint = GetCheckpoint();
        if (checkpoint != null)
        {
            PlayerController player = FindObjectOfType
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        checkpoints = GameObject.FindWithTag("Checkpoint").ToList();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

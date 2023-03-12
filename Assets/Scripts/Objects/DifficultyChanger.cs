using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifficultyChanger : MonoBehaviour
{
    
    public void setDifficulty(string difficulty) 
    {
        Debug.Debug.Log("Set Difficulty to " + difficulty);
        PlayerPrefs.SetString("Difficulty", difficulty);
    }
}

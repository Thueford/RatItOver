using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DifficultyChanger : MonoBehaviour
{
    void Start() {
        Debug.Log("Set Difficulty to easy");
        PlayerPrefs.SetString("difficulty", "easy");
    }
    public void setDifficulty(Toggle toggle) 
    {
        if (toggle.isOn) {
            TMP_Text text = toggle.GetComponentInChildren<TMP_Text>();
            PlayerPrefs.SetString("difficulty", text.text);
            Debug.Log("Set Difficulty to " + PlayerPrefs.GetString("difficulty", "none"));
        }
    }
}


///////////////////////
//Do not touch this!!//
///////////////////////

using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public void btnPressed_Credits()
    {
        UnityEngine.Debug.Log("Credits");
        SceneManager.LoadScene("CreditScene");
    }

    public void btnPressed_Start()
    {
        UnityEngine.Debug.Log("Story Start");
        SceneManager.LoadScene("story_scene");
    }

    public void btnPressed_StartJetztAberWirklich()
    {
        UnityEngine.Debug.Log("Start");
        SceneManager.LoadScene("Main");
    }

    public void btnPressed_Close()
    {
        UnityEngine.Debug.Log("Goodbye");
        Application.Quit();
    }

    public void btnPressed_BackToStartScene()
    {
        UnityEngine.Debug.Log("Menu Backstart");
        SceneManager.LoadScene("StartScene");
    }

    public void btnPressed_Anleitung()
    {
        UnityEngine.Debug.Log("Start Anleitung");
        SceneManager.LoadScene("explanation_scene");
    }
}

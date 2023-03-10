
///////////////////////
//Do not touch this!!//
///////////////////////

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{

    public void btnPressed_Credits()
    {
        SceneManager.LoadScene("CreditScene");
    }

    public void btnPressed_Start()
    {
        Debug.Log("Menu Start");
        SceneManager.LoadScene("Main");
    }

    public void btnPressed_Close()
    {
        Debug.Log("Goodbye");
        Application.Quit();
    }

    public void btnPressed_BackToStartScene()
    {
        Debug.Log("Menu Backstart");
        SceneManager.LoadScene("StartScene");
    }
}

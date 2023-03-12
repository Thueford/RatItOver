using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    public UnityEngine.InputSystem.UI.InputSystemUIInputModule uiInputs;
    public Image panel;
    public static bool pause => Time.timeScale == 0;
    public AudioSource audioSource;
    public AudioClip[] audioClips;
    public Button defaultSelection;

    // Start is called before the first frame update
    void Start()
    {
        // uiInputs.cancel.action.performed += (_) => endPause();
        //panel = GetComponent<Image>();
        panel.gameObject.SetActive(false);
    }

    public void enterPause()
    {
        panel.gameObject.SetActive(true);
        SoundHandler.PlayClick();
        GameController.SetTimeScale(0);
        defaultSelection.Select();
    }

    public void endPause()
    {
        SoundHandler.PlayClick();
        panel.gameObject.SetActive(false);
        GameController.SetTimeScale(1);
    }

    public void Exit()
    {
        SoundHandler.PlayClick();
        Time.timeScale = 1;
        SceneManager.LoadScene("StartScene");
    }

    public void restart()
    {
        GameController.SetTimeScale(1);
        GameController.RestartGame();
        panel.gameObject.SetActive(false);
    }
}

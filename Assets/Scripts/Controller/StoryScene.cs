using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine;

public class StoryScene : MonoBehaviour
{
    public TMPro.TextMeshProUGUI text;
    public InputActionMap gameplayActions;
    public Image overlay;
    private RectTransform tText;

    public bool repeat = true;
    private string[] s;
    private int iText, iChar;

    private void Start()
    {
        if (!text)
        {
            Destroy(this);
            return;
        }

        text.gameObject.SetActive(false);
        s = text.text.Trim().Replace("\n", "%\n").Split('%');
        gameplayActions["Click"].performed += ctx => Toggle(true, null);
        foreach (var action in gameplayActions) action.Enable();
        Debug.Log(Keyboard.current + " " + Mouse.current);
        gameplayActions.devices = new InputDevice[] { Keyboard.current, Mouse.current };
        tText = text.transform.GetComponent<RectTransform>();

        ev_Done();
    }

    public void Toggle(bool status, Collider2D c)
    {
        // start talking
        if (!text.IsActive())
        {
            if (!repeat && iText > 0) return;
            text.gameObject.SetActive(true);
            text.text = "";
            iText = iChar = 0;
            //KeyHandler.enableMovement = false;
            InvokeRepeating(nameof(DisplayText), 0, 0.03f);
        }
        // skip talking
        else if (iChar < s[iText].Length)
        {
            CancelInvoke();
            while (iChar < s[iText].Length) DisplayText();
        }
        // stop talking
        else if (++iText >= s.Length)
        {
            text.text = "";
            text.gameObject.SetActive(false);
            //KeyHandler.enableMovement = true;
        }
        // continue talking
        else
        {
            iChar = 0;
            InvokeRepeating(nameof(DisplayText), 0, 0.03f);
        }
    }

    private void DisplayText()
    {
        // Events
        if (text.text == "" && s[iText][iChar] == '[')
        {
            handleEvent(s[iText].Substring(iChar + 1, s[iText].IndexOf(']') - iChar - 1));
            iText++;
            return;
        }

        // mid pause
        if (iChar == s[iText].Length || s[iText][iChar] == '%') CancelInvoke();
        // newline
        else if (s[iText][iChar] == '/') text.text += "\n";
        // next line
        else if (s[iText][iChar] == '\n') text.text = "";
        // delays
        else if (s[iText][iChar] != '_') text.text += s[iText][iChar];
        iChar++;
    }

    public void handleEvent(string name)
    {
        Invoke("ev_" + name, 0);
    }

    int fadeDir = 1;
    public void ev_Done()
    {
        Debug.Log("ev_Done");
        fadeDir = -fadeDir;
        gameplayActions.Disable();
        CancelInvoke();
        text.gameObject.SetActive(false);
        overlay.enabled = true;
        overlay.color = new Color(0, 0, 0, fadeDir == 1 ? 0 : 1);
        InvokeRepeating(nameof(Fade), 0, 0.1f);
    }

    int shakeFrames = 0;
    Vector2 apos;
    public void ev_Shake()
    {
        apos = tText.anchoredPosition;
        shakeFrames = 50;
    }

    void Update()
    {
        if (shakeFrames-- <= 0) return;
        if (shakeFrames == 0) tText.anchoredPosition = apos;
        else tText.anchoredPosition = apos + 2 * new Vector2(Random.value - 0.5f, Random.value - 0.5f).normalized;
    }

    void Fade()
    {
        Color c = overlay.color;
        c.a += fadeDir * 0.1f / 2;
        overlay.color = c;
        if (fadeDir == 1 && c.a >= 1) GetComponent<ChangeScene>().btnPressed_StartJetztAberWirklich();
        if (fadeDir == -1 && c.a <= 0)
        {
            CancelInvoke();
            overlay.enabled = false;
            gameplayActions.Enable();
            Toggle(true, null);
        }
    }
}

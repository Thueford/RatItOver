using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TMPro.TextMeshProUGUI))]
public class StatusText : MonoBehaviour
{
    public static StatusText self;

    private float tShow = 0.2f, tDur = 0, tHide = 0.2f;
    private float scale = 11, angle = 8;

    private TMPro.TextMeshProUGUI txt;

    void Awake()
    {
        self = this;
        txt = GetComponent<TMPro.TextMeshProUGUI>();
        txt.alpha = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Pause.pause || txt.alpha <= 0 && scale >= 10) return;
        float dt = Time.deltaTime / Time.timeScale;

        if (scale < 1)
        {
            scale = Mathf.Clamp01(scale + dt / tShow);
            txt.alpha = scale;
            transform.rotation = Quaternion.Euler(0, 0, angle * scale);
        }
        else if (tDur <= 0.01 || scale >= 1.1f)
        {
            scale += scale * dt / tHide;
            txt.alpha -= dt / tHide;
        }
        else scale += 0.1f * dt / tDur;

        transform.localScale = scale * Vector3.one;
    }

    public static void ShowText(string text, float duration, float tShow = 0.3f, float tHide = 0.3f, float angle = 8)
    {
        Debug.Log("StatusText " + text);
        self.txt.SetText(text);
        self.tDur = duration;
        self.tShow = tShow;
        self.tHide = tHide;
        self.angle = angle;
        self.txt.alpha = self.scale = 0;
    }
}

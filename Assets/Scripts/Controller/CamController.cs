using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamController : MonoBehaviour
{
    public static CamController self;

    [Range(1, 50)]
    public float lazyness = 10;
    public GameObject[] targets;
    // public Vector2 offset = Vector2.zero;
    [SerializeField]
    private float zmin = 11f;
    [SerializeField]
    private float zmax = 15f;
    [SerializeField]
    private int targetFPS = 60;
    public int remainingShakeFrames = 0; // TODO: Convert to Duration
    [SerializeField, Range(0f, 1f)]
    private float shakeIntensity = 0.08f;
    private Vector3 stablePosition;

    public float maxDist => zmax * 2;

    void Awake()
    {
        self = this;
    }

    private void Start()
    {
        stablePosition = transform.position;
        InvokeRepeating(nameof(changeFramerate), 1f, 1f);
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = targetFPS;
        Time.fixedDeltaTime = 1f / targetFPS;
    }

    void changeFramerate()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = targetFPS;
        Time.fixedDeltaTime = 1f / targetFPS;
    }

    // private void LateUpdate() {   
    //     var dt = Time.unscaledDeltaTime;
    //     var targetDeltaTime = 1 / targetFPS;
    //     if (dt > targetDeltaTime) {
    //         Time.timeScale = targetDeltaTime / dt;
    //         Debug.Log(Time.timeScale);
    //         if (Time.timeScale == 0) Time.timeScale = 1;
    //     }
    // }

    private void Update()
    {
        // Debug.Log("fps " + (1 / Time.deltaTime));
        if (targets.Length == 0) return;

        var t = Vector3.zero;
        foreach (var target in targets)
            t += target.transform.position;
        t /= targets.Length;

        Vector3 dir = t - stablePosition;
        dir.z = dir.y = 0;
        dir += transform.forward * zmax;

        Vector3 pos = stablePosition + dir / lazyness;
        stablePosition = pos;
        if (remainingShakeFrames > 0 && !GameController.self.isHitlag)
        {
            pos += new Vector3(UnityEngine.Random.value - 0.5f,
                               UnityEngine.Random.value - 0.5f,
                               0).normalized * shakeIntensity;
            --remainingShakeFrames;
        }

        transform.position = pos;
    }

    public void ScreenShake(int duration)
    {
        if (duration == 0) return;
        remainingShakeFrames = Mathf.Max(duration, remainingShakeFrames);
    }
}

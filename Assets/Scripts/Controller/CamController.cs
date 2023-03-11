using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamController : MonoBehaviour
{
    public static CamController self;
    public static Camera cam;

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
    private Vector3 pos;
    private Vector3 relPos;

    public float maxDist => zmax * 2;

    void Awake()
    {
        self = this;
        cam = GetComponent<Camera>();
    }

    private void Start()
    {
        pos = transform.position;
        relPos = pos - Player.player.pos;
        Time.fixedDeltaTime = 1f / targetFPS;
    }

    private void FixedUpdate()
    {
        // Debug.Log("fps " + (1 / Time.deltaTime));
        if (targets.Length == 0) return;

        var t = Vector3.zero;
        foreach (var target in targets)
            t += target.transform.position;
        t /= targets.Length;

        Vector3 dir = t - pos + relPos;
        dir.z = 0;
        pos += dir / lazyness;

        Vector3 shakePos = pos;
        if (remainingShakeFrames > 0 && !GameController.self.isHitlag)
        {
            shakePos += shakeIntensity * new Vector3(Random.value - 0.5f, Random.value - 0.5f, 0).normalized;
            --remainingShakeFrames;
        }
        transform.position = shakePos;
    }

    public void ScreenShake(int duration)
    {
        if (duration == 0) return;
        remainingShakeFrames = Mathf.Max(duration, remainingShakeFrames);
    }
}

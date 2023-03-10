using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleBase : MonoBehaviour
{
    [SerializeField] protected ParticleSystem ps;
    [ReadOnly] public int forward = 1;
    [ReadOnly] public bool displace = false;
    private int spawnTime;

    protected void Start() {
        if (displace)
            transform.position += new Vector3(Random.value, Random.value, 0) * 1.5f;
        transform.localScale = new Vector3(-Mathf.Sign(forward), -Mathf.Sign(forward), 1);
        spawnTime = Time.frameCount;
    }

    protected void Update() {
        if (GameController.freeze)
        {
            if (!ps.isPaused) ps.Pause();
            return;
        }
        else if (ps.isPaused)
        {
            ps.Play();
        }
        if (ps.particleCount == 0 && Time.frameCount > spawnTime + 1)
        {
            Destroy(gameObject);
        }
    }
}

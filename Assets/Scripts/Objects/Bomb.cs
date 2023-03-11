using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public float speed, strength, radius;
    internal Vector3 dir;

    // Start is called before the first frame update
    void Start()
    {
        // ArrayList GetComponents<SphereCollider>().
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += speed * dir * Time.deltaTime;
    }

    void OnCollisionEnter(Collision collision)
    {
        StarteMegaFetteExplosion();
    }

    void StarteMegaFetteExplosion()
    {
        UnityEngine.Debug.Log("Das ist eine Mega Fette Explosion");
        Vector2 force = Player.player.pos - transform.position;
        float radiusSq = radius * radius;
        if (force.sqrMagnitude > radiusSq) return;

        float f = (radiusSq - force.sqrMagnitude) / radiusSq;
        force = strength * f * force.normalized;
        Player.player.physics.rb.AddForce(force, ForceMode.VelocityChange);
    }
}

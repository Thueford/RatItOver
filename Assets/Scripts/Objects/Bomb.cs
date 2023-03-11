using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public float throwHeight, strength, radius;
    internal Vector3 dir;

    // Start is called before the first frame update
    void Start()
    {
        System.Array.Find(GetComponents<SphereCollider>(), o => o.isTrigger);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Helper.GetJumpSpeed(throwHeight) * dir * Time.deltaTime;
    }

    void OnCollisionEnter(Collision collision)
    {
        StarteMegaFetteExplosion();
    }

    void StarteMegaFetteExplosion()
    {
        UnityEngine.Debug.Log("Das ist eine Mega Fette Explosion");
        Bounds b = GetComponent<Collider>().bounds;
        Vector2 force = Player.player.pos - (b.center - b.extents.y * Vector3.up);
        float radiusSq = radius * radius;
        Destroy(gameObject, 0);
        Instantiate(Prefabs.self.explosion, transform.position, Quaternion.identity);
        if (force.sqrMagnitude > radiusSq) return;

        float f = (radiusSq - force.sqrMagnitude) / radiusSq;
        force = Helper.GetJumpSpeed(strength) * f * force.normalized;
        Player.player.physics.rb.AddForce(force, ForceMode.VelocityChange);
    }
}

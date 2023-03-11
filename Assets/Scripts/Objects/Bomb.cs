using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public float throwHeight, strength, radius;
    internal Vector3 dir;
    private bool exploded = false;
    public static float tLastExpl = 0;

    void Awake() => Bomb.tLastExpl = 0;

    void Start()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.AddForce(Helper.GetJumpSpeed(throwHeight) * dir, ForceMode.VelocityChange);
        System.Array.Find(GetComponents<SphereCollider>(), o => o.isTrigger);
    }

    void OnCollisionEnter(Collision collision) => StarteMegaFetteExplosion();

    void StarteMegaFetteExplosion()
    {
        if (exploded) { Debug.Log("Saved your ass"); return; }
        exploded = true;

        Bounds b = GetComponent<Collider>().bounds;
        Vector2 force = Player.player.rat.transform.position - (b.center - b.extents.y * Vector3.up);

        Destroy(gameObject, 0);
        Instantiate(Prefabs.self.explosion, transform.position, Quaternion.identity);

        tLastExpl = Time.time;
        float radiusSq = radius * radius;
        if (force.sqrMagnitude > radiusSq) return;

        float f = (radiusSq - force.sqrMagnitude) / radiusSq;
        force = Helper.GetJumpSpeed(strength) * f * force.normalized;
        Player.player.physics.rb.AddForce(force, ForceMode.VelocityChange);
    }
}

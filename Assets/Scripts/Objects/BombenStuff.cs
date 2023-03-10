using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using UnityEngine;

public class BombenStuff : MonoBehaviour
{
    public float delay_explosion;
    public float explosion_strength;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision collision)
    {
        StarteMegaFetteExplosion();
    }

    void StarteMegaFetteExplosion()
    {
        UnityEngine.Debug.Log("Das ist eine Mega Fette Explosion");
    }
}

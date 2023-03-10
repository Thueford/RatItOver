using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class BombenStuff : MonoBehaviour
{
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

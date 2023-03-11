using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Helper;

public class SemiPlatform : MonoBehaviour
{
    Collider coll;
    void Awake() => coll = GetComponentInChildren<Collider>();
    void Update()
    {
        bool enbl = coll.bounds.max.y < Player.player.coll.bounds.min.y;
        if (coll.enabled != enbl) coll.enabled = enbl;
    }
}

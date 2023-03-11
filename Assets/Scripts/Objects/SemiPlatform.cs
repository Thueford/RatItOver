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
        bool enbl = Player.player.coll.bounds.min.y - coll.bounds.max.y > -0.03;
        if (coll.enabled != enbl) coll.enabled = enbl;
    }

    // http://physicist3d.blogspot.com/2013/12/unity-tutorial-how-to-make-one-way.html
}

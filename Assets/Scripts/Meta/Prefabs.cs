
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Prefabs : MonoBehaviour
{
    public static Prefabs self;
    [NotNull] public GameObject spark, explosion;
    [NotNull] public Bomb bomb;
    public bool triggerSparks = false;

    void Awake() => self = this;
}

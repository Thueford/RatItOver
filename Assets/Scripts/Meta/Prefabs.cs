
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Prefabs : MonoBehaviour
{
    public static Prefabs self;
    [NotNull] public GameObject spark;
    public bool triggerSparks = false;

    void Awake() => self = this;

    private void Update() {
    }
}

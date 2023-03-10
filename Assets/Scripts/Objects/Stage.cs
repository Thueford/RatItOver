using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage : MonoBehaviour
{
    public GameObject spawn;
    public static Stage current;

    private void Awake() => current = this;
}

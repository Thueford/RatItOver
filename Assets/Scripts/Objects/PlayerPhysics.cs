using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Helper;

[RequireComponent(typeof(InputController))]
public class PlayerPhysics : MonoBehaviour
{

    public static Player player { get; private set; }
    Rigidbody rb;
    Vector2 moveDir;

    void Awake()
    {
        player = Player.player;
        rb = GetComponentInChildren<Rigidbody>();
    }

    void Start()
    {
        transform.position = Stage.current.transform.position;

        player.inputController.dirAction.performed += ctx => {
            moveDir = player.inputController.dirAction.ReadValue<Vector2>();
            moveDir = new Vector3(moveDir.x, 0, moveDir.y);
        };
    }

    void Update()
    {
        rb.velocity = moveDir;
    }
}

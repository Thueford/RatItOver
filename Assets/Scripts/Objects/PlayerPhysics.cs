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

        /* player.inputController.dirAction.started += ctx =>
        {
            player.Log("started");
            moveDir = player.inputController.dirAction.ReadValue<Vector2>();
        };
        player.inputController.dirAction.canceled += ctx =>
        {
            player.Log("stopped");
            moveDir = Vector2.zero;
        };
        player.inputController.dirAction.performed += ctx =>
        {
            player.Log($"perform {ctx.canceled} {ctx.performed} {ctx.started}");
            moveDir = Vector2.zero;
        }; */
    }

    void Update()
    {
        rb.velocity = moveDir;
    }
}

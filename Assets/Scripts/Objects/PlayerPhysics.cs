using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static Helper;

[RequireComponent(typeof(InputController))]
public class PlayerPhysics : MonoBehaviour
{

    public static Player player => Player.player;
    public Rigidbody rb { get; private set; }

    Vector2 moveDir, bombDir;
    bool grounded = true;
    float jumpYSpeed;
    Vector3 pos => transform.position;
    Vector3 vel;

    void Awake()
    {
        rb = GetComponentInChildren<Rigidbody>();
    }

    void Start()
    {
        transform.position = Stage.current.transform.position;
        jumpYSpeed = Mathf.Sqrt(-2f * Physics.gravity.y * Player.player.jumpHeight);

        player.inputController.moveAction.performed += ctx =>
        {
            moveDir = ctx.ReadValue<Vector2>();
            if (moveDir.y > 0 && grounded) Jump();
        };

        player.inputController.dirAction.performed += ctx =>
        {
            bombDir = ctx.ReadValue<Vector2>();
        };
    }

    void Jump()
    {
        grounded = false;
        vel.y = jumpYSpeed;
        rb.velocity = vel;
    }

    void Update()
    {
        vel = rb.velocity;

        if (moveDir.x != 0) vel.x = player.speed * moveDir.x;

        rb.velocity = vel;
        rb.AddForce(-vel.x, 0, 0);

        if (grounded)
        {
            if (!player.inputController.dirAction.IsPressed())
            {
                Vector2 pos = CamController.cam.WorldToScreenPoint(player.pos);
                Vector2 dir = Mouse.current.position.ReadValue() - pos;
                bombDir = dir.normalized;
            }
            ShowBombDir();
        }
    }

    void ShowBombDir()
    {
        Debug.DrawRay(pos, 2 * (Vector3)bombDir, Color.red);
        Debug.DrawRay(pos, 2 * (Vector3)moveDir, Color.green);
    }

    void OnCollisionEnter(Collision c)
    {
        if (c.gameObject.CompareTag("Wall"))
        {
            Vector3 n = Vector3.zero;
            for (int i = 0; i < c.contactCount; i++)
                n += c.GetContact(i).normal;

            if (Vector3.Angle(Vector3.up, n) < 45)
            {
                grounded = true;
                Debug.Log("Grounded 1");
            }
        }
    }
}

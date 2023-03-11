using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Helper;

[RequireComponent(typeof(InputController), typeof(PlayerPhysics))]
public class Player : MonoBehaviour
{

    public static Player player { get; private set; }

    // internal objects
    internal InputController inputController;

    [ReadOnly] public bool grounded; // = !airborne
    public float speed = 4, jumpHeight = 6;

    public Action OnRoundReset = () => { };
    public PlayerPhysics physics;
    public Collider coll;
    public Vector3 pos => transform.position;

    //public Action OnMatchReset = () => { };

    private void RoundReset()
    {
        grounded = true;
        OnRoundReset();
    }

    #region Unity

    private void Reset()
    {
        RoundReset();
    }

    private void Awake()
    {
        inputController = GetComponent<InputController>();
        physics = GetComponent<PlayerPhysics>();
        coll = GetComponentInChildren<Collider>();

        // jumpYSpeed = Mathf.Sqrt(2f * jumpGravity * jumpHeight);
        player = this;
    }

    private void Start()
    {
        GameController.self.OnReset += Reset;
    }

    void Update()
    {
        // if (Pause.pause == anim.enabled) anim.enabled = !Pause.pause;
        if (GameController.self.isHitlag || GameController.freeze) return;
    }
    #endregion

    #region Actions

    public void Landed()
    {
        Log("Grounded");
        grounded = true;
        // UpdateState(MovementState.NEUTRAL, actionState, jumpState);
    }

    public void Die()
    {
        Log("Died. Restarting in 3 seconds");
        // SoundHandler.PlayClip("death");
    }

    #endregion

    #region Animation

    #endregion

    public void Log(params object[] args)
    {
        string log = name + ":";
        foreach (var arg in args)
            log += " " + arg.ToString();
        Debug.Log(log);
    }
}

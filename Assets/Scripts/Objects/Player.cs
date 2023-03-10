using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Helper;

[RequireComponent(typeof(InputController))]
public class Player : MonoBehaviour
{

    public static Player player { get; private set; }


    // inspector values
    [NotNull] public int speed;
    [NotNull] public int jumpHeight;

    internal int jumpGravity = 50, dashGravity = 10;
    private float jumpYSpeed;

    // internal objects
    internal InputController inputController;

    [ReadOnly] public bool grounded; // = !airborne

    public Action OnRoundReset = () => { };
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

        jumpYSpeed = Mathf.Sqrt(2f * jumpGravity * jumpHeight);
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

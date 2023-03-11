using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static Helper;

[RequireComponent(typeof(InputController), typeof(PlayerPhysics))]
public class Player : MonoBehaviour
{

    public static Player player { get; private set; }

    // internal objects
    internal InputController inputController;
    public GameObject holdBomb;
    public GameObject rat;

    [ReadOnly] public bool grounded; // = !airborne
    public float speed = 4, jumpHeight = 6;

    internal Action OnRoundReset = () => { };
    internal PlayerPhysics physics;
    internal Collider coll;
    internal Vector3 pos => transform.position;
    Vector2 bombDir;

    // for checkpoints
    private Vector3 respawnPoint;
    public GameObject fallDetector;
    public List<GameObject> checkpoints = new List<GameObject>();

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

        inputController.dirAction.performed += ctx =>
            bombDir = ctx.ReadValue<Vector2>();
        inputController.placeAction.performed += ctx => ThrowBomb();

        // for checkpoints
        respawnPoint = transform.position;
        checkpoints = new List<GameObject>(GameObject.FindGameObjectsWithTag("Checkpoint"));
    }

    void Update()
    {
        if (grounded)
        {
            if (!player.inputController.dirAction.IsPressed())
            {
                Vector2 pos = CamController.cam.WorldToScreenPoint(player.pos);
                Vector2 dir = Mouse.current.position.ReadValue() - pos;
                bombDir = dir.normalized;
                ShowBomb();
            }
        }

        // if (Pause.pause == anim.enabled) anim.enabled = !Pause.pause;
        if (GameController.self.isHitlag || GameController.freeze) return;
    }
    #endregion

    void ShowBomb()
    {
        Debug.DrawRay(pos, 2 * (Vector3)bombDir, Color.red);
        float ang = -Vector2.SignedAngle(bombDir, Vector2.down);
        rat.transform.rotation = Quaternion.Euler(0, 0, ang);
        // holdBomb.transform.position = pos + 0.5f * (Vector3)bombDir;
    }

    void ThrowBomb()
    {
        Bomb bomb = Instantiate(Prefabs.self.bomb, pos + 0.5f * (Vector3)bombDir, Quaternion.identity);
        bomb.dir = bombDir;
    }

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

    void OnTriggerEnter(Collider collision) 
    {
        Debug.Log("Collision");
        if (collision.CompareTag("FallDetector")) {
            Debug.Log("Collision: FallDetector");
            transform.position = respawnPoint;
        }
        else if (collision.CompareTag("Checkpoint")) {
            Checkpoint checkpoint = collision.GetComponent<Checkpoint>();
            if (!checkpoint.isActive()) {
                int currentId = checkpoint.id;
                foreach (GameObject cp in checkpoints) {
                    if (cp.GetComponent<Checkpoint>().id < currentId) cp.GetComponent<Checkpoint>().activateCheckpoint();
                }
                checkpoint.activateCheckpoint();
                respawnPoint = collision.transform.position;
            }
        }
    }
}

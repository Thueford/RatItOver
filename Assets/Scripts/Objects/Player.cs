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
    [NotNull] public SpriteRenderer rat;
    [NotNull] public GameObject rat_b;

    [ReadOnly] public bool grounded; // = !airborne
    public float speed = 4, jumpHeight = 6;
    [Range(0,2)] public float bombTimeout = 0.5f;

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

        // for checkpoints
        respawnPoint = transform.position;
        checkpoints = new List<GameObject>(GameObject.FindGameObjectsWithTag("Checkpoint"));
        Debug.Log("Get all Checkpoint in Lvl");

        inputController.dirAction.performed += ctx =>
            bombDir = ctx.ReadValue<Vector2>();
        inputController.placeAction.performed += ctx => ThrowBomb();
    }

    void LateUpdate()
    {
        if (Bomb.tLastExpl > 0 && Time.time - Bomb.tLastExpl >= bombTimeout) holdBomb.SetActive(true);
        if (grounded)
        {
            if (!player.inputController.dirAction.IsPressed())
            {
                Vector2 pos = CamController.cam.WorldToScreenPoint(rat.transform.position);
                Vector2 dir = Mouse.current.position.ReadValue() - pos;
                bombDir = dir.normalized;
                ShowBomb();
            }
        }

        // if (Pause.pause == anim.enabled) anim.enabled = !Pause.pause;
        setFallDetectorPos();
        if (GameController.self.isHitlag || GameController.freeze) return;
    }
    #endregion

    void ShowBomb()
    {
        Debug.DrawRay(pos, 2 * (Vector3)bombDir, Color.red);
        float ang = -Vector2.SignedAngle(bombDir, Vector2.down);
        rat.transform.rotation = Quaternion.Euler(0, 0, ang);
        // holdBomb.transform.position = pos + 0.5f * (Vector3)bombDir;

        rat_b.transform.rotation = rat.transform.rotation;
        float dot = Vector3.Dot(
            transform.rotation * Vector3.up,
            rat.transform.rotation * Vector3.up);
        bool front = dot > -0.2;

        rat.enabled = front;
        rat_b.transform.parent.gameObject.SetActive(!front);
    }

    void ThrowBomb()
    {
        if (!holdBomb.activeSelf) return;
        holdBomb.SetActive(false);
        Bomb bomb = Instantiate(Prefabs.self.bomb, rat.transform.position + 0.5f * (Vector3)bombDir, Quaternion.identity);
        bomb.dir = bombDir - (Vector2)physics.rb.velocity / Physics.gravity.y;
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
        Debug.Log("Collision " + collision.name);
        if (collision.CompareTag("FallDetector")) Respawn();
        else if (collision.CompareTag("Checkpoint"))
        {
            Checkpoint checkpoint = collision.GetComponent<Checkpoint>();
            if (!checkpoint.isActive())
            {
                int currentId = checkpoint.id;
                foreach (GameObject cp in checkpoints)
                {
                    if (cp.GetComponent<Checkpoint>().id <= currentId)
                        cp.GetComponent<Checkpoint>().activateCheckpoint();
                }
                fallDetector.transform.position = new Vector2(transform.position.x, checkpoint.transform.position.y - 10);
                respawnPoint = collision.transform.position;
            }
        }
    }

    public void Respawn() => transform.position = respawnPoint;

    public void RespawnAtLevelStart()
    {
        foreach (GameObject cp in checkpoints)
        {
            cp.GetComponent<Checkpoint>().deactivateCheckpoint();
        }
        fallDetector.transform.position = new Vector2(transform.position.x, Stage.current.spawn.transform.position.y - 10);
        transform.position = Stage.current.spawn.transform.position;
    }

    public void setEnabelationOfAllCheckpoints(bool enable = true)
    {
        foreach (GameObject cp in checkpoints)
        {
            cp.GetComponent<Checkpoint>().SetActive(enable);
        }
    }

    private void setFallDetectorPos()
    {
        float currPosDetector_Y = fallDetector.transform.position.y;
        fallDetector.transform.position = new Vector2(transform.position.x, currPosDetector_Y);
    }
}

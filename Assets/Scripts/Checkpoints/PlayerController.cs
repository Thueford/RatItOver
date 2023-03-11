using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody player;
    private Vector3 respawnPoint;
    public GameObject fallDetector;
    public List<GameObject> checkpoints = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        respawnPoint = transform.position;
        Debug.Log("RespawnPoint: " + respawnPoint.x + "," + respawnPoint.y + "," + respawnPoint.z);
        checkpoints = new List<GameObject>(GameObject.FindGameObjectsWithTag("Checkpoint"));
    }

    // Update is called once per frame
    void Update()
    {
        //fallDetector.transform.position = new Vector2(transform.position.x, transform.position.y -30);
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

    public void setRespawnPoint(Vector3 pos) {
        respawnPoint = pos;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceFields : MonoBehaviour
{

    public float power = 1;
    public Vector3 size = new Vector3(1, 1, 1);
    public float windspeed = 5;
    public Vector3 direction = new Vector3(0,0,0);

    private Collider collider;
    private ParticleSystem ps;
    private float mz;

    // Start is called before the first frame update
    void Start()
    {
        collider = GetComponent<Collider>();
        collider.isTrigger = true;
        ps = GetComponent<ParticleSystem>();
        ps.Play();
        transform.localScale = size;
        ps.transform.localScale = size;
        var m = ps.main;
        m.startSpeed = windspeed;
        m.startLifetime = transform.localScale.x / m.startSpeed.constant;
    }

    // Update is called once per frame
    void Update()
    {
        transform.localScale = size;
        ps.transform.localScale = size;
        var m = ps.main;
        m.startSpeed = windspeed;
        m.startLifetime = transform.localScale.x / m.startSpeed.constant;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Rigidbody>())
        {
            other.GetComponent<Rigidbody>().AddForce(direction * power);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Rigidbody>())
        {
            other.GetComponent<Rigidbody>().AddForce(direction * power * -1);
        }
    }
}

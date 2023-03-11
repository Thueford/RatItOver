using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneWayPlattform : MonoBehaviour
{

    public int up = 1;
    public int right = 0;

    private Collider collider;

    // Start is called before the first frame update
    void Start()
    {
        collider = GetComponent<Collider>();
        collider.isTrigger = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.position.y > up * transform.position.y)
        {
            UnityEngine.Debug.Log("LULULULUL");
            collider.isTrigger = false;
        } 
        else
        {
            UnityEngine.Debug.Log("aaaaaaa");
            collider.isTrigger = true;
        }
    }
}

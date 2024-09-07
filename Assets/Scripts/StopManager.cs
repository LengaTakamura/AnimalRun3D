using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopManager : MonoBehaviour
{
    GameObject obj;
    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
         obj = transform.parent.gameObject;

        rb = obj.GetComponent<Rigidbody>();
    }

    // Update is calleasd once per frame
    private void OnTriggerEnter(Collider other)
    {       
        Vector3 vect = (obj.transform.position - other.transform.position).normalized;
        rb.velocity = (vect + (Vector3.up * rb.velocity.y));
       
    }



}

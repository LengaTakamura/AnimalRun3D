using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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

   
    private void OnTriggerEnter(Collider other)
    {       
        Vector3 vect = (obj.transform.position - other.transform.position).normalized;
        rb.velocity = new Vector3(vect.x , rb.velocity.y , vect.z);             
    }
    

}

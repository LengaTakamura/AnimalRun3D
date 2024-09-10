using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class StopManager : MonoBehaviour
{
    GameObject obj;
    Rigidbody rb;
    public  bool isStop; 
    // Start is called before the first frame update
    void Start()
    {
         obj = transform.parent.gameObject;

        rb = obj.GetComponent<Rigidbody>();
    }

   
    private void OnTriggerEnter(Collider other)
    {       
        rb.velocity = new Vector3 (0,rb.velocity.y,0);
        
        isStop = true;
       

        
    }
    private void OnTriggerExit(Collider other)
    {
        isStop = false;
    }


}

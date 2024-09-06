using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopManager : MonoBehaviour
{
   
    // Start is called before the first frame update
    void Start()
    {
        GameObject obj = transform.parent.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("collision.gameObject.tag");
        Debug.Log(collision.gameObject.tag);
    }

    
}

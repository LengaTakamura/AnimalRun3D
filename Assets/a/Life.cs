using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Life : MonoBehaviour
{
   MouseController mouseController;
    float time = 0f;
    
    // Start is called before the first frame update
    void Awake()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if (time > 3f)
        {
            Destroy(gameObject);
        }
       
        
    }
}

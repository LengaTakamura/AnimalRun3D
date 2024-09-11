using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitch : MonoBehaviour
{
    [SerializeField] Camera main;
    [SerializeField] Camera sub;
    public bool mainActive = true;
    // Start is called before the first frame update
    void Start()
    {
        sub .enabled = false;    
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            if (mainActive)
            {
                main.enabled = false;
                mainActive = false;
                sub.enabled = true;
            }
            else 
            {
                main.enabled = true;
                mainActive = true;
                sub.enabled = false;
            }

        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Horse : ItemBase
{
    [SerializeField]FPSController fpsController;
    public override void Activate()
    {
        fpsController.speed = 0.3f;
        Debug.Log("sppedchange");
    }

   
}



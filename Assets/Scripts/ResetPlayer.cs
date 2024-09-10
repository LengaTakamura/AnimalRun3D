using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetPlayer : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            ResetPlayerRotating();
        }
    }


    public void ResetPlayerRotating()
    {
       transform.rotation = Quaternion.identity;
    }
}

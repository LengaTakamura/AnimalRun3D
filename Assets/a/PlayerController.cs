
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController1:SuperStateMachine1
{

    public enum Playercurrent
    {
        Idle,
        jump
    }
   


    private void Start()
    {
        currentState = Playercurrent.Idle;
        currentState = Playercurrent.jump;
    }

    void Idle_EnterState()
    {
        Debug.Log("Idle��ԂɂȂ�܂���");
    }
    void Idle_ExitState()
    {
        Debug.Log("Idle���nukeru");
    }
    void jump_EnterState()
    {
        Debug.Log("jump");

    }
}

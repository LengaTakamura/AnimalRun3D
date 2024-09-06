using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{

    // Start is called before the first frame update
    public float speed = 10F;
    public float jumpSpeed = 50.0F;
    public float gravity = 20.0F;
    Rigidbody rb;
    private Vector3 moveDirection;
    FPSController fpsControllerA;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        fpsControllerA = GetComponent<FPSController>();
    }

    void Update()
    {
        //地面についている時
        if (fpsControllerA.isJumping == false)
        {
            moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            //Transform.TransformDirectionはローカル空間からワールド空間へ方向Vectorを変換する
            moveDirection = transform.TransformDirection(moveDirection) * speed;
            //ジャンプ↓
            if (Input.GetKeyDown(KeyCode.Space)&&fpsControllerA.animal ==FPSController.Animal.horse )
            {
                moveDirection.y = jumpSpeed;
                Debug.Log("jumping");
            }
        }
        //重力分変更する
    //    moveDirection.y -= gravity * Time.deltaTime;
    //    rb.AddForce(moveDirection*Time.deltaTime,ForceMode.Acceleration);
    }
}

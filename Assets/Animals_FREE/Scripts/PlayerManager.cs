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
        //�n�ʂɂ��Ă��鎞
        if (fpsControllerA.isJumping == false)
        {
            moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            //Transform.TransformDirection�̓��[�J����Ԃ��烏�[���h��Ԃ֕���Vector��ϊ�����
            moveDirection = transform.TransformDirection(moveDirection) * speed;
            //�W�����v��
            if (Input.GetKeyDown(KeyCode.Space)&&fpsControllerA.animal ==FPSController.Animal.horse )
            {
                moveDirection.y = jumpSpeed;
                Debug.Log("jumping");
            }
        }
        //�d�͕��ύX����
    //    moveDirection.y -= gravity * Time.deltaTime;
    //    rb.AddForce(moveDirection*Time.deltaTime,ForceMode.Acceleration);
    }
}

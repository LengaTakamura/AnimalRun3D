using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float speed = 10F;
    public float jumpSpeed = 8.0F;
    public float gravity = 20.0F;
    private CharacterController controller;
    private Vector3 moveDirection;
    Animator m_anim;
    bool _isGround;

    

    void Start()
    {
        controller = GetComponent<CharacterController>();
        m_anim = GetComponent<Animator>();
    }

    void Update()
    {
        LookAhead();
        //Vector3 defalut = transform.position - lastPos;
        //lastPos = transform.position;
        //地面についている時
        if (controller.isGrounded)
        {
            moveDirection = new Vector3(0, 0, Input.GetAxis("Vertical"));
            //Transform.TransformDirectionはローカル空間からワールド空間へ方向Vectorを変換する
            moveDirection = transform.TransformDirection(moveDirection) * speed;
            //ジャンプ↓
            if (Input.GetButton("Jump"))
            {
                moveDirection.y = jumpSpeed;
            }
        }
        //重力分変更する
        moveDirection.y -= gravity * Time.deltaTime;
        controller.Move(moveDirection * Time.deltaTime);
        
       
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (controller.tag =="Ground") 
        {
            _isGround = true;
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (controller.tag == "Ground")
        {
            _isGround = false;
        }
    }
    private void LateUpdate()
    {
        if(m_anim)
        {
            m_anim.SetFloat("SpeedX", Mathf.Abs(moveDirection.x));
            m_anim.SetFloat("SpeedY", moveDirection.y);
            m_anim.SetBool("isGround", _isGround);
            m_anim.SetFloat("SpeedZ", Mathf.Abs(moveDirection.z));

        }
        
    }

    void LookAhead()
    {

        if(moveDirection.magnitude == 0)
        {
            return;
        }
        else if (moveDirection.magnitude > 0.1f) 
        {
            transform.rotation = Quaternion.LookRotation(moveDirection);
        }
    }
}

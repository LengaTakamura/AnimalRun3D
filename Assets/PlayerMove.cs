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
    float forward = 0f;
    Vector3 lastPos;
    Vector3 defalut;
    public float beside = 10f;
    public float backspeed;
    void Start()
    {
        controller = GetComponent<CharacterController>();
        m_anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (controller.isGrounded && Input.GetKey(KeyCode.W))
        {
            forward = 1f;
            moveDirection = new Vector3(0, 0, forward);
            Vector3 velocity = this.transform.rotation * new Vector3(0, 0, forward * speed);
            moveDirection = new Vector3(velocity.x, moveDirection.y, velocity.z);
            Debug.Log("go ahead");
            //�W�����v��
        }
        else if (Input.GetKey(KeyCode.S))
        {
            forward = -1f;
            moveDirection = new Vector3(0, 0, forward);
            Vector3 velocity = this.transform.rotation * new Vector3(0, 0, forward * backspeed);
            moveDirection = new Vector3(velocity.x, moveDirection.y, velocity.z);
            Debug.Log("back");
        }

        if (Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.A))
        {
            // �I�u�W�F�N�g�̉�]
            this.transform.Rotate(Vector3.up, beside);
        }
        //���L�[��������Ă��ā��L�[��������Ă��Ȃ���
        else if (!Input.GetKey(KeyCode.D) && Input.GetKey(KeyCode.A))
        {
            // �I�u�W�F�N�g�̉�]
            this.transform.Rotate(Vector3.up, -1 * beside);
        }


        if (controller.isGrounded && Input.GetButton("Jump"))
        {
            Debug.Log("Jump");
            moveDirection.y = jumpSpeed;
        }
        //LookAhead();

        //moveDirection.y -= gravity * Time.deltaTime;
        moveDirection.y -= gravity * Time.deltaTime;
        controller.Move(moveDirection * Time.deltaTime);

    }
    private void OnCollisionEnter(Collision collision)
    {
        if (controller.tag == "Ground")
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
    private void FixedUpdate()
    {
        if (m_anim)
        {
            m_anim.SetFloat("SpeedX", Mathf.Abs(moveDirection.x));
            m_anim.SetFloat("SpeedY", moveDirection.y);
            m_anim.SetBool("isGround", _isGround);
            m_anim.SetFloat("SpeedZ", Mathf.Abs(moveDirection.z));

        }

    }
}

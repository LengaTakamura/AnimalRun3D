using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float speed = 30F;
    public float jumpSpeed = 8.0F;

    private Vector3 moveDirection;
    Animator m_anim;
    public bool _isGround;
    float forward = 0f;
    Vector3 lastPos;
    Vector3 defalut;
    Rigidbody rb;
    public float beside = 10f;
    public float backspeed;
    public bool _running;
    public float _acceleration = 0.05f;
    void Start()
    {
        //controller = GetComponent<CharacterController>();
        m_anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        _isGround = true;
    }

    void Update()
    {
        if (rb.velocity.x == 0f && rb.velocity.z == 0f)
        {
            ResetSpeed();
            forward = 0f;
            _running = false;
        }
        if (Input.GetKey(KeyCode.W))
        {
            _acceleration = 0.05f;
            _running = false;
            forward = 1f;
            moveDirection = new Vector3(0, 0, forward);
            Vector3 velocity = this.transform.rotation * new Vector3(0, 0, forward);
            moveDirection = new Vector3(velocity.x, moveDirection.y, velocity.z);
            rb.AddForce(moveDirection * speed);
            Debug.Log("go ahead");
            speed += _acceleration;
            if (speed >= 55f)
            {
                Running();

            }

            if (speed >= 60f)
            {
                speed = 60f;
                _acceleration = 0f;
            }

        }
        else if (Input.GetKey(KeyCode.S))
        {
            forward = -1f;
            moveDirection = new Vector3(0, 0, forward);
            Vector3 velocity = this.transform.rotation * new Vector3(0, 0, forward);
            moveDirection = new Vector3(velocity.x, moveDirection.y, velocity.z);
            rb.AddForce(moveDirection * backspeed);
            //controller.Move(moveDirection * Time.deltaTime);
            Debug.Log("back");
        }

        if (Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.A) && (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.W)))
        {
            beside = 0.15f;
            // オブジェクトの回転
            if (Input.GetKey(KeyCode.S))
            {
                beside = 0.08f;

            }
            this.transform.Rotate(Vector3.up, beside);
        }
        //←キーが押されていて→キーが押されていない時
        else if (!Input.GetKey(KeyCode.D) && Input.GetKey(KeyCode.A) && (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.W)))
        {
            // オブジェクトの回転
            beside = 0.15f;
            if (Input.GetKey(KeyCode.S))
            {
                beside = 0.08f;

            }
            this.transform.Rotate(Vector3.up, -1 * beside);
        }


        if (_isGround && Input.GetButton("Jump"))
        {
            Debug.Log("Jump");
            rb.velocity = new Vector3(0, jumpSpeed, 0);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            _isGround = true;
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
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
            m_anim.SetFloat("SpeedZ", Mathf.Abs(moveDirection.z));
            m_anim.SetFloat("Moving", forward);
            m_anim.SetBool("Running", _running);
        }

    }
    void ResetSpeed()
    {
        if (_running == false)
        {
            speed = 30f;
        }
    }

    void Running()
    {
        _running = true;
    }
}

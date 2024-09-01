using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
    Vector3 velocity;
    public HorseState horseState;
    [SerializeField]AudioClip _audioClip;
    AudioSource _audioSource;
    float time;
    bool _isPlaying;
    float _speed;
    public float intertia;
    Vector3 objforward;
    public float GroundedOffset = -0.14f;
    public float GroundedRadius = 0.5f;
    public LayerMask GroundLayers;
    void Start()
    {    
        m_anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        _isGround = true;
        _audioSource = GetComponent<AudioSource>(); 
        horseState = HorseState.Idol;
        _isPlaying = true;
    }

    void Update()
    {
 
        objforward = transform.forward;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        GroundedCheck();
        Idol();
        Moving();
        Rotating();
        Jumpimg();
        if (!_isGround)
        {
            horseState = HorseState.Jumping;
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
            m_anim.SetBool("IsGround", _isGround);
        }


    }
    void Idol()
    {

        if ( rb.velocity.magnitude == 0f )
        {
            forward = 0f;
            horseState = HorseState.Idol;
        }
        else if (!Input.GetKey(KeyCode.S) && _isGround)
        {
            horseState = HorseState.Walking;
        }
    }
        
    
    
    void Moving()
    {

        

        if (Input.GetKey(KeyCode.W) && _isGround && !Input.GetKey(KeyCode.Space))
        {


            horseState = HorseState.Walking;
            _acceleration = 0.1f;
            _running = false;
            forward = 1f;
            moveDirection = new Vector3(0, 0, forward);
            velocity = this.transform.rotation * new Vector3(0, 0, forward);
            moveDirection = new Vector3(velocity.x, moveDirection.y, velocity.z);
           // rb.AddForce(moveDirection * speed);
            rb.velocity = moveDirection * speed * 0.1f;
            speed += _acceleration;
            


            if (speed >= 55f)
            {
                _running = true;
                horseState = HorseState.Running;
            }
            else
            {
                horseState = HorseState.Walking;

            }

            if (speed >= 60f)
            {
                speed = 60f;
                _acceleration = 0f;
            }

        }
        else if( speed == 60f && Input.GetKeyUp(KeyCode.W))    
        {
            _running = false ;
            ResetSpeed();

        }



        if (Input.GetKey(KeyCode.S))
        {
            if (_isGround)
            {
                horseState = HorseState.Back;
            }

            ResetSpeed();
            _running = false;   
            forward = -1f;
            moveDirection = new Vector3(0, 0, forward);
            Vector3 velocity = this.transform.rotation * new Vector3(0, 0, forward);
            moveDirection = new Vector3(velocity.x, moveDirection.y, velocity.z);
            rb.AddForce(moveDirection * backspeed);
        }

    }

    void Rotating()
    {
        if (Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.A) && (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.W)))
        {
            //beside = 0.15f;
            // オブジェクトの回転
            if (Input.GetKey(KeyCode.S))
            {
                beside = 0.05f;

            }
            this.transform.Rotate(Vector3.up, beside);
        }
        //←キーが押されていて→キーが押されていない時
        else if (!Input.GetKey(KeyCode.D) && Input.GetKey(KeyCode.A) && (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.W)))
        {
            // オブジェクトの回転
            //beside = 0.15f;
            if (Input.GetKey(KeyCode.S))
            {
                beside = 0.05f;

            }
            this.transform.Rotate(Vector3.up, -1 * beside);
        }
    }
    void Jumpimg()
    {
        if (_isGround && Input.GetButtonDown("Jump"))
        {
           
            rb.AddForce( new Vector3 (objforward.x * intertia ,1 * jumpSpeed   , objforward.z * intertia),ForceMode.Impulse);
            Debug.Log(1 * jumpSpeed);
            if (_isPlaying)
            {
                _audioSource.PlayOneShot(_audioClip);
            }
        }
    }

    void ResetSpeed()
    {
        if (_running == false)
        {
            speed = 30f;
        }
    }

    public void GroundedCheck()
    {
        // オフセットを計算して球の位置を設定する
        Vector3 spherePosition = new Vector3(transform.position.x, transform.position.y - GroundedOffset, transform.position.z);

        if (Physics.CheckSphere(spherePosition, GroundedRadius, GroundLayers, QueryTriggerInteraction.Ignore))
        {
            _isGround = true;
        }
           
    }

    public enum HorseState
    {
        Running,Idol,Walking,Back,Jumping,
    }
    
}

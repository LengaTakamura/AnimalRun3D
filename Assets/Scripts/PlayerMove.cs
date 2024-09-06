using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.ProBuilder;

public class PlayerMove : MonoBehaviour
{
    public float speed = 30F;
    public float jumpSpeed = 8.0F;

    private Vector3 moveDirection;
    Animator m_anim;
    public bool _isGround;
    float forward = 0f;
    Rigidbody rb;
    public float beside = 10f;
    public float backspeed;
    public bool _running;
    public float _acceleration = 0.05f;
    Vector3 _vect;
    public HorseState horseState;
    [SerializeField] AudioClip _audioClip;
    AudioSource _audioSource;
    bool _isPlaying;
    public float intertia;
    Vector3 objforward;
    public float GroundedOffset = -0.14f;
    public float GroundedRadius = 0.5f;
    public LayerMask GroundLayers;
    public float gravity = -9.81f;
    public float groundDistance = 0.4f; // 接地判定の半径
    public bool isGameover;
    public float rotationSpeed = 5.0f;
    RaycastHit hit;
    void Start()
    {
        m_anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        _audioSource = GetComponent<AudioSource>();
        horseState = HorseState.Idol;
        _isPlaying = true;
        GroundLayers = LayerMask.GetMask("Ground");

    }

    void Update()
    {
        GroundedCheck();
        objforward = transform.forward;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Idol();
        Moving();
        Rotating();
        Jumpimg();
       
        if (!_isGround)
        {
            horseState = HorseState.Jumping;
        }

    }
    private void FixedUpdate()
    {

       
        Horizontal();
        if (Input.GetKey(KeyCode.W) && _isGround && !Input.GetKey(KeyCode.Space))
        {
            _acceleration = 0.1f;
            SpeedUp();

        }

        if (m_anim)
        {
            m_anim.SetFloat("SpeedX", Mathf.Abs(moveDirection.x));
            m_anim.SetFloat("SpeedY", moveDirection.y);
            m_anim.SetFloat("SpeedZ", Mathf.Abs(moveDirection.z));
            m_anim.SetFloat("Moving", forward);
            m_anim.SetBool("Running", _running);
            m_anim.SetBool("IsGround", _isGround);
            m_anim.SetBool("Running2", horseState == HorseState.Running);
            m_anim.SetBool("Walking", horseState == HorseState.Walking);
            m_anim.SetBool("Idol", horseState == HorseState.Idol);
            m_anim.SetBool("Back", horseState == HorseState.Back);
            m_anim.SetBool("Jumping", horseState == HorseState.Jumping);
        }

    }
    void Idol()
    {
        
        if (rb.velocity.x < 0.1f && rb.velocity.z < 0.1f && _isGround)
        {
            forward = 0f;
            horseState = HorseState.Idol;
        }

        if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.S))
        {
            horseState = HorseState.Idol;
        }
    }
    void Moving()
    {
        if (Input.GetKey(KeyCode.W) && _isGround && !Input.GetKey(KeyCode.Space))
        {


            horseState = HorseState.Walking;
            _running = false;
            forward = 1f;
            moveDirection = new Vector3(0, 0, forward);
            _vect = this.transform.rotation * new Vector3(0, 0, forward);
            moveDirection = new Vector3(_vect.x, 0, _vect.z);
            rb.velocity = moveDirection * speed * 0.1f + (Vector3.up * rb.velocity.y);

            if (speed >= 40f)
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
        else if (speed >= 55f && Input.GetKeyUp(KeyCode.W))
        {
            horseState = HorseState.Idol;
            _running = false;
            ResetSpeed();

        }

        if (Input.GetKey(KeyCode.S) && _isGround)
        {
            horseState = HorseState.Back;
            ResetSpeed();
            _running = false;
            forward = -1f;
            moveDirection = new Vector3(0, 0, forward);
            Vector3 vec = this.transform.rotation * new Vector3(0, 0, forward);
            moveDirection = new Vector3(vec.x, 0, vec.z);
            rb.velocity = (moveDirection * backspeed) + (Vector3.up * rb.velocity.y);

        }

    }

    void Rotating()
    {
        if (Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.A) )
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
        else if (!Input.GetKey(KeyCode.D) && Input.GetKey(KeyCode.A) )
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

           rb.AddForce(new Vector3(objforward.x * intertia, 1 * jumpSpeed, objforward.z * intertia), ForceMode.Impulse);
            if (_isPlaying)
            {
                _audioSource.PlayOneShot(_audioClip);
            }

            
        }

        if(horseState == HorseState.Jumping && Input.GetKeyDown(KeyCode.S))
        {
            intertia = 0;
            horseState = HorseState.Back;
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
        bool sphereHit = Physics.CheckSphere(spherePosition, GroundedRadius, GroundLayers, QueryTriggerInteraction.Ignore);
        bool rayHit = Physics.Raycast(transform.position, Vector3.down, out hit, groundDistance + 0.1f, GroundLayers);
        
    
        _isGround = sphereHit || rayHit;

        
        
    }
    void SpeedUp()
    {
        speed += _acceleration;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Water")
        {
            isGameover = true;

        }
    }
    public enum HorseState
    {
        Running, Idol, Walking, Back, Jumping,
    }

    void Horizontal()
    {
        float rotatex = transform.eulerAngles.x;
        float rotatez = transform.eulerAngles.z;
       // Debug.Log(rotatez);

        if (_isGround && rotatex < 334)
        {
            if (rotatex > 0)
            {
                //transform.Rotate(Vector3.Scale(Vector3.right, hit.normal), Space.Self);
                //transform.Rotate(Vector3.right);
            }

        }



    }

}

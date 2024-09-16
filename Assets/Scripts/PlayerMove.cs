using DG.Tweening;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMove : MonoBehaviour
{
    public float speed = 30F;
    public float jumpSpeed = 8.0F;
    private Vector3 moveDirection;
    Animator m_anim;
    Animation anim;
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
    public float time = 0f;
    public float rotationSpeed = 5.0f;
    RaycastHit hit;
    public int hitCount;
    public string collisionObj = "none";
    SceneSystem sceneSystem;
    [SerializeField] StopManager stopManager;
    [SerializeField] CameraSwitch cameraSwitch;
    ScoreManager scoreManager;
    [SerializeField] Slider staminaBar;
    float defaSta;
    bool isStaminaCounting;
    public bool staminaAddCounting = true;
    public bool isOk; 
    public float turnPower;
    bool isStaminaCountingRunning;
    [SerializeField] AudioClip itemSound;
    PlayerMove playerMove;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] Pauser pauser;
    private void Awake()
    {
        StartCoroutine(nameof(StartDeray));
    }
    void Start()
    {
        anim = GetComponent<Animation>();
        m_anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        _audioSource = GetComponent<AudioSource>();
        horseState = HorseState.Idol;
        _isPlaying = true;
        GroundLayers = LayerMask.GetMask("Ground");
        scoreManager = GameObject.Find("ScoreManager").GetComponent<ScoreManager>();
        try
        {
            sceneSystem = GameObject.Find("System").GetComponent<SceneSystem>();
        }
        catch { }
        cameraSwitch = GameObject.Find("CameraSystem").GetComponent<CameraSwitch>();

        Idol();

    }
    IEnumerator StartDeray()
    {
        yield return new WaitForSeconds(1.5f);

        isOk = true;
    }


    void Update()
    {
        GroundedCheck();
        objforward = transform.forward;
        //Cursor.lockState = CursorLockMode.Locked;
        //Cursor.visible = false;
        

        defaSta = staminaBar.value;

        SliderOnOff();
        RePlace();
        staminaBar.value = defaSta;
        if (cameraSwitch.mainActive && isOk && !pauser.isPause)
        {
            Moving();

            Rotating();

            Jumpimg();

            if(staminaBar.value <= 150)
            {
                horseState = HorseState.Idol;
            }

        }
        else
        {
            horseState = HorseState.Idol;

        }

        if (!_isGround)
        {
            horseState = HorseState.Jumping;
        }

        if (Input.GetMouseButton(0) && !_isGround && !isOk)
        {
            horseState = HorseState.Walking;
        }

        if(horseState == HorseState.Idol )
        {
            if (staminaAddCounting )
            {
                StartCoroutine(nameof(StaminaAdd));
                Debug.Log("heal");
                staminaAddCounting = false;
            }
        }

        if (!pauser.isPause)
        {
            if (horseState == HorseState.Walking && !isStaminaCounting)
            {
                StartCoroutine(StaminaCount(50f));
                isStaminaCounting = true; // コルーチン実行中のフラグをセット
            }

            if (horseState == HorseState.Running && !isStaminaCountingRunning)
            {
                StartCoroutine(StaminaCountRunning(100f));
                isStaminaCountingRunning = true; // コルーチン実行中のフラグをセット
            }

            // horseState が他の状態に変わった場合はフラグをリセット
            else if ((horseState != HorseState.Walking && horseState != HorseState.Running && horseState != HorseState.Jumping))
            {
                isStaminaCounting = false;
                isStaminaCountingRunning = false;

            }

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
            ResetSpeed();

           
        }

    }
    void Moving()
    {
       if( Input.GetMouseButtonDown(0) && _isGround && !Input.GetKey(KeyCode.Space) && staminaBar.value >= 150)
        {
            horseState = HorseState.Walking;
        }

     
        if (Input.GetMouseButton(0) && _isGround && !Input.GetKey(KeyCode.Space) && staminaBar.value >= 150)
        {
            horseState = HorseState.Walking;

           time += Time.deltaTime;

            _running = false;
            forward = 1f;
            moveDirection = new Vector3(0, 0, forward);
            _vect = this.transform.rotation * new Vector3(0, 0, forward);
            moveDirection = new Vector3(_vect.x, 0, _vect.z);
            rb.velocity = moveDirection * speed * 0.1f + (Vector3.up * rb.velocity.y);

            if(time >= 3f)
            {
                Running();

            }


        }

        if (!Input.GetMouseButton(1)&& !Input.GetMouseButton(0) && _isGround && staminaBar.value >= 150)
        {
            horseState = HorseState.Idol;
            ResetSpeed();
            time = 0f;
            
        }

        if (Input.GetMouseButton(1) && _isGround)
        {
            horseState = HorseState.Back;
            time = 0f;
            _running = false;
            forward = -1f;
            moveDirection = new Vector3(0, 0, forward);
            Vector3 vec = this.transform.rotation * new Vector3(0, 0, forward);
            moveDirection = new Vector3(vec.x, 0, vec.z);
            rb.velocity = (moveDirection * backspeed) + (Vector3.up * rb.velocity.y);

            if (Input.GetMouseButton(0))
            {
                horseState = HorseState.Walking;
            }

        }

    }

    void Running()
    {

        _running = true;
        speed = 60f;
        horseState = HorseState.Running;
    }

   

    void Rotating()
    {

        float mx = Input.GetAxis("Mouse X");


        // X方向に一定量移動していれば横回転
        if (mx > 0.01f)
        {

            this.transform.Rotate(Vector3.up, turnPower);
        }
        else if (mx < -0.01f)
        {
            this.transform.Rotate(Vector3.up, turnPower * -1);

        }

       

    }
    void Jumpimg()
    {
        if (_isGround && Input.GetKeyDown(KeyCode.Space))
        {

            rb.AddForce(new Vector3(objforward.x * intertia, 1 * jumpSpeed, objforward.z * intertia), ForceMode.Impulse);
            if (_isPlaying)
            {
                _audioSource.PlayOneShot(_audioClip);
            }


        }

        if (horseState == HorseState.Jumping && Input.GetMouseButtonDown(2))
        {
            intertia = 0;
            horseState = HorseState.Back;

        }
    }
    void ResetSpeed()
    {
        if (horseState != HorseState.Running)
        {
            speed = 30f;
        }
    }
    public void GroundedCheck()
    {

        // オフセットを計算して球の位置を設定する
        Vector3 spherePosition = new Vector3(transform.position.x, transform.position.y - GroundedOffset, transform.position.z);
        bool sphereHit = Physics.CheckSphere(spherePosition, GroundedRadius, GroundLayers, QueryTriggerInteraction.Ignore);
        bool rayHit = Physics.Raycast(transform.position, Vector3.down, out hit, groundDistance, GroundLayers);

        _isGround = sphereHit;

    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Water")
        {
            if (isOk)
            {
                scoreManager.GameOver();

                sceneSystem.FadeOut();

                anim.Stop();

            }
            else
            {
                Vector3 vect = transform.position;
                transform.position = new Vector3(vect.x, 30f, vect.y);
            }
            
        }

        if(collision.gameObject.tag == "Item")
        {
            _audioSource.PlayOneShot(itemSound);
            ScoreManager.score -= 10f;
            scoreText.color = Color.green;
            StartCoroutine(TextColorChange());

            Destroy(collision.gameObject);

        }

    }

    IEnumerator TextColorChange()
    {
        yield return new WaitForSeconds(0.5f);
        scoreText.color = Color.white;
    }

    IEnumerator StaminaCount(float damage)
    {

        while (horseState == HorseState.Walking  && staminaBar.value >= 150)
        {
            yield return new WaitForSeconds(3f);

            float sta = staminaBar.value;
            staminaBar.DOValue(sta - damage, 1f);

        }
        isStaminaCounting = false;
    }

    IEnumerator StaminaCountRunning(float damage)
    {

        while ( horseState == HorseState.Running  && staminaBar.value >= 150)
        {
            yield return new WaitForSeconds(3f);

            float sta = staminaBar.value;
            staminaBar.DOValue(sta - damage, 1f);

        }
        isStaminaCountingRunning = false;
    }


    IEnumerator StaminaAdd()
    {
        while (horseState == HorseState.Idol && !pauser.isPause)
        {
            yield return new WaitForSeconds(3f);

            float staAdd = staminaBar.value;
            staminaBar.DOValue(staAdd + 100, 1F);

        }
        staminaAddCounting = true;
    }


    void SliderOnOff()
    {
        if (!cameraSwitch.mainActive)
        {
            staminaBar.gameObject.SetActive(false);
        }
        else
        {
            staminaBar.gameObject.SetActive(true);
        }


    }



    void RePlace()
    {
        if (transform.position.y <= 9)
        {
            Vector3 vect = transform.position;
            transform.position = new Vector3(vect.x + 3, 25f, vect.y + 3);
        }
    }

    private void OnPause()
    {
        if (pauser.isPause)
        {
            
        }
    }
    public enum HorseState
    {
        Running, Idol, Walking, Back, Jumping,
    }

}

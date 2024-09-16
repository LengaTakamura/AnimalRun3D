using DG.Tweening;
using JetBrains.Annotations;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;


public class KangarooMove : MonoBehaviour
{
    public bool _isGround;
    Rigidbody rb;
    public float jumpPower;
    Vector3 forward;
    public float intertia;
    public bool canJump;
    public bool falseCheck;
    public float turnPower;
    public float GroundedOffset = -0.14f;
    public float GroundedRadius = 0.5f;
    public LayerMask GroundLayers;
    public float gravity = -9.81f;
    public float groundDistance = 0.4f; // 接地判定の半径
    RaycastHit hit;
    RaycastHit hitt;
    Animator anim;
    CameraSwitch cameraSwitch;
    [SerializeField] Camera subCam;
    float angle;
    public bool _isClear;
    public bool isGameOver;
    SceneSystem sceneSystem;
    ScoreManager scoreManager;
    [SerializeField] AudioClip[] audioClips;
    AudioSource audioSource;
    public bool once;
    [SerializeField] Slider jumpPowerSlider;
    public static float jumpScore;
    [SerializeField] AudioClip itemSound;
    [SerializeField] AudioClip clear;
    // Start is called before the first frame update
    void Start()
    {
        jumpScore = 0;
        once = true;
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        cameraSwitch = GameObject.Find("CameraSystem").GetComponent<CameraSwitch>();
        try
        {
            sceneSystem = GameObject.Find("System").GetComponent<SceneSystem>();
        }
        catch { }
        scoreManager = GameObject.Find("ScoreManager").GetComponent<ScoreManager>();
    }

    // Update is called once per frame
    void Update()
    {

        Slow();
        if (!cameraSwitch.mainActive)
        {
            angle = subCam.transform.eulerAngles.x;
            Rotating();
            JumpUp();
            BackJump();
            jumpPower = jumpPowerSlider.value;
            Debug.Log(rb.velocity.y);
            
        }
        GroundedCheck();

    }
    private void FixedUpdate()
    {
        forward = transform.forward;

        if (anim)
        {
            anim.SetBool("s", canJump && !cameraSwitch.mainActive);
            anim.SetBool("g", _isGround);
            anim.SetBool("Space", Input.GetKey(KeyCode.Space) && !cameraSwitch.mainActive);
        }


    }

    void JumpUp()
    {
        if (Input.GetKeyDown(KeyCode.Space) && _isGround && !scoreManager.isGameOver)
        {
            if (once)
            {
                StartCoroutine(nameof(JumpingPower));

            }
            intertia = 0.5f;
            audioSource.PlayOneShot(audioClips[0]);

        }

        if (Input.GetKeyUp(KeyCode.Space) && _isGround)
        {

            rb.velocity = new Vector3(forward.x * intertia, jumpPower, forward.z * intertia);
            canJump = false;
            audioSource.PlayOneShot(audioClips[1]);

        }

        if(!Input.GetKey(KeyCode.Space) )
        {
            ResetValue();
            
        }
        else
        {
            if (jumpPower >= 9.5f)
            {
                intertia = 5;
            }
        }

    }


    IEnumerator JumpingPower()
    {
        while (Input.GetKey(KeyCode.Space))
        {
            yield return new WaitForSeconds(1f);


            jumpPowerSlider.DOValue(jumpPower + 3, 1F);
        }
        once = false;

    }

    void Rotating()
    {

        float x = Input.GetAxis("Mouse X");


        // X方向に一定量移動していれば横回転
        if (x > 0.01f)
        {

            this.transform.Rotate(Vector3.up, turnPower);
        }
        else if (x < -0.01f)
        {
            this.transform.Rotate(Vector3.up, turnPower * -1);

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

    void BackJump()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            transform.rotation = Quaternion.identity;
            rb.velocity = new Vector3(forward.x * intertia * -0.1f, jumpPower * 0.1f, forward.z * intertia * -0.1f);
        }
    }

    void ResetValue()
    {
        once = true;
        jumpPowerSlider.value = 1;
        //jumpPower = jumpPowerSlider.value;

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {

            _isClear = true;

            audioSource.PlayOneShot(clear);

            scoreManager.Clear();

            scoreManager.ScoreCount();

            StartCoroutine(SceneSystem.ForFadeTime("Result"));
        }

        if (collision.gameObject.tag == "Water")
        {
            

            isGameOver = true;

            sceneSystem.FadeOut();

            scoreManager.GameOver();

            audioSource.Pause();
        
        }

        if (collision.gameObject.tag == "Hell")
        {
            Vector3 vect = transform.position;
            transform.position = new Vector3(vect.x, 30f, vect.y);
        }

        if (collision.gameObject.tag == "Item")
        {
            audioSource.PlayOneShot(itemSound);
            ScoreManager.score -= 10f;
            Destroy(collision.gameObject);

        }

       

    }
   
    void Slow()
    {
        if (!_isGround && rb.velocity.y < -1)
        {
            Vector3 newVelocity = rb.velocity;
            newVelocity.y = -3; 
            rb.velocity = newVelocity; 
        }

    }
}


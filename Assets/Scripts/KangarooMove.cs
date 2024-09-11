using System.Collections;
using UnityEngine;


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
    Animator anim;
    CameraSwitch cameraSwitch;
    [SerializeField] Camera subCam;
    float angle;
    public bool _isClear;
    public bool isGameOver;
    SceneSystem sceneSystem;
    ScoreManager scoreManager;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
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
        if (!cameraSwitch.mainActive)
        {

            angle = subCam.transform.eulerAngles.x;


            Rotating();

            JumpUp();
            BackJump();
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



        if (Input.GetKeyDown(KeyCode.Space) && _isGround)
        {
            falseCheck = false;
            canJump = true;
            StartCoroutine(nameof(Jumping));

        }

        if (Input.GetKeyUp(KeyCode.Space) && !falseCheck && _isGround)
        {
            canJump = false;

        }

        if (Input.GetKeyUp(KeyCode.Space) && canJump && _isGround)
        {

            rb.velocity = new Vector3(forward.x * intertia, jumpPower, forward.z * intertia);
            canJump = false;
        }


    }

    IEnumerator Jumping()
    {
        yield return new WaitForSeconds(0.1f);
        if (Input.GetKey(KeyCode.Space))
        {
            falseCheck = true;

        }
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
       
      

        //if (Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.A))
        //{
        //    this.transform.Rotate(Vector3.up, turnPower);
        //}

        //if (Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
        //{
        //    this.transform.Rotate(Vector3.up, turnPower * -1);
        //}

        //if (angle <= 50 || angle >= 310)
        //{

        //    if (Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S))
        //    {
        //        subCam.transform.Rotate(Vector3.left, turnPower);
        //    }

        //    if (!Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.S))
        //    {
        //        subCam.transform.Rotate(Vector3.left, turnPower * -1);
        //    }
        //}
        //else if (angle <= 100)
        //{
        //    if (Input.GetKey(KeyCode.W))
        //    {
        //        subCam.transform.Rotate(Vector3.left, turnPower);
        //    }
        //}
        //else if (angle >= 250)
        //{
        //    if (Input.GetKey(KeyCode.S))
        //    {
        //        subCam.transform.Rotate(Vector3.left, turnPower - 1);

        //    }
        //}




    

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

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            _isClear = true;
            scoreManager.ScoreCount();
        }

        if (collision.gameObject.tag == "Water")
        {
            isGameOver = true;

            sceneSystem.FadeOut();

        }



    }
}


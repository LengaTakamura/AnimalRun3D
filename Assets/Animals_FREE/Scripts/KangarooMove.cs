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
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();  
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        JumpUp();
        Rotating();
        GroundedCheck();
    }
    private void FixedUpdate()
    {
        forward = transform.forward;

        if (anim)
        {
            anim.SetBool("s", canJump);
            anim.SetBool("g", _isGround);
            anim.SetBool("Space", Input.GetKey(KeyCode.Space));
        }
    }

    void JumpUp()
    {

        if (Input.GetKeyDown(KeyCode.Space) && _isGround)
        {
            falseCheck = false;
            canJump = true;
            StartCoroutine(nameof(Jumping));
            UnityEngine.Debug.Log('s');
        }

        if (Input.GetKeyUp(KeyCode.Space) && !falseCheck && _isGround)
        {
            canJump = false;
            UnityEngine.Debug.Log('f');
        }

        if (Input.GetKeyUp(KeyCode.Space) && canJump && _isGround)
        {
            UnityEngine.Debug.Log('j');
            rb.velocity = new Vector3(forward.x * intertia, jumpPower, forward.z * intertia);
            canJump = false;
        }


    }

    IEnumerator Jumping()
    {
        yield return new WaitForSeconds(3f);
        if (Input.GetKey(KeyCode.Space))
        {
            falseCheck = true;
            UnityEngine.Debug.Log('t');

        }
    }

    void Rotating()
    {
        if (Input.GetKey(KeyCode.L) && !Input.GetKey(KeyCode.K))
        {
            this.transform.Rotate(Vector3.up ,turnPower);
        }

        if (Input.GetKey(KeyCode.K) && !Input.GetKey(KeyCode.L))
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

        if (_isGround && rb.velocity.y < 0)
        {

            // Y軸の速度をリセットして、キャラクターが地面に吸着するようにする
            rb.velocity = new Vector3(rb.velocity.x, -3f, rb.velocity.z);
        }


        if (!_isGround)
        {
            rb.velocity += Vector3.up * gravity * Time.deltaTime;
        }
    }
}

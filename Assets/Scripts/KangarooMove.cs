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
        BackJump(); 
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
        yield return new WaitForSeconds(3f);
        if (Input.GetKey(KeyCode.Space))
        {
            falseCheck = true;
    
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

       
    }

    void BackJump()
    {
        if (Input.GetKeyDown(KeyCode.Less))
        {
            rb.velocity = new Vector3(forward.x * intertia * -0.1f, jumpPower * 0.1f, forward.z * intertia * -0.1f);
        }
    }
}

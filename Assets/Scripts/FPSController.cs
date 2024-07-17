using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSController : MonoBehaviour
{
    float x, z;
    [SerializeField]public float speed = 0.1f;

    public GameObject cam;
    Quaternion cameraRot, characterRot; //回転を表す変数
    float Xsensityvity = 3f, Ysensityvity = 3f;

    bool cursorLock = true;

    float minX = -90f, maxX = 90f;　//変数の宣言(角度の制限用)

    Rigidbody rb;

    [SerializeField]float jumpSpeed = 10f;

    bool isJumping;

    // Start is called before the first frame update
    void Start()
    {
        cameraRot = cam.transform.localRotation;　//cameraの回転を取得
        characterRot = transform.localRotation;   //playerの回転を取得
        rb = GetComponent<Rigidbody>();
        animal = Animal.normal;
    }

    // Update is called once per frame
    void Update()
    {
        float xRot = Input.GetAxis("Mouse X") * Ysensityvity;　//マウスのX方向の動きを検知して動きを格納
        float yRot = Input.GetAxis("Mouse Y") * Xsensityvity;　//マウスのY方向の動きを検知して動きを格納

        //Input.GetAxis("Mouse ○")で取得したマウスの値は、transform.Rotateでxyを入れ替える
        cameraRot *= Quaternion.Euler(-yRot, 0, 0);
        characterRot *= Quaternion.Euler(0, xRot, 0);

        //Updateの中で作成した関数を呼ぶ
        cameraRot = ClampRotation(cameraRot);

        cam.transform.localRotation = cameraRot;
        transform.localRotation = characterRot;


        UpdateCursorLock();

        if (Input.GetKeyDown(KeyCode.Space)&& !isJumping &&animal ==Animal.horse)
        {
            rb.velocity = Vector3.up * jumpSpeed;

            isJumping = true;
        }

    }

    private void FixedUpdate() //一定秒数ごとに呼ばれる　TimeManagerから秒数を変更可能
    {
        x = 0;
        z = 0;

        x = Input.GetAxisRaw("Horizontal") * speed;　//水平方向のキー入力を検知してスピードとかけ合わせる
        z = Input.GetAxisRaw("Vertical") * speed;　　//垂直方向のキー入力を検知してスピードとかけ合わせる

        //transform.position += new Vector3(x,0,z);

        transform.position += cam.transform.forward * z + cam.transform.right * x; 
        //カメラの向き(それぞれの正の方向に動きを加える)から動きを作る

        
    }


    public void UpdateCursorLock()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            cursorLock = false;
        }
        else if (Input.GetMouseButton(0))
        {
            cursorLock = true;
        }


        if (cursorLock)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
        else if (!cursorLock)
        {
            Cursor.lockState = CursorLockMode.None;
        }
    }

    //角度制限関数の作成
    public Quaternion ClampRotation(Quaternion q)
    {
        //q = x,y,z,w (x,y,zはベクトル（量と向き）：wはスカラー（座標とは無関係の量）)

        q.x /= q.w;
        q.y /= q.w;
        q.z /= q.w;
        q.w = 1f;

        float angleX = Mathf.Atan(q.x) * Mathf.Rad2Deg * 2f;

        angleX = Mathf.Clamp(angleX, minX, maxX);

        q.x = Mathf.Tan(angleX * Mathf.Deg2Rad * 0.5f);

        return q;
    }

    private void OnCollisionEnter(Collision collision)
    {

        if(collision.gameObject.tag == "Ground")
        {
            isJumping =false;
        }

        if (collision.gameObject.tag == "Horse")
        {
            Debug.Log("hit horse");
            animal = Animal.horse;
            
        }

    }
    enum Animal { horse, crab, normal, owl, frog }

    Animal animal;


}
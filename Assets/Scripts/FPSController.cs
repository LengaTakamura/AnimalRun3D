using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSController : MonoBehaviour
{
    float x, z;
    [SerializeField]public float speed = 0.1f;

    public GameObject cam;
    Quaternion cameraRot, characterRot; //��]��\���ϐ�
    float Xsensityvity = 3f, Ysensityvity = 3f;

    bool cursorLock = true;

    float minX = -90f, maxX = 90f;�@//�ϐ��̐錾(�p�x�̐����p)

    Rigidbody rb;

    [SerializeField]float jumpSpeed = 10f;

    bool isJumping;

    // Start is called before the first frame update
    void Start()
    {
        cameraRot = cam.transform.localRotation;�@//camera�̉�]���擾
        characterRot = transform.localRotation;   //player�̉�]���擾
        rb = GetComponent<Rigidbody>();
        animal = Animal.normal;
    }

    // Update is called once per frame
    void Update()
    {
        float xRot = Input.GetAxis("Mouse X") * Ysensityvity;�@//�}�E�X��X�����̓��������m���ē������i�[
        float yRot = Input.GetAxis("Mouse Y") * Xsensityvity;�@//�}�E�X��Y�����̓��������m���ē������i�[

        //Input.GetAxis("Mouse ��")�Ŏ擾�����}�E�X�̒l�́Atransform.Rotate��xy�����ւ���
        cameraRot *= Quaternion.Euler(-yRot, 0, 0);
        characterRot *= Quaternion.Euler(0, xRot, 0);

        //Update�̒��ō쐬�����֐����Ă�
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

    private void FixedUpdate() //���b�����ƂɌĂ΂��@TimeManager����b����ύX�\
    {
        x = 0;
        z = 0;

        x = Input.GetAxisRaw("Horizontal") * speed;�@//���������̃L�[���͂����m���ăX�s�[�h�Ƃ������킹��
        z = Input.GetAxisRaw("Vertical") * speed;�@�@//���������̃L�[���͂����m���ăX�s�[�h�Ƃ������킹��

        //transform.position += new Vector3(x,0,z);

        transform.position += cam.transform.forward * z + cam.transform.right * x; 
        //�J�����̌���(���ꂼ��̐��̕����ɓ�����������)���瓮�������

        
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

    //�p�x�����֐��̍쐬
    public Quaternion ClampRotation(Quaternion q)
    {
        //q = x,y,z,w (x,y,z�̓x�N�g���i�ʂƌ����j�Fw�̓X�J���[�i���W�Ƃ͖��֌W�̗ʁj)

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
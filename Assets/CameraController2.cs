using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController2 : MonoBehaviour
{
    [SerializeField] GameObject player;

    Vector3 currentPos;//���݂̃J�����ʒu
    Vector3 pastPos;//�ߋ��̃J�����ʒu



    private void Start()
    {
        //�ŏ��̃v���C���[�̈ʒu�̎擾

    }
    void Update()
    {


        //Vector3 diff = new Vector3(0 , 0.5f,  - 0.7f );


        //transform.position = Vector3.Lerp(transform.position, player.transform.position + diff, 1.0f);//�J�������v���C���[�̈ړ�������������������

        //pastPos = currentPos;




        //    //------�J�����̉�]------

        //    //�}�E�X�̈ړ��ʂ��擾
        //    float mx = Input.GetAxis("Horizontal");
        //    float my = Input.GetAxis("Vertical");

        //    // X�����Ɉ��ʈړ����Ă���Ή���]
        //    if (Mathf.Abs(mx) > 0.01f)
        //    {
        //        // ��]���̓��[���h���W��Y��
        //        transform.RotateAround(player.transform.position, Vector3.up, mx);
        //    }

        //    // Y�����Ɉ��ʈړ����Ă���Ώc��]
        //    if (Mathf.Abs(my) > 0.01f)
        //    {
        //        // ��]���̓J�������g��X��
        //        transform.RotateAround(player.transform.position, transform.right, -my);
        //    }
        //}
    }
}


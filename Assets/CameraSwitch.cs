using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraSwitch : MonoBehaviour
{
    [SerializeField] Camera main;
    [SerializeField] Camera sub;
    AudioSource audioSource;
    public bool mainActive = true;
    [SerializeField] AudioClip switchClip;
    PlayerMove playerMove;
    KangarooMove kangarooMove;
    [SerializeField] Slider subSlider;
    public int count;
    [SerializeField] Pauser pauser;
    // Start is called before the first frame update
    void Start()
    {
        sub .enabled = false;    
        audioSource = GetComponent<AudioSource>();
        playerMove = GameObject.Find("Player").GetComponent<PlayerMove>();
        kangarooMove = GameObject.Find("Kangaroo").GetComponent <KangarooMove>();
    }

    // Update is called once per frame
    void Update()
    {
        audioSource.Play();
        if (Input.GetKeyDown(KeyCode.C) && playerMove._isGround && kangarooMove._isGround && !pauser.isPause )
        {
            audioSource.PlayOneShot(switchClip);

            if (mainActive)
            {
                main.enabled = false;
                mainActive = false;
                sub.enabled = true;
                subSlider.gameObject.SetActive(true);
            }
            else 
            {
                
                main.enabled = true;
                mainActive = true;
                sub.enabled = false;
                
                subSlider.gameObject.SetActive(false);
            }

        }
    }
}

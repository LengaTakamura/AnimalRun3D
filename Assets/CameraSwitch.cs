using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitch : MonoBehaviour
{
    [SerializeField] Camera main;
    [SerializeField] Camera sub;
    AudioSource audioSource;
    public bool mainActive = true;
    [SerializeField] AudioClip switchClip;
    // Start is called before the first frame update
    void Start()
    {
        sub .enabled = false;    
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        audioSource.Play();
        if (Input.GetKeyDown(KeyCode.C))
        {
            audioSource.PlayOneShot(switchClip);

            if (mainActive)
            {
                main.enabled = false;
                mainActive = false;
                sub.enabled = true;
            }
            else 
            {
                main.enabled = true;
                mainActive = true;
                sub.enabled = false;
            }

        }
    }
}

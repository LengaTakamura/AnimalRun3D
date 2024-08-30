using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManager : MonoBehaviour
{
    [SerializeField] AudioClip[] clip;
    PlayerMove _playerMove;
    AudioSource _audioSource;
    bool _isPlaying;
    // Start is called before the first frame update
    void Start()
    {
        _playerMove =GetComponent<PlayerMove>();
        _audioSource = GetComponent<AudioSource>();
        _isPlaying = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (_playerMove.horseState == PlayerMove.HorseState.Running)
        {
            _audioSource.PlayOneShot(clip[0]);
        }
        else if(_playerMove.horseState == PlayerMove.HorseState.Walking)
        {
            _audioSource.PlayOneShot(clip[1]);
        }
        else if(_playerMove.horseState != PlayerMove.HorseState.Jumping)
        {
            if (_isPlaying)
            {

                _audioSource.PlayOneShot(clip[2]);
                _isPlaying = false;
                StartCoroutine(nameof(WaitSeconds));
            }

        }


    }
    IEnumerator WaitSeconds()
    {
        yield return new WaitForSeconds(3f);
        _isPlaying = true;
    }
}

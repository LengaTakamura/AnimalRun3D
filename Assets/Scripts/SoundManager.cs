using System.Collections;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] AudioClip[] clip;
    PlayerMove _playerMove;
    AudioSource _audioSource;
    bool _isPlayingIdol;
    bool _isPlayingWalk;
    bool _isPlayingRun;

    // Start is called before the first frame update
    void Start()
    {
        _playerMove = GetComponent<PlayerMove>();
        _audioSource = GetComponent<AudioSource>();
        _isPlayingIdol = true;
        _isPlayingWalk = true;
        _isPlayingRun = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (_playerMove.horseState == PlayerMove.HorseState.Running)
        {

            if (_isPlayingRun)
            {
                _audioSource.PlayOneShot(clip[0]);
                _isPlayingRun = false;
                StartCoroutine(WaitSecondsR(1f));
            }
        }
        else if (_playerMove.horseState == PlayerMove.HorseState.Walking)
        {

            if (_isPlayingWalk)
            {
                _audioSource.PlayOneShot(clip[1]);
                _isPlayingWalk = false;
                StartCoroutine(WaitSecondsW(0.8f));
            }
        }
        else if (_playerMove.horseState != PlayerMove.HorseState.Jumping)
        {

            if (_isPlayingIdol)
            {
                Debug.Log("soundIdol");
                _audioSource.PlayOneShot(clip[2]);
                _isPlayingIdol = false;
                StartCoroutine(WaitSeconds(3f));
            }

        }
    }
    IEnumerator WaitSeconds(float second)
    {
        yield return new WaitForSeconds(second);
        _isPlayingIdol = true;
        //_isPlayingWalk = true;
        //_isPlayingRun = true;
    }
    IEnumerator WaitSecondsW(float speed)
    {
        yield return new WaitForSeconds(speed);
        _isPlayingWalk = true;  
    }
    IEnumerator WaitSecondsR(float speed)
    {
        yield return new WaitForSeconds(speed);
        _isPlayingRun = true;
    }

}

using UnityEngine;

public abstract class ItemBase : MonoBehaviour
{
    [SerializeField] AudioClip _audioClip;

    [SerializeField] AudioSource _audioSource;

    public abstract void Activate();

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("アイテム取得");

            AudioSource.PlayClipAtPoint(_audioClip, transform.position);

            Activate();

            Destroy(gameObject);

        }
    }



}

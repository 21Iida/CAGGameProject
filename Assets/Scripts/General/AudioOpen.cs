using UnityEngine;

namespace General
{
    public class AudioOpen : MonoBehaviour
    {
        [SerializeField] private AudioClip audioClip;
        private AudioSource _audioSource;

        private void Awake()
        {
            _audioSource = this.GetComponent<AudioSource>();
        }

        private void OnEnable()
        {
            _audioSource.PlayOneShot(audioClip);
        }
    }
}

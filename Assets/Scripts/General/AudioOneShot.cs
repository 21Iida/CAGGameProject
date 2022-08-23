using System;
using UnityEngine;

namespace General
{
    public class AudioOneShot : MonoBehaviour
    {
        [SerializeField] private AudioClip audioClip;
        private AudioSource _audioSource;

        private void Awake()
        {
            _audioSource = this.GetComponent<AudioSource>();
        }

        public void PlaySe()
        {
            _audioSource.PlayOneShot(audioClip);
        }
    }
}

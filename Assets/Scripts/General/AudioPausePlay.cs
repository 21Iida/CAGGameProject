using System;
using UnityEngine;

namespace General
{
    public class AudioPausePlay : MonoBehaviour
    {
        private AudioSource _audioSource;

        private void Awake()
        {
            _audioSource = this.GetComponent<AudioSource>();
        }

        public void Play()
        {
            _audioSource.Play();
        }
        public void Pause()
        {
            _audioSource.Pause();
        }
    }
}

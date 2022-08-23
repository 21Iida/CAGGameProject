using System;
using UnityEngine;
using UnityEngine.UI;

namespace Title
{
    public class AudioScroll : MonoBehaviour
    {
        [SerializeField] private AudioClip audioClip;
        private AudioSource _audioSource;
        private Scrollbar _scrollbar;
        
        private void Awake()
        {
            _audioSource = this.GetComponent<AudioSource>();
            _scrollbar = this.GetComponent<Scrollbar>();
        }

        private void Update()
        {
            
        }

        public void AudioPlay()
        {
            _audioSource.Stop();
            _audioSource.PlayOneShot(audioClip);
        }
    }
}

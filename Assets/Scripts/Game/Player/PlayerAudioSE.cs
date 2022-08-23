using System;
using UnityEngine;

namespace Game.Player
{
    /// <summary>
    /// プレイヤーの効果音を鳴らします
    /// </summary>
    [RequireComponent(typeof(AudioSource))]
    public class PlayerAudioSE : MonoBehaviour
    {
        [SerializeField] private AudioClip jumpSe, landingSe, runSe, damageSe;
        private AudioSource _audioSource;

        private void Awake()
        {
            _audioSource = this.GetComponent<AudioSource>();
        }

        public void JumpSe()
        {
            _audioSource.PlayOneShot(jumpSe);
        }

        public void LandingSe()
        {
            _audioSource.PlayOneShot(landingSe);
        }
        public void RunSe()
        {
            _audioSource.PlayOneShot(runSe);
        }

        public void DamageSe()
        {
            _audioSource.PlayOneShot(damageSe);
        }
    }
}

using System;
using UnityEngine;

namespace Game.World
{
    public class OpenCredit : MonoBehaviour
    {
        [SerializeField] private GameObject credit;
        [SerializeField] private AudioSource audioSource;
        [SerializeField] private AudioClip audioClip;
        
        private void Update()
        {
            if (Input.GetButtonDown("Jump"))
            {
                audioSource.clip = audioClip;
                audioSource.loop = true;
                audioSource.Play();
                credit.SetActive(true);
                this.gameObject.SetActive(false);
            }
        }
    }
}

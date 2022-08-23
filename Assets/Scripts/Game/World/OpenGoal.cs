using System;
using Mono.Cecil.Cil;
using UnityEngine;

namespace Game.World
{
    public class OpenGoal : MonoBehaviour
    {
        private bool goalOver = false;
        [SerializeField] private GameObject clearPanel;
        [SerializeField] private AudioClip goalMusic;
        [SerializeField] private AudioSource audioSource;

        private void OnTriggerEnter2D(Collider2D col)
        {
            if(!col.CompareTag("Player")) return;
            if(goalOver) return;
            
            Goal();
        }

        private void Goal()
        {
            goalOver = true;
            audioSource.loop = false;
            audioSource.clip = goalMusic;
            audioSource.Play();
            Time.timeScale = 0.000001f;
            clearPanel.SetActive(true);
        }
    }
}

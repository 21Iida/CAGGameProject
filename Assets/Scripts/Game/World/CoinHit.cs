using System;
using UnityEngine;

namespace Game.World
{
    public class CoinHit : MonoBehaviour
    {
        private CoinHold coinHold;
        [SerializeField] private GameObject getEffect;
        [SerializeField] private AudioClip audioClip;
        private AudioSource audioSource;

        private void OnEnable()
        {
            coinHold = GameObject.FindObjectOfType<CoinHold>();
            audioSource = transform.parent.GetComponent<AudioSource>();
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            if(!col.CompareTag("Player")) return;
            coinHold.AddCoin(1);
            audioSource.PlayOneShot(audioClip);
            GameObject.Instantiate(getEffect, this.transform.position, Quaternion.identity);
            GameObject.Destroy(this.gameObject);
        }
    }
}

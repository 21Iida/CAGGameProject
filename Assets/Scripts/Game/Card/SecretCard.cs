using System;
using Game.World;
using UnityEngine;

namespace Game.Card
{
    /// <summary>
    /// シークレットカードを拾ったときにランダムなカードかコインを取得します
    /// </summary>
    public class SecretCard : MonoBehaviour
    {
        private Gacha gacha;
        private CoinHold coinHold;
        private PlayerHoldCard playerHoldCard;
        [SerializeField] private int changeCoin = 5;
        [SerializeField] private GameObject coinIcon;

        [SerializeField] private AudioClip audioClip;
        private AudioSource audioSource;

        private void OnEnable()
        {
            gacha = GameObject.FindObjectOfType<Gacha>();
            coinHold = GameObject.FindObjectOfType<CoinHold>();
            playerHoldCard = GameObject.FindObjectOfType<PlayerHoldCard>();
            audioSource = transform.parent.GetComponent<AudioSource>();
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            if(!col.CompareTag("Player")) return;

            //カードの所持数上限ならコイン5枚獲得
            if (playerHoldCard.IsCardMax())
            {
                coinHold.AddCoin(changeCoin);
                Instantiate(coinIcon, transform.position, Quaternion.identity);
            }
            //カードを追加可能ならガチャ
            else
            {
                var resultCard = gacha.RunGacha();
                ResultPopUp(resultCard);
            }
            audioSource.PlayOneShot(audioClip);
            GameObject.Destroy(this.gameObject);
        }

        private void ResultPopUp(GameObject card)
        {
            var popIcon = card.GetComponent<CardPopIcon>().GetIconPrefab;
            Instantiate(popIcon, transform.position, Quaternion.identity);
        }
    }
}

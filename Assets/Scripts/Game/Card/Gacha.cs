using System;
using System.Collections.Generic;
using System.Linq;
using Game.World;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Game.Card
{
    /// <summary>
    /// カードをランダムに読み込んで、ハンドに追加します
    /// </summary>
    public class Gacha : MonoBehaviour
    {
        [SerializeField] private List<GameObject> cardList;
        private List<float> _probabilities = new List<float>();
        [SerializeField] private PlayerHoldCard playerHoldCard;

        private void Start()
        {
            foreach (var card in cardList)
            {
                _probabilities.Add(card.GetComponent<CardGachaRarity>().Probability);
            }
        }

        public GameObject RunGacha()
        {
            //重み付き確立
            var total = _probabilities.Sum();
            var result = Random.value * total;

            var resultIndex = 0;

            foreach (var probability in _probabilities)
            {
                if (result < probability)
                {
                    result = probability;
                    break;
                }
                else
                {
                    result -= probability;
                    resultIndex++;
                }
            }
            
            //選出されたカードをハンドに追加
            playerHoldCard.AddCard(cardList[resultIndex]);

            return cardList[resultIndex];
        }
    }
}

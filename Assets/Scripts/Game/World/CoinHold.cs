using System;
using UnityEngine;
using TMPro;

namespace Game.World
{
    public class CoinHold : MonoBehaviour
    {
        [SerializeField] private int coinCount = 0;
        [SerializeField] private TextMeshProUGUI tmp;
        
        private void Update()
        {
            tmp.text = coinCount.ToString();
        }

        public void AddCoin(int coin)
        {
            coinCount += coin;
        }
        public void UseCoin(int coin)
        {
            coinCount -= coin;
        }
    }
}

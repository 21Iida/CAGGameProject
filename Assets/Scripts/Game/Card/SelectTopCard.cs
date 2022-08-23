using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Game.Card
{
    /// <summary>
    /// すべてのカードで、Y軸が一番上のカードがどれかを判定する
    /// </summary>
    public class SelectTopCard : MonoBehaviour
    {
        [SerializeField] private List<GameObject> card;

        private void Update()
        {
            ResetCard();
        }

        public bool IsTopCard(Vector3 pos)
        {
            var maxY = card.Select(item => item.transform.position.y).Max();

            return Mathf.Approximately(pos.y, maxY);
        }
        
        private void ResetCard()
        {
            card.Clear();
            foreach (Transform item in transform)
            {
                card.Add(item.gameObject.GetComponent<SelectedCard>().Card);
            }
        }
    }
}

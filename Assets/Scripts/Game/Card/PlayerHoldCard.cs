using System.Collections.Generic;
using UnityEngine;

namespace Game.Card
{
    /// <summary>
    /// プレイヤーの持っているカード一覧
    /// </summary>
    public class PlayerHoldCard : MonoBehaviour
    {
        [SerializeField] private List<GameObject> cardList;
        public List<GameObject> CardList => cardList;
        [SerializeField] private int holdMax = 8;

        public bool IsCardMax()
        {
            return cardList.Count >= holdMax;
        }

        public void AddCard(GameObject card)
        {
            cardList.Add(card);
        }
        public void UseCard(int index)
        {
            cardList.RemoveAt(index);
        }
    }
}

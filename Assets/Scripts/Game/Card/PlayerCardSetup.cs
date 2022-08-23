using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Game.Card
{
    /// <summary>
    /// プレイヤーの持っているカードを配置する
    /// </summary>
    public class PlayerCardSetup : MonoBehaviour
    {
        [SerializeField] private PlayerHoldCard playerHoldCard;
        private List<GameObject> instantCard = new List<GameObject>();

        private void OnEnable()
        {
            var cardList = playerHoldCard.CardList;
            if(!cardList.Any()) return;
            var rot = 0f;
            rot += this.transform.rotation.eulerAngles.z;
            var rotStep = 360 / cardList.Count;
            foreach (var card in cardList)
            {
                instantCard.Add(
                    GameObject.Instantiate(card,
                        this.transform.position,
                        Quaternion.Euler(new Vector3(0, 0, rot)),
                        this.gameObject.transform)
                );

                rot -= rotStep;
            }
        }

        private void OnDisable()
        {
            foreach (var item in instantCard)
            {
                GameObject.Destroy(item);
            }
            instantCard.Clear();
        }
    }
}

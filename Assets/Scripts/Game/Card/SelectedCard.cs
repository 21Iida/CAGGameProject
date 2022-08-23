using System;
using UnityEditor;
using UnityEngine;

namespace Game.Card
{
    /// <summary>
    /// Y軸の座標的に一番上なら選択されている状態になる
    /// </summary>
    public class SelectedCard : MonoBehaviour
    {
        private ICardEffect iCardEffect;
        private bool isSelect = false;
        [SerializeField] private GameObject card;
        [SerializeField] private GameObject selectBox;
        [SerializeField] private GameObject expWindow;
        public GameObject Card => card;

        private SelectTopCard selectTopCard;

        private bool delay = false;
        
        private void OnEnable()
        {
            selectTopCard = transform.parent.gameObject.GetComponent<SelectTopCard>();
            iCardEffect = this.GetComponent<ICardEffect>();
        }

        private void Update()
        {
            //なぜか1フレーム目にコンポーネントが取れないので応急処置
            if (!delay)
            {
                delay = true;
                return;
            }
            
            //選択状態をチェック
            isSelect = selectTopCard.IsTopCard(card.transform.position);
            if (isSelect)
            {
                SelectedMe();
            }
            else
            {
                UnSelectedMe();
            }
            
            //決定ボタンでカードを使用
            if (Input.GetButtonDown("Jump") && selectBox.activeSelf)
            {
                if(iCardEffect == null) return;
                UseMe();
            }
        }

        private void SelectedMe()
        {
            selectBox.SetActive(true);
            expWindow.SetActive(true);
            const int selectScale = 2;
            card.transform.localScale = Vector3.one * selectScale;
        }

        private void UnSelectedMe()
        {
            selectBox.SetActive(false);
            expWindow.SetActive(false);
            card.transform.localScale = Vector3.one;
        }

        private void UseMe()
        {
            iCardEffect.RunEffect();
            //GameObject.FindObjectOfType<OpenCardPanel>().Close();
        }
    }
}

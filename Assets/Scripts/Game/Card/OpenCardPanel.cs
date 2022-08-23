using System;
using UnityEngine;

namespace Game.Card
{
    /// <summary>
    /// カード選択画面を開く
    /// </summary>
    public class OpenCardPanel : MonoBehaviour
    {
        [SerializeField] private GameObject cardPanel;

        private void Update()
        {
            if (!Input.GetButtonDown("Fire2")) return;
            if (cardPanel.activeInHierarchy)
            {
                Close();
            }
            else
            {
                Open();
            }
        }

        private void Open()
        {
            if(Time.timeScale <= 0.001f) return;
            Time.timeScale = 0.000001f;
            cardPanel.SetActive(true);
        }

        public void Close()
        {
            Time.timeScale = 1f;
            cardPanel.SetActive(false);
        }
    }
}

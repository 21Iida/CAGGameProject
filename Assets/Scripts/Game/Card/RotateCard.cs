using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Game.Card
{
    /// <summary>
    /// キー入力でカードを回す
    /// </summary>
    public class RotateCard : MonoBehaviour
    {
        [SerializeField] private List<GameObject> cardPivot;
        [SerializeField] private float targetRot;
        private PlayerHoldCard playerHoldCard;
        private int selectIndex;
        [SerializeField] private AudioSource audioSource;
        [SerializeField] private AudioClip useSe;

        private void Start()
        {
            playerHoldCard = GameObject.FindObjectOfType<PlayerHoldCard>();
        }

        private void OnEnable()
        {
            targetRot = 0f;
            this.transform.rotation = Quaternion.identity;
            selectIndex = 0;
        }

        private void LateUpdate()
        {
            ResetCard();


            var activeCard = cardPivot.Count(item => item.activeInHierarchy);
            if(activeCard <= 0) return;
            var rotateStep = 360 / activeCard;
            
            RotateUpdate();
            
            if (Input.GetButtonDown("Jump"))
            {
                UseSelectedCard();
            }

            if (!Input.GetButtonDown("Horizontal")) return;

            var inputAxis = Input.GetAxisRaw("Horizontal");
            switch (inputAxis)
            {
                case > 0:
                    targetRot += rotateStep;
                    selectIndex++;
                    break;
                case < 0:
                    targetRot -= rotateStep;
                    selectIndex--;
                    break;
            }

            targetRot = Mathf.Repeat(targetRot, 360);
            if (selectIndex == -1) selectIndex = cardPivot.Count - 1;
            else if (selectIndex == cardPivot.Count) selectIndex = 0;
        }

        private void RotateUpdate()
        {
            this.transform.rotation =
                Quaternion.Slerp(transform.rotation,
                    Quaternion.Euler(new Vector3(0, 0, targetRot)),
                    0.2f);
        }

        private void ResetCard()
        {
            cardPivot.Clear();
            foreach (Transform item in transform)
            {
                cardPivot.Add(item.gameObject);
            }
        }

        private void UseSelectedCard()
        {
            audioSource.PlayOneShot(useSe);
            playerHoldCard.UseCard(selectIndex);
            GameObject.FindObjectOfType<OpenCardPanel>().Close();
        }
    }
}

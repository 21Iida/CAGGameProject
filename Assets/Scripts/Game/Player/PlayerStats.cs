using System;
using System.Collections;
using UnityEngine;

namespace Game.Player
{
    public class PlayerStats : MonoBehaviour
    {
        [SerializeField] private int normalAtk = 16;
        private int atk;
        public int Atk => atk;
        [SerializeField] private float effectAngleRange = 30;
        [SerializeField] private GameObject normalEffect;
        private GameObject attackEffect;
        [SerializeField] private AudioClip normalSe;
        private AudioClip attackSe;
        [SerializeField] private AudioSource audioSource;
        [SerializeField] private SpriteRenderer playerSprite;

        private Coroutine enchantCoroutine;//同じカードを使ったときにリセットを先延ばし

        private void Awake()
        {
            atk = normalAtk;
            attackEffect = normalEffect;
            attackSe = normalSe;
        }
        
        public void AttackMotion(Vector3 effectPoint)
        {
            var randomRot = new Vector3(0, 0,
                UnityEngine.Random.Range(-effectAngleRange, effectAngleRange));
            GameObject.Instantiate(attackEffect, effectPoint, Quaternion.Euler(randomRot));
            audioSource.PlayOneShot(attackSe);
        }
        public void AttackMotion(Vector3 effectPoint, GameObject effectPrefab)
        {
            var randomRot = new Vector3(0, 0,
                UnityEngine.Random.Range(-effectAngleRange, effectAngleRange));
            GameObject.Instantiate(effectPrefab, effectPoint, Quaternion.Euler(randomRot));
            audioSource.PlayOneShot(attackSe);
        }

        public void Enchantment(int attack, GameObject effect, AudioClip se, Color spriteColor, float lifeTime)
        {
            atk = attack;
            attackEffect = effect;
            attackSe = se;
            playerSprite.color = spriteColor;
            if(enchantCoroutine != null) StopCoroutine(enchantCoroutine);
            enchantCoroutine = StartCoroutine(EndEnchantment(lifeTime));
        }

        private IEnumerator EndEnchantment(float lifeTime)
        {
            yield return new WaitForSeconds(lifeTime);
            ResetStats();
        }

        private void ResetStats()
        {
            atk = normalAtk;
            attackEffect = normalEffect;
            attackSe = normalSe;
            playerSprite.color = Color.white;
        }
    }
}

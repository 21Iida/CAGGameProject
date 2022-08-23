using Game.Player;
using Game.World;
using UnityEngine;

namespace Game.Card
{
    public class CardEffectShield : MonoBehaviour, ICardEffect
    {
        private PlayerDamage playerDamage;
        [SerializeField] private GameObject effect;
        [SerializeField] private AudioClip se;
        [SerializeField] private float lifeTime = 1;

        private void Start()
        {
            playerDamage = GameObject.FindObjectOfType<PlayerDamage>();
        }

        public void RunEffect()
        {
            playerDamage.Shield(lifeTime);
            var shield =
                GameObject.Instantiate(effect,
                    playerDamage.transform.position,
                    Quaternion.identity,
                    playerDamage.gameObject.transform);
            shield.GetComponent<OneShotEffect>().lifeTime = lifeTime;
        }
    }
}

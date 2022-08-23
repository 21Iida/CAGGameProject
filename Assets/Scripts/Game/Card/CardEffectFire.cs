using Game.Player;
using UnityEngine;

namespace Game.Card
{
    public class CardEffectFire : MonoBehaviour, ICardEffect
    {
        private PlayerStats playerStats;
        [SerializeField] private int attack = 24;
        [SerializeField] private GameObject effect;
        [SerializeField] private AudioClip se;
        [SerializeField] private float lifeTime = 1;

        private void Start()
        {
            playerStats = GameObject.FindObjectOfType<PlayerStats>();
        }

        public void RunEffect()
        {
            playerStats.Enchantment(attack, effect, se, new Color(1, 0.5f, 0.5f, 1), lifeTime);
        }
    }
}

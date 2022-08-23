using Game.Enemy;
using UnityEngine;

namespace Game.Player
{
    public class EffectAttack : MonoBehaviour
    {
        private PlayerStats playerStats;
        [SerializeField] private int attack;
        [SerializeField] private GameObject damageEffect;

        private void Awake()
        {
            playerStats = GameObject.FindObjectOfType<PlayerStats>();
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (!col.CompareTag("Enemy")) return;
            if(!col.GetComponent<EnemyStatus>()) return;
            col.GetComponent<EnemyStatus>().Damaged(attack);
            playerStats.AttackMotion(col.transform.position);
        }
    }
}

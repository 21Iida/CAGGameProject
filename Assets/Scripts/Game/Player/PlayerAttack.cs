using System;
using Game.Enemy;
using Mono.Cecil.Cil;
using UnityEngine;

namespace Game.Player
{
    public class PlayerAttack : MonoBehaviour
    {
        private PlayerStats playerStats;
        [SerializeField] private int attackUp;

        private void Awake()
        {
            playerStats = GameObject.FindObjectOfType<PlayerStats>();
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            if(!col.CompareTag("Enemy")) return;
            
            if(!col.GetComponent<EnemyStatus>()) return;
            
            col.GetComponent<EnemyStatus>().Damaged(playerStats.Atk + attackUp);
            playerStats.AttackMotion(col.transform.position);
        }
    }
}

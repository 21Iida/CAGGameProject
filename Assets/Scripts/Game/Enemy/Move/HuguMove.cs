using System;
using Game.Player;
using UnityEngine;

namespace Game.Enemy.Move
{
    public class HuguMove : MonoBehaviour, IEnemyMove
    {
        [SerializeField] private float attackTime = 1;
        private float nowTime = 0;
        private bool isDamaging = false;
        
        private Animator _animator;
        private static int _hashAttack = Animator.StringToHash("Attack");

        private PlayerStats _playerStats;

        private void Awake()
        {
            _animator = this.GetComponent<Animator>();
            _playerStats = GameObject.FindObjectOfType<PlayerStats>();
        }

        private void Update()
        {
            if (isDamaging) nowTime = 0;
            if (_animator.GetCurrentAnimatorStateInfo(0).IsName("HuguAttack"))
            {
                nowTime = 0;
            }
            else
            {
                Flip();
            }
            nowTime += Time.deltaTime;
            if (nowTime >= attackTime)
            {
                Attack();
                nowTime = 0;
            }
        }

        private void Attack()
        {
            _animator.SetTrigger(_hashAttack);
        }

        private void Flip()
        {
            var diff = (this.transform.position.x - _playerStats.transform.position.x);
            if (diff > 0)
            {
                this.transform.localScale = new Vector3(1, 1, 1);
            }
            else
            {
                this.transform.localScale = new Vector3(-1, 1, 1);
            }
        }

        public void Damage()
        {
            
        }
    }
}

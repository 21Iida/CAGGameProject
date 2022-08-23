using System;
using UnityEngine;

namespace Game.Enemy
{
    /// <summary>
    /// コピーエネミーの寿命や攻撃の有効性を制限します
    /// </summary>
    public class CopiedEnemyLimit : MonoBehaviour
    {
        [SerializeField] private float lifeTime = 2f;
        //役割とは逸脱するけど、計算量のためにここに初期状態の向きを設定する
        private SpriteRenderer _enemySprite;
        private Rigidbody2D _rigidbody2D;
        private Animator _animator;

        private void Awake()
        {
            _enemySprite = this.GetComponentInChildren<SpriteRenderer>();
            _rigidbody2D = this.GetComponent<Rigidbody2D>();
            _animator = this.GetComponent<Animator>();
        }

        private void Update()
        {
            if (Time.timeScale < 1)
            {
                this.tag = "Untagged";
            }
            else
            {
                this.tag = "CopiedEnemy";
            }

            if(!_rigidbody2D.simulated) return;
            //寿命
            lifeTime -= Time.deltaTime;
            if (lifeTime <= 0)
            {
                GameObject.Destroy(this.gameObject);
            }
        }

        public void StopPower()
        {
            _rigidbody2D.simulated = false;
            _animator.speed = 0;
        }
        public void StartPower()
        {
            _rigidbody2D.simulated = true;
            _animator.speed = 1;
        }

        public void EnemyAxis(bool flip)
        {
            _enemySprite.flipX = flip;
        }
    }
}

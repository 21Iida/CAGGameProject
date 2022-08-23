using Game.Player;
using UnityEngine;

namespace Game.Enemy.Move
{
    /// <summary>
    /// 雑魚エネミーの移動処理です
    /// </summary>
    public class ZakoWalk : MonoBehaviour, IEnemyMove
    {
        [SerializeField] private float moveSpeed = 0.1f;
        private float nowSpeed;
        private Rigidbody2D _rigidbody2D;
        private SpriteRenderer _enemySprite;
        private PlayerStats _playerStats;

        private void Awake()
        {
            nowSpeed = moveSpeed;
            _rigidbody2D = this.GetComponent<Rigidbody2D>();
            _enemySprite = this.GetComponentInChildren<SpriteRenderer>();
            _playerStats = GameObject.FindObjectOfType<PlayerStats>();
        }

        private void FixedUpdate()
        {
            if(!_rigidbody2D.simulated) return;
            
            transform.Translate(nowSpeed,0,0);
        }

        private void Update()
        {
            Flip();
        }

        private void Flip()
        {
            if (_enemySprite.flipX)
            {
                nowSpeed = moveSpeed;
            }
            else
            {
                nowSpeed = moveSpeed * -1;
            }
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            //左右判定を取った方がいい？
            //もし壁につまるようなら修正する
            _enemySprite.flipX = !_enemySprite.flipX;
        }

        public void Damage()
        {
            var distance = 
                this.transform.position.x - _playerStats.transform.position.x;
            if (distance > 0)
            {
                _rigidbody2D.velocity = Vector2.zero;
                _rigidbody2D.AddForce(new Vector2(0.5f, 0.5f), ForceMode2D.Impulse);
            }
            else
            {
                _rigidbody2D.velocity = Vector2.zero;
                _rigidbody2D.AddForce(new Vector2(-0.5f, 0.5f), ForceMode2D.Impulse);
            }
        }
    }
}

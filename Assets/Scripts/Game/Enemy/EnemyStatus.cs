using System;
using UnityEngine;

namespace Game.Enemy
{
    /// <summary>
    /// ゲームに登場しているエネミーの情報です
    /// </summary>
    public class EnemyStatus : MonoBehaviour
    {
        [SerializeField] private ParamNormalEnemy paramNormalEnemy;
        private string _myName;
        private int _myHp;
        private int _myAttack;
        public int MyAttack => _myAttack;
        private IEnemyMove _enemyMove;

        private void Awake()
        {
            _myName = paramNormalEnemy.EnemyName;
            _myHp = paramNormalEnemy.Hp;
            _myAttack = paramNormalEnemy.Attack;
            _enemyMove = this.GetComponent<IEnemyMove>();
        }

        public void Damaged(int attack)
        {
            _enemyMove.Damage();
            
            _myHp -= attack;
            if(_myHp <= 0) Dead();
        }

        private void Dead()
        {
            GameObject.Destroy(this.gameObject);
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            var otherTag = "EditorOnly";
            if (this.gameObject.CompareTag("Enemy")) otherTag = "CopiedEnemy";
            if (this.gameObject.CompareTag("CopiedEnemy")) otherTag = "Enemy";
            
            if(!col.CompareTag(otherTag)) return;
            col.GetComponent<EnemyStatus>().Damaged(_myAttack);
        }
    }
}

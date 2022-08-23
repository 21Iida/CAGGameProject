using System;
using UnityEngine;

namespace Game.Enemy.Move
{
    public class UniAttack : MonoBehaviour
    {
        [SerializeField] private float attackTime = 1f;
        [SerializeField] private GameObject toge;
        private float _timer = 0;
        
        private void Update()
        {
            _timer += Time.deltaTime;
            if (_timer >= attackTime)
            {
                GameObject.Instantiate(toge,this.transform.position,this.transform.rotation);
                //なぜかPickCopyEnemyがうまく初期化されない？
                //途中でインスタンスを取ったオブジェクトがコピーできない
                _timer = 0;
            }
        }
    }
}

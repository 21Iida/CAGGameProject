using UnityEngine;

namespace Game.Enemy
{
    /// <summary>
    /// エネミーのパラメータです。ゲーム開始時に読み込むために使用します。
    /// </summary>
    [System.Serializable]
    [CreateAssetMenu(fileName = "ParamNormalEnemy")]
    public class ParamNormalEnemy : ScriptableObject
    {
        [SerializeField] private string enemyName = "Zako";
        public string EnemyName => enemyName;
        
        [SerializeField] private int hp = 16;
        public int Hp => hp;
        
        [SerializeField] private int attack = 16;
        public int Attack => attack;

    }
}
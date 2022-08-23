using System.Collections.Generic;
using UnityEngine;

namespace Game.Enemy
{
    /// <summary>
    /// エネミーの一覧です。
    /// エネミーそのもののプレハブと、対応するコピーエネミーのプレハブを持ちます
    /// </summary>
    [System.Serializable]
    [CreateAssetMenu(fileName = "EnemyParams")]
    public class EnemyParams : ScriptableObject
    {
        public List<ParamMaster> paramMasters = new List<ParamMaster>();
        
        [System.Serializable]
        public class ParamMaster
        {
            [SerializeField] private ParamNormalEnemy paramNormalEnemy;
            public ParamNormalEnemy ParamNormalEnemy => paramNormalEnemy;

            [SerializeField] private GameObject enemyPrefab;
            public GameObject EnemyPrefab => enemyPrefab;

            [SerializeField] private GameObject copiedPrefab;
            public GameObject CopiedPrefab => copiedPrefab;
        }
    }
}

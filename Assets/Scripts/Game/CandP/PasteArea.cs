using System;
using System.Collections.Generic;
using System.Linq;
using Game.Enemy;
using UnityEngine;

namespace Game.CandP
{
    /// <summary>
    /// ペーストの矩形を生成してコピーエネミーを配置します
    /// Update()内でGetComponentしているが、1フレーム内なのでひとまず許容
    /// </summary>
    public class PasteArea : MonoBehaviour
    {
        [SerializeField] private DragAreaGenerate dragAreaGenerate;
        [SerializeField] private float areaWidth, areaHeight;
        [SerializeField] private List<AreaEnemy> pasteEnemies = new List<AreaEnemy>();
        private CopyTarget _copyTarget;
        private List<GameObject> _instances = new List<GameObject>();
        
        //コピーエネミーの位置調整用
        [SerializeField] private GameObject copiedHold, copiedRoot;

        private void Awake()
        {
            _copyTarget = this.GetComponent<CopyTarget>();
        }

        private void Update()
        {
            //Esc中ははじく
            if(Time.timeScale == 0) return;
            
            if (Input.GetMouseButtonDown(0))
            {
                PreviewCopiedEnemies();
            }
            
            if (Input.GetMouseButton(0))
            {
                CreateArea();
            }
            
            if (Input.GetMouseButtonUp(0))
            {
                EndCreateArea();
                CompleteCopyEnemy();
            }

            if (Input.GetMouseButtonUp(1))
            {
                _copyTarget.TargetCount();
                if (_copyTarget.TargetEnemies.Count != 0)
                {
                    pasteEnemies = _copyTarget.TargetEnemies;
                }
            }
        }

        private void CreateArea()
        {
            dragAreaGenerate.MouseArea(areaWidth, areaHeight);
        }

        private void EndCreateArea()
        {
            dragAreaGenerate.DragAreaComplete();
        }

        private void PreviewCopiedEnemies()
        {
            foreach (var enemy in pasteEnemies)
            {
                var mouse = dragAreaGenerate.RegisterMouseVertex() + this.transform.position;
                
                var instance = Instantiate(enemy.enemyPrefab, copiedHold.transform, true);
                _instances.Add(instance);
                var pivot = - new Vector2(areaWidth / 2, areaHeight / 2) + new Vector2(mouse.x, mouse.y);
                var localPos = enemy.enemyPos + pivot; 
                instance.transform.position = localPos;
                instance.transform.rotation = enemy.enemyRot;
                instance.transform.GetComponentInChildren<SpriteRenderer>().flipX = enemy.enemyAxis;
                instance.GetComponent<CopiedEnemyLimit>().StopPower();
            }
        }

        private void CompleteCopyEnemy()
        {
            if(_instances.Count <= 0) return;
            
            foreach (var instance in _instances)
            {
                //なぜかたまにnullになって怒られるので除外
                if(instance == null) return;
                instance.GetComponent<CopiedEnemyLimit>().StartPower();
                instance.transform.parent = copiedRoot.transform;
            }
            _instances.Clear();
        }

        public void PasteAreaSize(float width, float height)
        {
            // Debug.Log("PAS " + _copyTarget.TargetEnemies.Count);
            // _copyTarget.TargetCount();
            areaWidth = width;
            areaHeight = height;
        }
    }
}

using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using UnityEngine;

namespace Game.CandP
{
    /// <summary>
    /// コピーの対象になったエネミーをリストとして保存します
    /// </summary>
    public class CopyTarget : MonoBehaviour
    {
        //エネミーの種類、エネミーの位置、エネミーの向き
        [SerializeField] private List<AreaEnemy> targetEnemies = new List<AreaEnemy>();
        //もしも新たにコピーを取得しなかった場合は前回の内容を呼び起こす
        //TODO:消す。代わりにPasteAreaのリストをキャッシュに使う
        //[SerializeField] private List<AreaEnemy> cashEnemies = new List<AreaEnemy>(); 
        private DragAreaGenerate _dragAreaGenerate;
        private PasteArea _pasteArea;
        public List<AreaEnemy> TargetEnemies => targetEnemies;

        public void TargetCount()
        {
            Debug.Log(targetEnemies.Count);
        }

        private void Awake()
        {
            _dragAreaGenerate = this.GetComponentInChildren<DragAreaGenerate>();
            _pasteArea = this.GetComponent<PasteArea>();
        }

        private void Update()
        { 
            if (Input.GetMouseButtonDown(1))
            {
                //cashEnemies = targetEnemies;
                ResetCopyPool();
            }
            //ペースト用のcashを保存
            
            if (Input.GetMouseButtonUp(1))
            {
                if (targetEnemies.Any())
                {
                    //cashEnemies = targetEnemies;
                }
                else
                {
                    //targetEnemies = cashEnemies;
                }
            }
        }

        //現在のドラッグエリアのRectangleFを生成します
        private RectangleF AreaRectF()
        {
            var daOrigin = _dragAreaGenerate.GetAreaOrigin();
            return new RectangleF(daOrigin.x,daOrigin.y,
                _dragAreaGenerate.GetAreaWidth,_dragAreaGenerate.GetAreaHeight);
        }

        public bool DragAreaContains(Vector2 enemyVec)
        {
            var rectF = AreaRectF();
            
            return rectF.Contains(enemyVec.x, enemyVec.y);
        }

        //エネミーからコピー内容を受け取る
        public void AddCopyPool(GameObject enemyPrefab, Vector2 enemyWorldPos, Quaternion enemyRot, bool enemyAxis)
        {
            var daOrigin = _dragAreaGenerate.GetAreaOrigin();
            enemyWorldPos -= daOrigin;
            targetEnemies.Add(new AreaEnemy(enemyPrefab, enemyWorldPos, enemyRot, enemyAxis));
        }
        public void ResetCopyPool()
        {
            targetEnemies.Clear();
        }
    }

    [System.Serializable]
    public class AreaEnemy
    {
        public GameObject enemyPrefab;
        public Vector2 enemyPos;
        public Quaternion enemyRot;
        public bool enemyAxis;

        public AreaEnemy(GameObject enemyPrefab, Transform transform, bool enemyAxis)
        {
            this.enemyPrefab = enemyPrefab;
            this.enemyPos = transform.localToWorldMatrix.GetPosition();
            this.enemyRot = transform.rotation;
            this.enemyAxis = enemyAxis;
        }
        public AreaEnemy(GameObject enemyPrefab, Vector2 enemyPos, Quaternion enemyRot, bool enemyAxis)
        {
            this.enemyPrefab = enemyPrefab;
            this.enemyPos = enemyPos;
            this.enemyRot = enemyRot;
            this.enemyAxis = enemyAxis;
        }
    }
}

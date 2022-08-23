using System;
using UnityEngine;

namespace Game.CandP
{
    /// <summary>
    /// コピーエリアの生成
    /// </summary>
    public class CopyDrag : MonoBehaviour
    {
        [SerializeField] private DragAreaGenerate dragAreaGenerate;
        [SerializeField] private DragGage dragGage;

        [SerializeField] private float maxDragArea = 1.0f;
        public float GetMaxDragArea => maxDragArea;

        private PasteArea _pasteArea;

        private void Awake()
        {
            _pasteArea = this.GetComponent<PasteArea>();
        }

        private void Update()
        {
            //これをしないと頂点が置いていかれる
            //ゲージをチャージしているようにしたい場合はあとで考える
            if (!Input.GetMouseButton(1))
            {
                dragAreaGenerate.ResetVertex();
            }
            
            //ドラッグ中にエスケープされた時を想定してこの位置ではじきます
            if(Time.timeScale == 0) return;
            
            if (Input.GetMouseButtonUp(1))
            {
                dragAreaGenerate.DragAreaComplete();
                Time.timeScale = 1;
            }
            
            //ドラッグエリアの面積を計算
            var boxArea = DragBoxArea();
            //ドラッグゲージの内容を更新する
            dragGage.GageUpdate(maxDragArea, maxDragArea - boxArea);
            
            //ドラッグエリアの開始
            if (Input.GetMouseButtonDown(1))
            {
                dragAreaGenerate.DragAreaStart();
                //0にすると復帰できなくなる
                Time.timeScale = 0.000001f;
            }
            
            if (Input.GetMouseButton(1))
            {
                var withinRange = boxArea < maxDragArea;
                
                //範囲内なら現在のマウスの位置をそのままドラッグエリアに使用
                if (withinRange)
                {
                    dragAreaGenerate.DragAreaEndPoint();
                }
                //範囲外なら終端の位置を計算して登録する
                else
                { 
                    dragAreaGenerate.DragAreaOverEndPoint(maxDragArea);
                }
                _pasteArea.PasteAreaSize(dragAreaGenerate.GetAreaWidth, dragAreaGenerate.GetAreaHeight);
            }
        }

        //四角形の面積が規定以上になるとアウトにしたい
        //評価用の仮のドラッグエリアの面積を返してくれる
        float DragBoxArea()
        {
            //開始地点と、現在のマウスの位置から、面積を計算
            var nowPos = dragAreaGenerate.RegisterMouseVertex();
            var boxX = Mathf.Abs(dragAreaGenerate.GetStartVertex.x 
                                 - nowPos.x);
            var boxY = Mathf.Abs(dragAreaGenerate.GetStartVertex.y 
                                  - nowPos.y);
            return boxX * boxY;
        }
    }
}

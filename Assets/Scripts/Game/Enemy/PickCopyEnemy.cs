using Game.CandP;
using UnityEngine;

namespace Game.Enemy
{
    /// <summary>
    /// コピーエリアに囲われると、コピー対象のリストに自身を追加します
    /// </summary>
    public class PickCopyEnemy : MonoBehaviour
    {
        //エネミーをコピーするとできる個体
        [SerializeField] private GameObject copied;
        private GameObject _previewInstance;
        private bool _canCopy = true;

        private DragAreaGenerate _dragAreaGenerate;
        private CopyTarget _copyTarget;
        private SpriteRenderer _enemySprite;
        
        private void Awake()
        {
            _dragAreaGenerate = GameObject.FindObjectOfType<DragAreaGenerate>();
            _copyTarget = GameObject.FindObjectOfType<CopyTarget>();
            _enemySprite = this.GetComponentInChildren<SpriteRenderer>();
            //EndCopy();
        }

        private void Update()
        {
            if (!this.CompareTag("Enemy")) return;

            if (Input.GetMouseButton(1))
            {
                if (_copyTarget.DragAreaContains(this.transform.position))
                {
                    CreatePreviewCopy();
                }
                else
                {
                    DeletePreviewCopy();
                }
            }

            if (Input.GetMouseButtonUp(1))
            {
                //Script Execution Orderで実行順をいじることで通している
                //あまり良い方法ではないけど動くから据え置き
                if (_copyTarget.DragAreaContains(this.transform.position))
                {
                    DeletePreviewCopy();

                    EndCopy();
                }
            }

        }

        private void CreatePreviewCopy()
        {
            if(!_canCopy) return;
            _canCopy = false;

            //プレビュー用のスプライト表示
            _previewInstance = Instantiate(copied);
            var trans = this.transform;
            _previewInstance.transform.position = trans.position;
            _previewInstance.transform.rotation = trans.rotation;
            _previewInstance.tag = "Untagged";
            
            //スプライトの強調
            //TODO:マテリアルのアルファをいじる？
            //TODO:少しのアニメーションがあったほうがいい
            _previewInstance.transform.localScale = Vector3.one * 1.2f;
        }

        private void DeletePreviewCopy()
        {
            if(_canCopy) return;
            _canCopy = true;
            
            GameObject.Destroy(_previewInstance);
        }

        private void EndCopy()
        {
            _canCopy = true;

            //ここに位置の保存処理
            var trans = this.transform;
            var worldPos = trans.localToWorldMatrix.GetPosition();
            _copyTarget.AddCopyPool(copied, worldPos, trans.rotation, !_enemySprite.flipX);
        }
    }
}

using System.Collections.Generic;
using UnityEngine;

namespace Game.Player
{
    /// <summary>
    /// プレイヤーのリスポーンをします
    /// </summary>
    public class PlayerRespawn : MonoBehaviour
    {
        [SerializeField] private List<Transform> savePoints = new List<Transform>();
        [SerializeField] private int nowSave = 0;
        [SerializeField] private float deathTime = 1f;
        private float _deathDeltaTime = 0;
        private bool _death = false;
        private PlayerMove _playerMove;

        private void Awake()
        {
            if(savePoints.Count <= 0)
                Debug.LogError("リスポーン地点を設定してください");
            if(deathTime <= 0)
                Debug.LogError("死亡の猶予時間を設定してください");
        }

        private void Start()
        {
            _playerMove = this.GetComponent<PlayerMove>();
        }

        private void Update()
        {
            SavePointUpdate();

            if (_death)
            {
                _deathDeltaTime += Time.deltaTime;
            }

            if (_deathDeltaTime >= deathTime)
            {
                Respawn();
            }
        }

        private void SavePointUpdate()
        {
            //セーブポイントを超えたらリスポーン位置を更新する
            var nextSave = nowSave + 1;
            //次のセーブポイントがなければなにもしない
            if(savePoints.Count <= nextSave) return;
            
            if (savePoints[nextSave].position.x <= this.transform.position.x)
            {
                nowSave = nextSave;
            }
        }

        public void GoDeath(Vector2 moveVector)
        {
            if(_death) return;
            _death = true;
            _playerMove.DamagedMove(moveVector);
        }

        private void Respawn()
        {
            _death = false;
            _deathDeltaTime = 0;
            _playerMove.CompleteRespawn();
            this.transform.position = savePoints[nowSave].position;
        }
    }
}

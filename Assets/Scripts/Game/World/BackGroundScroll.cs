using System;
using UnityEngine;

namespace Game.World
{
    public class BackGroundScroll : MonoBehaviour
    {
        private Camera _camera;
        [SerializeField] private float scrollSpeed = -1f;
        [SerializeField] private Vector3 resetPosition;
        private float _cashCameraPosX;

        private void Awake()
        {
            _camera = Camera.main;
        }

        private void Start()
        {
            _cashCameraPosX = _camera.transform.position.x;
        }

        private void Update()
        {
            //カメラの位置を追って移動する
            var cameraPosX = _camera.transform.position.x;
            var moveX = cameraPosX - _cashCameraPosX;
            var myPos = this.transform.position;
            myPos += new Vector3(moveX * scrollSpeed, 0, 0);
            _cashCameraPosX = cameraPosX;
            
            //無限スクロール用
            var diff = myPos.x - cameraPosX;
            if (diff > resetPosition.x)
            {
                myPos = -resetPosition + new Vector3(cameraPosX, 0, 0);
            }

            if (diff < -resetPosition.x)
            {
                myPos = resetPosition + new Vector3(cameraPosX, 0, 0);
            }

            this.transform.position = myPos;
        }
    }
}

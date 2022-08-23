using UnityEngine;

namespace Game.CandP
{
    /// <summary>
    /// マウスカーソルに常に追従するオブジェクトです
    /// </summary>
    public class CursorHold : MonoBehaviour
    {
        private Camera _camera;

        private void Start()
        {
            _camera = Camera.main;
        }

        void Update()
        {
            var mousePos = _camera.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0;
            this.transform.position = mousePos;
        }
    }
}

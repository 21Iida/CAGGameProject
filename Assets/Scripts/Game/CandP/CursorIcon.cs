using UnityEngine;

namespace Game.CandP
{
    /// <summary>
    /// UIのマウスカーソル画像を実際のマウスカーソルに合わせます
    /// </summary>
    public class CursorIcon : MonoBehaviour
    {
        private Camera _camera;
        private RectTransform _rectTransform;
        private RectTransform _myRectTransform;

        private void Start()
        {
            _camera = null;
            _rectTransform = transform.parent.GetComponent<RectTransform>();
            _myRectTransform = this.GetComponent<RectTransform>();
        }

        void Update()
        {
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                _rectTransform,
                Input.mousePosition,
                _camera,
                out var mousePos);

            _myRectTransform.anchoredPosition = mousePos;
        }
    }
}

using UnityEngine;

namespace Game.CandP
{
    /// <summary>
    /// マウスカーソルの表示切替と
    /// マウスカーソルがゲーム画面外へいかないようにします
    /// 離脱キー、設定画面展開などで解除
    /// </summary>
    public class CursorLimit : MonoBehaviour
    {
        [SerializeField] private GameObject escWindow;
        private void Awake()
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Confined;
            escWindow.SetActive(false);
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                CursorEsc();
            }
            if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
            {
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Confined;
            }
        }

        public void CursorEsc()
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            escWindow.SetActive(true);
        }
    }
}

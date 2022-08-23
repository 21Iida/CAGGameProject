using UnityEngine;
using UnityEngine.UI;

namespace Game.CandP
{
    /// <summary>
    /// ゲージ残量をUIに表示します
    /// ゲージの形は弧であり
    /// </summary>
    public class DragGage : MonoBehaviour
    {
        private Image _image;
        void Start()
        {
            _image = GetComponent<Image>();
        }

        public void GageUpdate(float maxVol,float remainingVol)
        {
            var nextValue = remainingVol / maxVol;
            _image.fillAmount = nextValue;
        }
    }
}

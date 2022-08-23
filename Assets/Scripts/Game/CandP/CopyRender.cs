using System;
using UnityEngine;
using UnityEngine.UI;

namespace Game.CandP
{
    public class CopyRender : MonoBehaviour
    {
        [SerializeField] private new Camera camera;
        [SerializeField] private RawImage _rawImage;
        private void Awake()
        {
            //AreaScreenShot();
        }

        public void AreaScreenShot()
        {
            RenderTexture renderTexture = new RenderTexture(512, 512,0);
            camera.targetTexture = renderTexture;
            
            camera.Render(); 
            var cache = RenderTexture.active;
            RenderTexture.active = renderTexture;
            
            Debug.Log("画像を更新しました" + renderTexture); 
        }
    }
}

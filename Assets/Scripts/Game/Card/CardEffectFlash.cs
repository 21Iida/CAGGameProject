using Game.Player;
using Game.World;
using UnityEngine;

namespace Game.Card
{
    public class CardEffectFlash : MonoBehaviour, ICardEffect
    {
        private new Camera camera;
        [SerializeField] private GameObject effect;
        [SerializeField] private AudioClip se;
        [SerializeField] private float lifeTime = 1;
        
        private void Start()
        {
            camera = Camera.main;
        }

        public void RunEffect()
        {
            var camTrans = camera.transform;
            var midPos = new Vector3(camTrans.position.x, camTrans.position.y, 0);
            var flash =
                GameObject.Instantiate(effect,
                    midPos,
                    Quaternion.identity,
                    camTrans);
            flash.GetComponent<OneShotEffect>().lifeTime = lifeTime;
        }
    }
}

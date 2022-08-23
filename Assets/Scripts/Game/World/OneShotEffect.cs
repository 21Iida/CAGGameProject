using System;
using UnityEngine;

namespace Game.World
{
    public class OneShotEffect : MonoBehaviour
    {
        public float lifeTime = 0.25f;

        private void Update()
        {
            lifeTime -= Time.deltaTime;
            if (lifeTime <= 0)
            {
                GameObject.Destroy(this.gameObject);
            }
        }
    }
}

using System;
using UnityEngine;

namespace Game.World
{
    public class CoinEffectMove : MonoBehaviour
    {
        [SerializeField] private float lifeTime;
        [SerializeField] private float moveSpeed;

        private void Update()
        {
            lifeTime -= Time.deltaTime;
            if (lifeTime <= 0)
            {
                GameObject.Destroy(this.gameObject);
            }
            
            transform.Translate(Vector3.up * (moveSpeed * Time.deltaTime));
        }
    }
}

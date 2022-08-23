using System;
using UnityEngine;

namespace Game.Enemy.Move
{
    public class TogeMove : MonoBehaviour
    {
        [SerializeField] float moveSpeed = 1f;
        private Rigidbody2D _rigidbody2D;

        private void Awake()
        {
            _rigidbody2D = this.GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            if(!_rigidbody2D.simulated) return;
            
            transform.position += transform.up * (-moveSpeed * Time.deltaTime);
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            if(!col.CompareTag("Ground")) return;
            GameObject.Destroy(this.gameObject);
        }
    }
}

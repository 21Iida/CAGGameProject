using System;
using Game.Player;
using Mono.Cecil.Cil;
using UnityEngine;

namespace Game.Enemy.Move
{
    public class TobiuoMove : MonoBehaviour, IEnemyMove
    {
        private Animator animator;
        private static int _hashAttack = Animator.StringToHash("Attack");
        
        private PlayerStats playerStats;
        private Vector3 targetPos;
        private Camera mainCam;
        private bool isAttackMode = false, attackComplete = false;
        private static readonly int TobiuoAttack = Animator.StringToHash("Attack");

        [SerializeField] private float rotSpeed = 5f, moveSpeed = 5f;

        private void Awake()
        {
            animator = GetComponent<Animator>();
            playerStats = GameObject.FindObjectOfType<PlayerStats>();
            mainCam = Camera.main;
        }

        private void Update()
        {
            if (isAttackMode && !attackComplete)
            {
                //プレイヤーとの距離
                var diff = this.transform.position - targetPos;
                
                //プレイヤーの方向を向く
                var targetRot = Quaternion.FromToRotation(Vector3.right, diff);
                this.transform.rotation = 
                    Quaternion.Slerp(transform.rotation,targetRot,Time.deltaTime * rotSpeed);
                
                //プレイヤーと離れているほど照準が甘い
                var nextVec = new Vector3();
                nextVec.y = -diff.y * (diff.y / 1.5f);
                nextVec.x = -diff.x  / (diff.y * diff.y * 2f);
                nextVec *= (Time.deltaTime * moveSpeed);
                this.transform.position += nextVec;

                if (diff.sqrMagnitude <= 1f) attackComplete = true;
            }

            if(attackComplete)
            {
                this.transform.position += -transform.right * (Time.deltaTime * moveSpeed);
            }
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.CompareTag("EnemyWakeUpArea"))
            {
                animator.SetBool(_hashAttack, true);
                targetPos = playerStats.transform.position;
                isAttackMode = true;
            }

            if (col.CompareTag("Player"))
            {
                attackComplete = true;
            }
        }

        /*
        private void OnTriggerExit2D (Collider2D col)
        {
            if(!col.CompareTag("CameraArea")) return;
            GameObject.Destroy(this.gameObject);
        }
        */

        public void Damage()
        {
            
        }
    }
}

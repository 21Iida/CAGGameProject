using System.Collections;
using UnityEngine;

namespace Game.Player
{
    /// <summary>
    /// プレイヤーがダメージを受けます
    /// 敵に当たると即死します
    /// </summary>
    public class PlayerDamage : MonoBehaviour
    {
        [SerializeField] private PlayerMove playerMove;
        [SerializeField] private PlayerRespawn playerRespawn;
        [SerializeField] private SpriteRenderer playerSprite;
        private bool canDeath = true, isFlash = false;

        private void Update()
        {
            //無敵時の点滅
            if(isFlash) {
                var spAlpha = Mathf.Abs(Mathf.Sin(Time.time * 10));
                playerSprite.color = new Color(1,1,1,spAlpha);
            }
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            Vector2 moveVector;
            if (this.transform.position.x < col.transform.position.x)
            {
                moveVector = Vector2.left;
            }
            else
            {
                moveVector = Vector2.right;
            }

            if (col.CompareTag("Dead"))
            {
                playerRespawn.GoDeath(moveVector);
                StopAllCoroutines();
                StartCoroutine(RespawnFlash(3.0f));
            }
            if(!canDeath) return;
            if (col.CompareTag("Enemy"))
            {
                playerRespawn.GoDeath(moveVector);
                StartCoroutine(RespawnFlash(3.0f));
            }
        }
        
        private IEnumerator RespawnFlash(float lifeTime)
        {
            canDeath = false;
            isFlash = true;
            
            yield return new WaitForSeconds(lifeTime);
            
            ResetSprite();
        }

        private void ResetSprite()
        {
            canDeath = true;
            isFlash = false;
            playerSprite.color = new Color(1f, 1f, 1f, 1f);
        }

        public void Shield(float lifeTime)
        {
            StartCoroutine(RespawnFlash(lifeTime));
            isFlash = false;
        }
    }
}

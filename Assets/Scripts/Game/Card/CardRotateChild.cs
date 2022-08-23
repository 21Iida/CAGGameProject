using UnityEngine;

namespace Game.Card
{
    /// <summary>
    /// カード表示が常に上を向いてくれる
    /// 微妙な更新の差がランダムな傾きの動きになっている
    /// </summary>
    public class CardRotateChild : MonoBehaviour
    {
        private void Update()
        {
            transform.rotation = Quaternion.Euler(Vector3.up);
        }
    }
}

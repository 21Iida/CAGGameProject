using UnityEngine;

namespace Game.Card
{
    public class CardPopIcon : MonoBehaviour
    {
        [SerializeField] private GameObject iconPrefab;
        public GameObject GetIconPrefab => iconPrefab;
    }
}

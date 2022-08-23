using UnityEngine;

namespace Title
{
    public class WindowOpen : MonoBehaviour
    {
        [SerializeField] private GameObject targetObject;

        public void Open()
        {
            targetObject.SetActive(true);
        }
    }
}

using UnityEngine;

namespace Title
{
    public class WindowClose : MonoBehaviour
    {
        [SerializeField] private GameObject targetObject;
        public void Close()
        {
            targetObject.SetActive(false);
        }
    }
}

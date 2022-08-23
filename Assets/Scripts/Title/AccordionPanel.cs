using System;
using UnityEngine;
using UnityEngine.UI;

namespace Title
{
    public class AccordionPanel : MonoBehaviour
    {
        [SerializeField] private GameObject targetPanel;
        private bool _openPanel = false;

        private void Start()
        {
            targetPanel.SetActive(false);
        }

        public void OnClickPanel()
        {
            if (_openPanel)
            {
                targetPanel.SetActive(false);
                _openPanel = false;
            }
            else
            {
                targetPanel.SetActive(true);
                _openPanel = true;
            }
        }
    }
}

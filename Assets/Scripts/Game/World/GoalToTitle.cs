using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game.World
{
    public class GoalToTitle : MonoBehaviour
    {
        private void Update()
        {
            if (Input.GetButtonDown("Jump"))
            {
                GoTitle();
            }
        }

        public void GoTitle()
        {
            SceneManager.LoadScene("MainTitleScene");
        }
    }
}

using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Title
{
    public class GameLoad : MonoBehaviour
    {
        private void Update()
        {
            if (Input.GetButtonDown("Jump"))
            {
                SceneManager.LoadScene("MainGameScene");
            }
        }
    }
}

using UnityEngine;
using UnityEngine.SceneManagement;

namespace General
{
    public class LoadScene : MonoBehaviour
    {
        public void LoadGame()
        {
            Time.timeScale = 1;
            SceneManager.LoadScene("GameScene");
        }

        public void LoadTitle()
        {
            Time.timeScale = 1;
            SceneManager.LoadScene("TitleScene");
        }
    }
}


using UnityEngine;
using UnityEngine.SceneManagement;

namespace General
{
    /// <summary>
    /// ボタンによるシーン移動
    /// </summary>
    public class SceneSwitch : MonoBehaviour
    {
        public void GoTitle()
        {
            SceneManager.LoadScene("Title");
        }
        public void GoLocalMode()
        {
            SceneManager.LoadScene("LocalMode");
        }
        public void GoHomeMode()
        {
            SceneManager.LoadScene("ModelViewerMode");
        }
    }
}

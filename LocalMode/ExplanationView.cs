using UnityEngine;

namespace LocalMode
{
    /// <summary>
    /// オブジェクトについての解説ウィンドウを出したり消したりします
    /// ExplanationMessageが使ってます
    /// </summary>
    public class ExplanationView : MonoBehaviour
    {
        [SerializeField] private GameObject windowPrefab;

        private void Start()
        {
            windowPrefab.SetActive(false);
        }

        public void WindowSwitch()
        {
            if (windowPrefab.activeSelf)
            {
                CloseWindow();
            }
            else
            {
                OpenWindow();
            }
        }

        private void OpenWindow()
        {
            windowPrefab.SetActive(true);
        }

        private void CloseWindow()
        {
            windowPrefab.SetActive(false);
        }
    }
}

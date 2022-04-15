using UnityEngine;

namespace LocalMode
{
    /// <summary>
    /// 百箇日法要の再生/停止を行うボタン部分
    /// </summary>
    public class uiHyakunichi : MonoBehaviour
    {
        [SerializeField] private GameObject playButtonObj, pauseButtonObj;

        public void PlayToPause()
        {
            playButtonObj.SetActive(false);
            pauseButtonObj.SetActive(true);
        }

        public void PauseToStart()
        {
            pauseButtonObj.SetActive(false);
            playButtonObj.SetActive(true);
        }
    }
}

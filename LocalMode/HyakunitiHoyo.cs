using UnityEngine;
using UnityEngine.Playables;

namespace LocalMode
{
    /// <summary>
    /// 百箇日法要イベントの実行部分です
    /// </summary>
    public class HyakunitiHoyo : MonoBehaviour
    {
        [SerializeField] private PlayableDirector playableDirector;

        [SerializeField] private GameObject[] playabledObjects;
    
        public void HoyoStart()
        {
            playableDirector.Play();
        }

        public void HoyoEnd()
        {
            AllOffActive();
            playableDirector.Stop();
        }

        void AllOffActive()
        {
            foreach (var item in playabledObjects)
            {
                item.SetActive(false);
            }
        }
    }
}

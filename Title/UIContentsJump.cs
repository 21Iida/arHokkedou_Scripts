using UnityEngine;
using UnityEngine.UI;

namespace Title
{
    /// <summary>
    /// 目次のボタンを押すと該当箇所までジャンプする機能です
    /// </summary>
    public class UIContentsJump : MonoBehaviour
    {
        [SerializeField] private Scrollbar scrollbar;

        public void JumpTo(float jumpValue)
        {
            scrollbar.value = jumpValue;
        }
    }
}

using LocalMode;
using UnityEngine;
using UnityEngine.UI;

namespace General
{
    /// <summary>
    /// デバッグ用です
    /// 実機実行時にエディタ機能の表示非表示を行います
    /// </summary>
    public class DebugMode : MonoBehaviour
    {
        private SingletonData _singletonData;
        [SerializeField] private GameObject runtimeEditor;
        [SerializeField] private Toggle debugToggleOn,debugToggleOff;
        [SerializeField] private CheckTriArea checkTriArea;
    
        private void Awake()
        {
            _singletonData = FindObjectOfType<SingletonData>();
        
            //UIの更新確認
            if(!debugToggleOn || !debugToggleOff) return;
            if (_singletonData.IsDebugMode)
            {
                debugToggleOff.isOn = false;
                debugToggleOn.isOn = true;
            }
            else
            {
                debugToggleOn.isOn = false;
                debugToggleOff.isOn = true;
            }
        }

        private void Start()
        {
            //デバッグウィンドウの展開をするかしないか
            if (!runtimeEditor) return;
            if (_singletonData.IsDebugMode)
            {
                OpenDebugWindow();
                DisableStairSystem();
            }
            else
            {
                CloseDebugWindow();
                EnableStairSystem();
            }
        }

        private void OpenDebugWindow()
        {
            runtimeEditor.SetActive(true);
        }

        private void CloseDebugWindow()
        {
            runtimeEditor.SetActive(false);
        }

        private void EnableStairSystem()
        {
            checkTriArea.enabled = true;
        }

        private void DisableStairSystem()
        {
            checkTriArea.enabled = false;
        }

        public void ToggleDebugOn(Toggle toggle)
        {
            if (toggle.isOn) _singletonData.IsDebugMode = true;
        }

        public void ToggleDebugOff(Toggle toggle)
        {
            if (toggle.isOn) _singletonData.IsDebugMode = false;
        }
    }
}

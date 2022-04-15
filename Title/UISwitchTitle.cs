using DG.Tweening;
using General;
using UnityEngine;
using UnityEngine.UI;
using Button = UnityEngine.UI.Button;

namespace Title
{
    /// <summary>
    /// タイトル画面にて、ボタンの入力を受け取って画面遷移を行います
    /// </summary>
    public class UISwitchTitle : MonoBehaviour
    {
        [SerializeField] private GameObject startCanvas,titleCanvas,settingCanvas,usingCanvas,infoCanvas,audioOffCanvas;
        [SerializeField] private RectTransform settingItems,usingItems,infoItems;
        private SingletonData _singletonData;
        [SerializeField] private ApplyAudioMute applyAudioMute;
        private void Awake()
        {
            UIAllOff();
            //すでに音量確認を終えているならタイトル画面に直接飛ぶ
            _singletonData = FindObjectOfType<SingletonData>();
            if (_singletonData.IsAudioChecked)
            {
                UIAllOff();
                titleCanvas.SetActive(true);
            }
            else
            {
                UIAllOff();
                startCanvas.SetActive(true);
            }
        
        }
    
        public void TitleOpen(Button button)
        {
            _singletonData.IsAudioChecked = true;
        
            //rootオブジェクトを非表示にします
            //なのでCanvasオブジェクトより上にオブジェクトを置かないでください
            var rootObj = button.transform.root.gameObject;
            rootObj.transform.GetChild(0).GetComponent<RectTransform>().DOLocalMoveX(1000, 0.4f).SetEase(Ease.OutQuad);
            titleCanvas.SetActive(true);
        }

        public void ResetViewPosition(ScrollRect scrollRect)
        {
            //スクロールビュー内のオブジェクトの位置をリセット
            //MovementTypeをClampedにすることで慣性を消していないことを誤魔化しています
            //Inertiaがオンなため、スクロールに微妙に慣性が残っていますが、大きな問題ではないので放置
            DOVirtual.DelayedCall(0.4f, () => scrollRect.content.localPosition = Vector3.zero);
        }

        public void AudioOffMessage(Button button)
        {
            applyAudioMute.MuteAudioSe();
            applyAudioMute.MuteAudioVoice();
            _singletonData.IsAudioChecked = true;
        
            button.transform.parent.gameObject.SetActive(false);
            audioOffCanvas.SetActive(true);
        }
        public void SettingOpen()
        {
            titleCanvas.SetActive(false);
            settingCanvas.SetActive(true);
            //バグ回避用
            DOVirtual.DelayedCall(0.4f, () => settingCanvas.SetActive(true));
            settingItems.anchoredPosition = new Vector2(1000, 0);
            settingItems.DOLocalMoveX(0, 0.4f);
        }
        public void UsingOpen()
        {
            titleCanvas.SetActive(false);
            usingCanvas.SetActive(true);
            DOVirtual.DelayedCall(0.4f, () => usingCanvas.SetActive(true));
            usingItems.anchoredPosition = new Vector2(1000, 0);
            usingItems.DOLocalMoveX(0, 0.4f);
        }
        public void InfoOpen()
        {
            titleCanvas.SetActive(false);
            infoCanvas.SetActive(true);
            DOVirtual.DelayedCall(0.4f, () => infoCanvas.SetActive(true));
            infoItems.anchoredPosition = new Vector2(1000, 0);
            infoItems.DOLocalMoveX(0, 0.4f);
        }

        private void UIAllOff()
        {
            startCanvas.SetActive(false);
            settingCanvas.SetActive(false);
            usingCanvas.SetActive(false);
            audioOffCanvas.SetActive(false);
            titleCanvas.SetActive(false);
        }
    
    }
}

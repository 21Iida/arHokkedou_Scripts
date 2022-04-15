using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

namespace LocalMode
{
    /// <summary>
    /// QRマーカーについての案内用テキストウィンドウの切替です
    /// QRマーカーを読んで　→　カメラを上に向けて　→　現地を歩き回って　の順です
    /// </summary>
    public class QRDialog : MonoBehaviour
    {
        [SerializeField] private GameObject dialogWindow;
        [SerializeField] private TextMeshProUGUI tmp;
        [Multiline][SerializeField] private string upCamera, walkAR;
        [SerializeField] private ARTrackedImageManager imageManager;
        private bool _qrFlag = false;
        [SerializeField] private GameObject frameUI;

        private void OnEnable()
        {
            imageManager.trackedImagesChanged += StartDialog;
        }

        private void OnDisable()
        {
            imageManager.trackedImagesChanged -= StartDialog;
        }
    
        private void StartDialog(ARTrackedImagesChangedEventArgs eventArgs)
        {
            //現在新しく取ったものがその瞬間だけ入るもの
            foreach (var trackedImage in eventArgs.added)
            {
                UpdateTrackAR(trackedImage);
            }
            //過去の分を取得して更新があれば入るもの
            foreach (var trackedImage in eventArgs.updated)
            {
                UpdateTrackAR(trackedImage);
            }
        }
    
        private void UpdateTrackAR(ARTrackedImage trackedImage)
        {
            if (trackedImage.trackingState != TrackingState.Tracking) return;

            if (_qrFlag) return;
            _qrFlag = true;
        
            dialogWindow.SetActive(true);
            frameUI.SetActive(false);

            Sequence sequence = DOTween.Sequence()
                .InsertCallback(0f, () =>
                    {
                        tmp.text = upCamera;
                    }
                )
                .InsertCallback(4.5f, () =>
                    {
                        tmp.text = walkAR;
                    }
                )
                .InsertCallback(9f, () =>
                    {
                        _qrFlag = false;
                        dialogWindow.SetActive(false);
                    }
                );
            sequence.Play();
        }
    }
}

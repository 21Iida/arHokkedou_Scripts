using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

namespace LocalMode
{
    /// <summary>
    /// 寺の方向を向いてれる矢印を生成します
    /// </summary>
    public class GuidArrowLife : MonoBehaviour
    {
        [SerializeField] private GameObject guideArrowObj;
        [SerializeField] private ARTrackedImageManager imageManager;

        private void Awake()
        {
            guideArrowObj.SetActive(false);
        }

        private void OnEnable()
        {
            imageManager.trackedImagesChanged += OnTrackedImagesChanged;
        }

        private void OnDisable()
        {
            imageManager.trackedImagesChanged -= OnTrackedImagesChanged;
        }
    
        private void OnTrackedImagesChanged(ARTrackedImagesChangedEventArgs eventArgs)
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
        
            guideArrowObj.SetActive(true);
        }
    }
}

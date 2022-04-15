using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

//参考
//https://qiita.com/OKsaiyowa/items/29504242ec74cb5dfb04
namespace LocalMode
{
    /// <summary>
    /// 複数のARマーカーを読み込んで、対応した位置にモデルを表示します
    /// </summary>
    public class ARMultiMarker : MonoBehaviour
    {
        [SerializeField] private GameObject templePrefab;
        [SerializeField] private Transform[] arPoints;
        [SerializeField] private Transform qrPositionRoot;
        [SerializeField] private ARTrackedImageManager imageManager;
        private int _nowIndex = 0;
    
        private void Start()
        {
            templePrefab.SetActive(false);
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
            //現在取ったものがその瞬間だけ入るもの
            foreach (var trackedImage in eventArgs.added)
            {
                UpdateTrackAR(trackedImage);
            }
            //過去に取ったものが全部入ってる場所(シーンを切り替えても残る)
            foreach (var trackedImage in eventArgs.updated)
            {
                UpdateTrackAR(trackedImage);
            }
        }
    
        private void UpdateTrackAR(ARTrackedImage trackedImage)
        {
        
            if (trackedImage.trackingState == TrackingState.None) return;
            if (trackedImage.trackingState != TrackingState.Tracking) return;

            switch (trackedImage.referenceImage.name)
            {
                case "QR_01":
                    ActiveAndPositionSet(trackedImage.transform,1);
                    break;
                /*
                case "QR_02":
                    ActiveAndPositionSet(trackedImage.transform,1);
                    break;
                */
                case "QR_03":
                    ActiveAndPositionSet(trackedImage.transform,2);
                    break;
                case "QR_debug":
                    ActiveAndPositionSet(trackedImage.transform,3);
                    break;
                case "Guide_01":
                    UpdateArPosition(trackedImage.transform, _nowIndex);
                    break;
                case "Guide_02":
                    break;
                case "Guide_03":
                    break;
                case "Guide_04":
                    break;
                case "QR_dualLink":
                    ActiveAndPositionSet(trackedImage.transform,0);
                    break;
                default:
                    break;
            }
        }

        private void ActiveAndPositionSet(Transform trackedTrans,int qrIndex)
        {
            //法華堂の表示
            templePrefab.SetActive(true);
            
            //オブジェクトを指定した位置に合わせる
            templePrefab.transform.SetParent(arPoints[qrIndex]);
            templePrefab.transform.localPosition = Vector3.zero;
            templePrefab.transform.localRotation = Quaternion.identity;
            
            //ワールドの中心をマーカーに合わせる
            qrPositionRoot.position = trackedTrans.position;
            qrPositionRoot.rotation = Quaternion.Euler(0,trackedTrans.rotation.eulerAngles.y,0);

            _nowIndex = qrIndex;
        }

        private void UpdateArPosition(Transform trackedTrans, int qrIndex)
        {
            var tPos = trackedTrans.position;
            arPoints[qrIndex].position = new Vector3(tPos.x, arPoints[qrIndex].position.y, tPos.z);
            arPoints[qrIndex].rotation = Quaternion.Euler(0,trackedTrans.rotation.eulerAngles.y,0);
        }
    }
}

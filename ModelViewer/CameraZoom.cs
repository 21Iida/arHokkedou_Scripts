using UnityEngine;

namespace ModelViewer
{
    /// <summary>
    /// プラスボタンとマイナスボタンを入力として
    /// カメラとモデルの距離を変更します
    /// </summary>
    public class CameraZoom : MonoBehaviour
    {
        //配列の後ろの方がカメラの寄っている状態
        [SerializeField] private Transform[] camPos;
        [SerializeField] private Transform mainCam;
        private int _cameraPositionNum;

        private void Start()
        {
            mainCam.SetParent(camPos[0]);
        }

        public void CameraZoomIn()
        {
            _cameraPositionNum++;
            if (_cameraPositionNum >= camPos.Length)
            {
                _cameraPositionNum = camPos.Length - 1;
            }
            mainCam.SetParent(camPos[_cameraPositionNum]);
            mainCam.localPosition = Vector3.zero;
            mainCam.localRotation = Quaternion.identity;
        }

        public void CameraZoomOut()
        {
            _cameraPositionNum--;
            if (_cameraPositionNum < 0)
            {
                _cameraPositionNum = 0;
            }
            mainCam.SetParent(camPos[_cameraPositionNum]);
            mainCam.localPosition = Vector3.zero;
            mainCam.localRotation = Quaternion.identity;
        }
    }
}

using UnityEngine;

namespace ModelViewer
{
    /// <summary>
    /// ビルボードです
    /// 単純にカメラの方向を向きます
    /// </summary>
    public class LookCameraWindow : MonoBehaviour
    {
        private void Update()
        {
            transform.LookAt(Camera.main.transform.position);
        }

        private void OnEnable()
        {
            transform.LookAt(Camera.main.transform.position);
        }
    }
}

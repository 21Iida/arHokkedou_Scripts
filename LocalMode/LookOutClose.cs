using UnityEngine;

namespace LocalMode
{
    /// <summary>
    /// 如意輪観音像オブジェクトがプレイヤーの近くに居て、カメラの中央に映っている場合、
    /// 解説用のウィンドウを表示させます
    /// </summary>
    public class LookOutClose : MonoBehaviour
    {
        [SerializeField] private Transform mainCamera;
        [SerializeField] private GameObject namePlate;
        [SerializeField] private float distance,angle;

        private void Update()
        {
            if (!(IsOnCamera() && IsNearPlayer()))
            {
                namePlate.SetActive(true);
                this.gameObject.SetActive(false);
            }
        }
    
        private bool IsOnCamera()
        {
            var justVec = (this.transform.position - mainCamera.position).normalized;
            var nowVec = mainCamera.forward;

            return Mathf.Abs(Vector3.SignedAngle(nowVec, justVec,mainCamera.up)) <= angle;
        }
    
        private bool IsNearPlayer()
        {
            var sqrDis = (this.transform.position - mainCamera.position).sqrMagnitude;

            return sqrDis <= Mathf.Pow(distance,2);
        }
    }
}

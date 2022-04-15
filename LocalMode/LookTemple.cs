using UnityEngine;

namespace LocalMode
{
    /// <summary>
    /// ARスタート直後の案内用矢印が目標に向いてくれる機能です
    /// </summary>
    public class LookTemple : MonoBehaviour
    {
        [SerializeField] private Transform cameraTransform;
        [SerializeField] private Transform targetTransform;
        [SerializeField] private Transform imageTransform;
        [SerializeField] private float marginAngle;
        private GameObject _arrowObj;

        private void Start()
        {
            _arrowObj = this.gameObject;
        }

        private void Update()
        {
            LookArrowRot(cameraTransform, targetTransform.position);
            //LookAtArrow();
        }
    
        //ほしい矢印の方向を返してくれる
        private void LookArrowRot(Transform cameraTrans,Vector3 targetVec)
        {
            var justVec = (targetVec - cameraTrans.position).normalized;
            var nowVec = cameraTrans.forward;
        
            //もし十分に目的の角度なら矢印(自身)を消す
            //再出現はGuidArrowLifeが担当
            if (Mathf.Abs(Vector3.SignedAngle(nowVec,justVec,cameraTrans.up)) <= marginAngle)
            {
                _arrowObj.SetActive(false);
                return;
            }

            var justArrow = (justVec - nowVec).normalized;
            var rot = Quaternion.FromToRotation(_arrowObj.transform.up, justArrow);
        
            _arrowObj.transform.rotation = rot * _arrowObj.transform.rotation;
        
            _arrowObj.transform.localRotation = Quaternion.Euler(0,0,_arrowObj.transform.localRotation.eulerAngles.z);
        
        
        }
    }
}

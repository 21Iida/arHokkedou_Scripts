using UnityEngine;

namespace LocalMode
{
    public class NearLookOpen : MonoBehaviour
    {
        [SerializeField] private Transform mainCamera;
        [SerializeField] private GameObject targetWindow,namePlane;
        [SerializeField] private float distance, angle;

        private void Update()
        {
            if (IsOnCamera() && IsNearPlayer())
            {
                targetWindow.SetActive(true);
                namePlane.SetActive(false);
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

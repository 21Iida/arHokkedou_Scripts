using UnityEngine;

namespace ModelViewer
{
    public class CameraRot : MonoBehaviour
    {
        [SerializeField] private float rotateSpeed = 30.0f;
        private Vector2 _beforePoint, _nowPoint, _diff;
        private float _horizontalAngle;

        void Update()
        {
            if (Input.touchCount != 1) return;
            if (Input.touchCount > 0)
            {
                if (Input.GetTouch(0).phase == TouchPhase.Began)
                {
                    _beforePoint = Input.GetTouch(0).position;
                }
            }

            if (Input.GetTouch(0).phase != TouchPhase.Moved) return;
            _nowPoint = Input.GetTouch(0).position;
            
            if (_nowPoint.x - _beforePoint.x == 0) return;
            _horizontalAngle = _nowPoint.x - _beforePoint.x;
            _horizontalAngle *= rotateSpeed * Time.deltaTime;

            this.transform.Rotate(0, _horizontalAngle, 0);

            _beforePoint = _nowPoint;
        }
    }
}

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

namespace LocalMode
{
    /// <summary>
    /// オブジェクトをタップしたら解説ウィンドウを出します
    /// もう一度タップすると解説ウィンドウを消します
    /// </summary>
    public class ExplanationMessage : MonoBehaviour
    {
        [SerializeField] private Camera arCamera;
        [SerializeField] private List<ARRaycastHit> _raycastHits = new List<ARRaycastHit>();

        private void Update()
        {
            if (Input.touchCount <= 0) return;

            var touch = Input.GetTouch(0);
            if (touch.phase != TouchPhase.Began) return;
            var ray = arCamera.ScreenPointToRay(touch.position);

            var hasHit = Physics.Raycast(ray, out var hit);
            if (!hasHit) return;
            var target = hit.collider.gameObject;
            //ここに処理のトリガーを置く
            //メッセージ用オブジェクトの持つレシーバーで処理のする/しない
            if (target.CompareTag("ExplanationItem"))
            {
                target.GetComponent<ExplanationView>().WindowSwitch();
            }
        }
    }
}

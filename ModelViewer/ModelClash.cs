using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace ModelViewer
{
    /// <summary>
    /// モデルがタップされたら表示を切り替えます
    /// 基本的にはどんどんとパーツが消えていき、最後にタップするとリセットされます
    /// </summary>
    public class ModelClash : MonoBehaviour
    {
        [SerializeField] private GameObject[] hallParts;
        [SerializeField] private GameObject[] commentaryWindows;
        [SerializeField] private ChangeLightMap changeLightMap;
        private int _clashCount = 0;
        [SerializeField] private AudioSource audioSource;
        [SerializeField] private AudioClip audioClip;

        private Vector2 _touchBegan, _touchEnd;

        private void Start()
        {
            AllHall(true, false);
        }

        private void Update()
        {
            //タップじゃなければ(スワイプなら)拒否
            if (!IsSingleTouch()) return;

            //UI以外のタッチを通す
            if (IsTouchUI(Input.GetTouch(0).position)) return;

            //タップしているモノがHallか、カメラがめり込んでいる場合のみ続行
            if (!(IsHitHall() || IsOverlapHall())) return;
            if (_clashCount >= hallParts.Length)
            {
                ResetHall();
                return;
            }

            //タップされたので、モデルとウィンドウの表示切替を行う
            MeshEnable(_clashCount, false);
            ChangeCommentWindow(_clashCount);
            audioSource.PlayOneShot(audioClip);
            _clashCount++;
        }

        //タップの判定を取る
        //スワイプでもなく、UI上でもなく、モデルをタップしたときのみtrueを返す
        private bool IsSingleTouch()
        {
            if (Input.touchCount <= 0) return false;

            var touch = Input.GetTouch(0);
            var touchDis = 0.0f;
            
            if (touch.phase == TouchPhase.Began)
            {
                _touchBegan = touch.position;
            }
            if (touch.phase == TouchPhase.Ended)
            {
                _touchEnd = touch.position;
                touchDis = (_touchBegan - _touchEnd).sqrMagnitude;
            }

            const int touchSize = 50;
            if (touchDis > Mathf.Pow(touchSize,2)) return false;

            return true;
        }
        private bool IsTouchUI(Vector2 vec)
        {
            //参考<https://ninagreen.hatenablog.com/entry/2016/06/27/222855>
            var eventDataCurrentPosition = new PointerEventData(EventSystem.current);
            eventDataCurrentPosition.position = vec;
            var results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
            return results.Count > 0;
        }
        private bool IsHitHall()
        {
            var touch = Input.GetTouch(0);
            if (touch.phase != TouchPhase.Ended) return false;
            
            var touchPoint = touch.position;
            var ray = Camera.main.ScreenPointToRay(touchPoint);
            if (!Physics.Raycast(ray, out var hit)) return false;
        
            if(!hit.collider.CompareTag("Hall")) return false;
            
            return true;
        }
        private bool IsOverlapHall()
        {
            var touch = Input.GetTouch(0);
            if (touch.phase != TouchPhase.Ended) return false;
            
            var sphereRadius = 0.1f;
            return Physics.CheckSphere(Camera.main.transform.position, sphereRadius);
        }
        
        //モデルの表示切替
        //ライトマップの切替を同時に行う
        private void MeshEnable(int index, bool enable)
        {
            var childMeshRenderer = hallParts[index].GetComponentsInChildren<MeshRenderer>();
            foreach (var rend in childMeshRenderer)
            {
                rend.enabled = enable;
            }
            var childCanvas = hallParts[index].GetComponentsInChildren<Canvas>();
            foreach (var canvas in childCanvas)
            {
                canvas.enabled = enable;
            }
            changeLightMap.Change(_clashCount);
        }
        
        //解説ウィンドウの表示切替
        private void ChangeCommentWindow(int clashNum)
        {
            if(clashNum != 0) commentaryWindows[clashNum-1].SetActive(false);
            commentaryWindows[clashNum].SetActive(true);
        }

        public void ResetHall()
        {
            AllHall(true, false);
        }
        
        //モデル、ウィンドウ表示の一括処理
        private void AllHall(bool hallActive,bool windowActive)
        {
            for (var i = 0; i < hallParts.Length; i++)
            {
                MeshEnable(i, hallActive);
            }

            foreach (var cw in commentaryWindows)
            {
                cw.SetActive(windowActive);
            }

            changeLightMap.Change(hallActive ? 0 : changeLightMap.MapsLength()-1);

            if (hallActive)
            {
                _clashCount = 0;
            }
        }
    }
}

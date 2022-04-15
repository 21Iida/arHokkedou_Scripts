using DG.Tweening;
using UnityEngine;

namespace LocalMode
{
    /// <summary>
    /// 百箇日法要のイベント再生エリアであることを判定します
    /// その結果、UIの表示切替を行います
    /// </summary>
    public class HyakunitiArea : MonoBehaviour
    {
        [SerializeField] private RectTransform rect;
        [SerializeField] private HyakunitiHoyo hyakunitiHoyo;
        [SerializeField] private uiHyakunichi uiHyakunichi;
    
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                PopUpUI();
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                PopDownUI();
                hyakunitiHoyo.HoyoEnd();
                uiHyakunichi.PauseToStart();
            }
        }

        private void PopUpUI()
        {
            rect.DOAnchorPosX(-10, 0.2f).SetEase(Ease.InQuad);
        }

        private void PopDownUI()
        {
            rect.DOAnchorPosX(170, 0.2f).SetEase(Ease.OutQuad);
        }
    
    }
}

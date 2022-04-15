using UnityEngine;

namespace LocalMode
{
    /// <summary>
    /// 移動時に歩く効果音を出します
    /// </summary>
    public class WalkSe : MonoBehaviour
    {
        [SerializeField] private AudioSource audioSource;
        [SerializeField] private AudioClip audioClip;
        [SerializeField] private GameObject player;
        private Vector3 _cachePos = Vector3.zero;
        private float _walkCount = 0.0f;
        private readonly float _walkValue = 0.5f;
    
        private void Start()
        {
            _walkCount = 0.0f;
        }

        private void Update()
        {
            var dis = player.transform.position - _cachePos;
            _walkCount += dis.sqrMagnitude;
            if (_walkCount > Mathf.Pow(_walkValue, 2))
            {
                PlaySe();
                _walkCount = 0;
            }
            _cachePos = player.transform.position;
        }

        private void PlaySe()
        {
            audioSource.PlayOneShot(audioClip);
        }
    }
}

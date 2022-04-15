using UnityEngine;

namespace ModelViewer
{
    public class BindActive : MonoBehaviour
    {
        [SerializeField] private GameObject target;

        private void OnEnable()
        {
            target.SetActive(true);
        }

        private void OnDisable()
        {
            target.SetActive(false);
        }
    }
}

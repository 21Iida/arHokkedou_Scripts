using UnityEngine;

namespace General
{
    /// <summary>
    /// フラグ保存用シングルトン
    /// </summary>
    public class SingletonData : MonoBehaviour
    {
        //シングルトンの定型文
        private static SingletonData _instance;
        private static SingletonData Instance
        {
            get
            {
                if (_instance != null) return _instance;
                _instance = (SingletonData) FindObjectOfType(typeof(SingletonData));
                if (_instance != null) return _instance;
                Debug.Log("SingletonData Instance Error");

                return _instance;
            }
        }

        private void Awake()
        {
            if (this != Instance)
            {
                Destroy(this.gameObject);
                return;
            }
            DontDestroyOnLoad(this.gameObject);
        }
    
        //ここから中身
        //最初の音量確認が過ぎればずっとtにする
        public bool IsAudioChecked { get; set; }
        public bool IsDebugMode { get; set; }

    }
}

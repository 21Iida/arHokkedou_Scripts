using UnityEngine;

namespace ModelViewer
{
    /// <summary>
    /// モデルを切り替えたとき、ベイク済みのライトマップを切り替えます
    /// </summary>
    //参考<http://corevale.com/unity/6307>
    public class ChangeLightMap : MonoBehaviour
    {
        [SerializeField] private Texture2D[] maps;
        void Start()
        {
            Change(0);
        }

        public int MapsLength()
        {
            return maps.Length;
        }

        public void Change(int index)
        {
            if (index >= maps.Length)
            {
                index = 0;
                return;
            }

            var mapData = new LightmapData
            {
                lightmapColor = maps[index]
            };
            var mapsData = new LightmapData[1];
            mapsData[0] = mapData;
            LightmapSettings.lightmaps = mapsData;
        
        }
    }
}

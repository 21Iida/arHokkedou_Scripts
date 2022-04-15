using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;

namespace LocalMode
{
    /// <summary>
    /// ARで表示させる3Dモデルの階段部分の当たり判定です
    /// </summary>
    [RequireComponent(typeof(MeshFilter))]
    [RequireComponent(typeof(MeshRenderer))]
    public class TriColliMesh : MonoBehaviour
    { 
        // 頂点リストを作成
        [SerializeField] List<Vector3> vertices = new List<Vector3>
        {
            new Vector3(1.0f, 0.0f, 1.0f),
            new Vector3(1.0f, 0.0f, -1.0f),
            new Vector3(-1.0f, 0.0f, -1.0f),
            new Vector3(-1.0f, 0.0f, 1.0f),
            new Vector3(0.0f, 3.0f, 0.0f),
        };

        //渡すときにグローバルに変換してから渡す
        public List<Vector3> GetGlobalVertices()
        {
            var trans = this.transform;
            return vertices.Select(item => trans.TransformPoint(item)).ToList();
        }

        [Button]
        private void CreateTriMesh()
        {
            var mesh = new Mesh();
        
            // 三角錐のインデックスリスト
            var triangles = new List<int>
            {
                //底面
                0, 3, 2,
                2, 1, 0,

                //側面
                0, 1, 4,
                1, 2, 4,
                2, 3, 4,
                3, 0, 4
            };

            mesh.SetVertices(vertices);
            mesh.SetTriangles(triangles, 0);

            // メッシュフィルターに適応
            var meshFilter = GetComponent<MeshFilter>();
            meshFilter.mesh = mesh;
        
        }
    }
}

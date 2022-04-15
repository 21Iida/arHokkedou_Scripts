using UnityEngine;

namespace LocalMode
{
    /// <summary>
    /// 法華堂の階段部分の当たり判定を取ります
    /// 判定内にて、モデルを階段の位置に合わせます
    /// 正直レイ飛ばしてポリゴンの高さを取った方が良かった…
    /// </summary>
    public class CheckTriArea : MonoBehaviour
    {
        [SerializeField] private TriColliMesh triColliMesh;
        [SerializeField] private Transform upLimit;
        [SerializeField] private Transform playerTrans;
        [SerializeField] private GameObject worldObj;
        private float _nowUp = 0.0f;
        //直前のエリア判定を保存
        private bool _afterMove = false;

        private void Update()
        {
            var vecs = triColliMesh.GetGlobalVertices().ToArray();
            var pvec = playerTrans.position;

            if (AreaRange(pvec, vecs[0], vecs[1], vecs[4]))
            {
                var dist = EdgeToPoint(pvec, vecs[0],vecs[1]);
                var angle = SlopeAngle(vecs[4], vecs[0], vecs[1]);
                var target = TargetVecY(dist, angle);
                _afterMove = true;
                WorldDown(target);
                return;
            }
            if (AreaRange(pvec, vecs[1], vecs[2], vecs[4]))
            {
                var dist = EdgeToPoint(pvec, vecs[1],vecs[2]);
                var angle = SlopeAngle(vecs[4], vecs[1], vecs[2]);
                var target = TargetVecY(dist, angle);
                _afterMove = true;
                WorldDown(target);
                return;
            }
            if (AreaRange(pvec, vecs[2], vecs[3], vecs[4]))
            {
                var dist = EdgeToPoint(pvec, vecs[2],vecs[3]);
                var angle = SlopeAngle(vecs[4], vecs[2], vecs[3]);
                var target = TargetVecY(dist, angle);
                _afterMove = true;
                WorldDown(target);
                return;
            }
            if (AreaRange(pvec, vecs[3], vecs[0], vecs[4]))
            {
                var dist = EdgeToPoint(pvec, vecs[3],vecs[0]);
                var angle = SlopeAngle(vecs[4], vecs[3], vecs[0]);
                var target = TargetVecY(dist, angle);
                _afterMove = true;
                WorldDown(target);
                return;
            }
            if (_afterMove)
            {
                WorldDown(0.0f);
                _afterMove = false;
            }

            var triCenter = (vecs[0] + vecs[1] + vecs[2] + vecs[3]) / 4;
            triCenter = new Vector3(triCenter.x, 0, triCenter.z);
            var pvecY0 = new Vector3(pvec.x, 0, pvec.z);
            if ((triCenter - pvecY0).sqrMagnitude < 0.7f)
            {
                WorldDown(upLimit.localPosition.y);
            }
        }

        private void WorldDown(float moveY)
        {
            if (moveY <= upLimit.localPosition.y)
            {
                worldObj.transform.Translate(0, _nowUp - moveY,0);
                _nowUp = moveY;
            }
            else
            {
                worldObj.transform.Translate(0, _nowUp - upLimit.localPosition.y,0);
                _nowUp = upLimit.localPosition.y;
            }
        }

        private bool AreaRange(Vector3 vecP,Vector3 vec0,Vector3 vec1,Vector3 vec2)
        {
            //参考
            //http://www.thothchildren.com/chapter/5b267a436298160664e80763
            var area = 0.5 *(-vec1.z*vec2.x + vec0.z*(-vec1.x + vec2.x) + vec0.x*(vec1.z - vec2.z) + vec1.x* vec2.z);
            var s = 1/(2*area)*(vec0.z*vec2.x - vec0.x*vec2.z + (vec2.z - vec0.z)*vecP.x + (vec0.x - vec2.x)*vecP.z);
            var t = 1/(2*area)*(vec0.x*vec1.z - vec0.z*vec1.x + (vec0.z - vec1.z)*vecP.x + (vec1.x - vec0.x)*vecP.z);
 
            if((0 < s && s < 1) && (0 < t && t < 1)&&(0 < 1-s-t && 1-s-t < 1)){
                return true; //Inside Triangle
            }
            else
            {
                return false;
            }
        }

        //プレイヤーをどれだけ上げるか(世界をどれだけ下げるか)
        private float TargetVecY(float distance,float angle)
        {
            return angle * distance;
        }

        //四角錐の斜辺の傾きを出します
        //今回は正四角錐なので、一辺について出せば使いまわせます
        private float SlopeAngle(Vector3 vecTop, Vector3 vecA, Vector3 vecB)
        {
            var edgeMid = (vecA + vecB) * 0.5f;
            //高さの差
            var distUp = vecTop.y - edgeMid.y;
            //平面上の差
            var planeTopPos = new Vector3(vecTop.x, edgeMid.y, vecTop.z);
            var distSide = (planeTopPos - edgeMid).magnitude;
        
            //傾き
            return (distUp / distSide);
        }

        //プレイヤーが辺からどれだけ離れているか
        //pからabに垂線を降ろして距離を取る
        private float EdgeToPoint(Vector3 vecP,Vector3 vecA,Vector3 vecB)
        {
            var vecp_y0 = new Vector3(vecP.x, 0, vecP.z);
            var vecA_y0 = new Vector3(vecA.x, 0, vecA.z);
            var vecB_y0 = new Vector3(vecB.x, 0, vecB.z);
            //垂線との交点
            var vecProj = vecA_y0 + Vector3.Project(vecp_y0 - vecA_y0, vecB_y0 - vecA_y0);
            return (vecp_y0 - vecProj).magnitude;
        }
    }
}

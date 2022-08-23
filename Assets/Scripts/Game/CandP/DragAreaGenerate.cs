using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.CandP
{
    /// <summary>
    /// ドラッグエリアに対してメッシュを生成します
    /// </summary>
    [RequireComponent(typeof(MeshFilter))]
    [RequireComponent(typeof(MeshRenderer))]
    public class DragAreaGenerate : MonoBehaviour
    {
        [SerializeField] private Vector3 startVertex, endVertex;
        private List<Vector3> _vertices = new List<Vector3>();
        private List<int> _triangles = new List<int>();
        
        private Mesh _mesh;
        private MeshFilter _meshFilter;
        private Camera _camera;

        public Vector3 GetStartVertex => startVertex;
        public Vector3 GetEndVertex => endVertex;
        public float GetAreaWidth => Mathf.Abs(startVertex.x - endVertex.x);
        public float GetAreaHeight => Mathf.Abs(startVertex.y - endVertex.y);

        //ドラッグエリアの4頂点のうち、左下の頂点を返します
        public Vector2 GetAreaOrigin()
        {
            var worldPosition = this.transform.localToWorldMatrix.GetPosition();
            var startOrigin = startVertex + worldPosition;
            var endOrigin = endVertex + worldPosition;
            return new Vector2
                (Mathf.Min(startOrigin.x, endOrigin.x), 
                    Mathf.Min(startOrigin.y, endOrigin.y));
        }
        
        void Awake()
        {
            _mesh = new Mesh();
            _meshFilter = GetComponent<MeshFilter>();
            _camera = Camera.main;
        }

        public void MouseArea(float width, float height)
        {
            var mouse = RegisterMouseVertex() + this.transform.localToWorldMatrix.GetPosition();
            startVertex = RegisterDirectVertex(new Vector3(-width / 2, -height / 2, 0)) + mouse;
            endVertex = RegisterDirectVertex(new Vector3(width / 2, height / 2, 0)) + mouse;
            DragAreaUpdate();
        }

        public void DirectBoxArea(Vector3 startPoint, Vector3 endPoint)
        {
            startVertex = RegisterDirectVertex(startPoint);
            endVertex = RegisterDirectVertex(endPoint);
            DragAreaUpdate();
        }

        public void DragAreaStart()
        {
            startVertex = RegisterMouseVertex();
        }
        public void DragAreaComplete()
        {
            //頂点をリセット
            endVertex = startVertex;
            _mesh.Clear();
        }

        public void ResetVertex()
        {
            startVertex = RegisterMouseVertex();
            endVertex = startVertex;
        }

        public Vector3 RegisterMouseVertex()
        {
            /*
            var vertex = _camera.ScreenToWorldPoint(Input.mousePosition);
            var position = this.transform.position;
            vertex -= position;
            vertex.z = position.z;
            return vertex;
            */
            return RegisterDirectVertex(_camera.ScreenToWorldPoint(Input.mousePosition));
        }
        private Vector3 RegisterDirectVertex(Vector3 vec)
        {
            var position = this.transform.position;
            vec -= position;
            vec.z = position.z; //描画順に自信がないので、カメラ側に寄せてください
            return vec;
        }
        //終了地点の登録と、描画をしてくれます
        public void DragAreaEndPoint()
        {
            endVertex = RegisterMouseVertex();
            DragAreaUpdate();
        }
        /// <summary>
        /// マウスの位置が制限範囲を超えた場合のendVertexを出したい
        /// 評価方法は面積なので、x * y = (基準の最大値) が基準の境界
        /// 整理して y = max / x なので逆数のグラフ
        /// 第一、第三象限はそのままだけど、第二、第四象限は y = -max / x にする
        /// startVertexと現在のマウスの座標を結んだ直線と、逆数のグラフとの交点を出す
        /// 連立から。 endVertex.x = ±√ {(マウス.x / マウス.y) * max}
        /// 交点は2箇所出るが、よりマウスに近い方が正しいendVertex.x
        /// y = max / x に代入してベクトルが出る
        /// ルートを計算するので重い、処理速度が問題になるかもしれない
        /// </summary>
        public void DragAreaOverEndPoint(float maxArea)
        {
            //計算しやすいようにstartVertexを原点として考える
            var mousePos = RegisterMouseVertex() - startVertex;
            //象限を出すついでに、分数に0が入る可能性を除外する
            endVertex = RegisterMouseVertex();
            var quad = Quadrant();
            switch (quad)
            {
                case 1 :
                    endVertex.x = Mathf.Sqrt((mousePos.x / mousePos.y) * maxArea);
                    endVertex.y = maxArea / endVertex.x;
                    break;
                case 2 :
                    endVertex.x = Mathf.Sqrt((mousePos.x / mousePos.y) * -maxArea);
                    endVertex.x *= -1;
                    endVertex.y = -maxArea / endVertex.x;
                    break;
                case 3 :
                    endVertex.x = Mathf.Sqrt((mousePos.x / mousePos.y) * maxArea);
                    endVertex.x *= -1;
                    endVertex.y = maxArea / endVertex.x;
                    break;
                case 4:
                    endVertex.x = Mathf.Sqrt((mousePos.x / mousePos.y) * -maxArea);
                    endVertex.y = -maxArea / endVertex.x;
                    break;
            }
            //startVertexを原点としていたところを戻す
            endVertex += startVertex;
            endVertex.z = startVertex.z;
            DragAreaUpdate();
        }

        private void DragAreaUpdate()
        {
            GenerateVertices();
            var quad = Quadrant();
            switch (quad)
            {
                case 1 or 3:
                    FirstThirdQuadrant(startVertex, endVertex);
                    break;
                case 2 or 4:
                    SecondFourthQuadrant(startVertex, endVertex);
                    break;
            }
            //メッシュの登録
            _mesh.SetVertices(_vertices);
            _mesh.SetTriangles(_triangles, 0);
            //メッシュフィルターに適応
            _meshFilter.mesh = _mesh;
        }
        
        //描画に使用する頂点を生成
        void GenerateVertices()
        {
            var position = this.transform.position;
            _vertices = new List<Vector3>()
            {
                startVertex,
                endVertex,
                new Vector3(startVertex.x, endVertex.y, position.z),
                new Vector3(endVertex.x, startVertex.y, position.z),
            };
        }
        
        //ドラッグ開始地点を原点とみて、終了地点の象限を返します
        int Quadrant()
        {
            //象限にない場合、便宜上0を返す
            if (Math.Abs(startVertex.x - endVertex.x) <= 0) return 0;
            if (Math.Abs(startVertex.y - endVertex.y) <= 0) return 0;

            if (startVertex.y < endVertex.y)
            {
                return (startVertex.x < endVertex.x) ? 1 : 2;
            }
            else
            {
                return (startVertex.x < endVertex.x) ? 4 : 3;
            }
        }
        //面がカメラを向くように頂点の順番を決定します
        //第一、第三象限の頂点配列
        void FirstThirdQuadrant(Vector3 startPos, Vector3 endPos)
        {
            _triangles = new List<int>
            {
                0, 2, 1,
                0, 1, 3
            };
        }
        //第二、第四象限の頂点配列
        void SecondFourthQuadrant(Vector3 startPos, Vector3 endPos)
        {
            _triangles = new List<int>
            {
                0, 1, 2,
                0, 3, 1
            };
        }
    }
}

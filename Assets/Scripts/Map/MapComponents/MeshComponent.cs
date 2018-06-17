using System.Collections.Generic;
using UnityEngine;

namespace Map.MapComponents
{
    [RequireComponent(typeof(MeshFilter), typeof(MeshRenderer), typeof(MeshCollider))]
    abstract class MeshComponent : ChunkComponent
    {
        private Mesh _mesh;
        private MeshCollider _meshCollider;
        protected static List<Vector3> Vertices = new List<Vector3>();
        protected static List<int> Indexes = new List<int>();
        protected static List<Color> Colors = new List<Color>();

        protected abstract void Triangulate(HexCell cell);

        void Awake()
        {
            _mesh = GetComponent<MeshFilter>().mesh;
            _meshCollider = GetComponent<MeshCollider>();
        }

        public override void Draw(HexCell[] cells)
        {
            Vertices.Clear();
            Indexes.Clear();
            _mesh.Clear();
            Colors.Clear();
            foreach (HexCell cell in cells)
                Triangulate(cell);
            _mesh.vertices = Vertices.ToArray();
            _mesh.triangles = Indexes.ToArray();
            _mesh.colors = Colors.ToArray();
            _mesh.RecalculateNormals();
            _meshCollider.sharedMesh = _mesh;
        }
        
        protected void AddTriangle(Vector3 p1, Vector3 p2, Vector3 p3, Color c)
        {
            AddTriangle(p1, p2, p3, c, c, c);
        }

        protected void AddTriangle(Vector3 p1, Vector3 p2, Vector3 p3, Color c1, Color c2, Color c3)
        {
            int indexVertex = Vertices.Count;
            Vertices.Add(p1);
            Vertices.Add(p2);
            Vertices.Add(p3);
            Indexes.Add(indexVertex);
            Indexes.Add(indexVertex + 1);
            Indexes.Add(indexVertex + 2);
            Colors.Add(c1);
            Colors.Add(c2);
            Colors.Add(c3);
        }
    }
}

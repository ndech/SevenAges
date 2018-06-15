using System;
using System.Collections.Generic;
using UnityEngine;

namespace Map
{
    [RequireComponent(typeof(MeshFilter), typeof(MeshRenderer), typeof(MeshCollider))]
    class HexMesh : MonoBehaviour
    {
        private Mesh _mesh;
        private MeshCollider _meshCollider;
        private static List<Vector3> _vertices;
        private static List<int> _indexes;
        private static List<Color> _colors;

        void Awake()
        {
            _mesh = GetComponent<MeshFilter>().mesh;
            _meshCollider = GetComponent<MeshCollider>();
            _vertices = new List<Vector3>();
            _indexes = new List<int>();
            _colors = new List<Color>();
        }

        public void Triangulate(HexCell[] cells)
        {
            _vertices.Clear();
            _indexes.Clear();
            _mesh.Clear();
            _colors.Clear();
            foreach (HexCell cell in cells)
                Triangulate(cell);
            _mesh.vertices = _vertices.ToArray();
            _mesh.triangles = _indexes.ToArray();
            _mesh.colors = _colors.ToArray();
            _mesh.RecalculateNormals();
            _meshCollider.sharedMesh = _mesh;
        }

        private void Triangulate(HexCell cell)
        {
            foreach (HexDirection direction in Enum.GetValues(typeof(HexDirection)))
                AddTriangle(HexMetrics.Center(cell.Coordinates),
                    cell.FirstVertex(direction),
                    cell.SecondVertex(direction),
                    cell.Color);
            Bridge(cell, HexDirection.NE);
            Bridge(cell, HexDirection.E);
            Bridge(cell, HexDirection.SE);
            if(cell.GetNeighbour(HexDirection.E) && cell.GetNeighbour(HexDirection.NE))
                AddTriangle(cell.FirstVertex(HexDirection.E),
                    cell.GetNeighbour(HexDirection.NE).FirstVertex(HexDirection.SW),
                    cell.GetNeighbour(HexDirection.E).SecondVertex(HexDirection.W),
                    cell.Color,
                    cell.GetNeighbour(HexDirection.NE).Color,
                    cell.GetNeighbour(HexDirection.E).Color);
            if (cell.GetNeighbour(HexDirection.E) && cell.GetNeighbour(HexDirection.SE))
                AddTriangle(cell.SecondVertex(HexDirection.E),
                    cell.GetNeighbour(HexDirection.E).FirstVertex(HexDirection.W),
                    cell.GetNeighbour(HexDirection.SE).SecondVertex(HexDirection.NW),
                    cell.Color,
                    cell.GetNeighbour(HexDirection.E).Color,
                    cell.GetNeighbour(HexDirection.SE).Color);
        }

        private void Bridge(HexCell cell, HexDirection direction)
        {
            HexCell neighbour = cell.GetNeighbour(direction);
            if (neighbour == null) return;
            AddTriangle(cell.FirstVertex(direction), neighbour.FirstVertex(direction.Opposite()), cell.SecondVertex(direction),
                cell.Color, neighbour.Color, cell.Color);
            AddTriangle(neighbour.FirstVertex(direction.Opposite()), cell.FirstVertex(direction), neighbour.SecondVertex(direction.Opposite()),
                neighbour.Color, cell.Color, neighbour.Color);
        }

        private void AddTriangle(Vector3 p1, Vector3 p2, Vector3 p3, Color c)
        {
            AddTriangle(p1, p2, p3, c, c, c);
        }

        private void AddTriangle(Vector3 p1, Vector3 p2, Vector3 p3, Color c1, Color c2, Color c3)
        {
            int indexVertex = _vertices.Count;
            _vertices.Add(p1);
            _vertices.Add(p2);
            _vertices.Add(p3);
            _indexes.Add(indexVertex);
            _indexes.Add(indexVertex + 1);
            _indexes.Add(indexVertex + 2);
            _colors.Add(c1);
            _colors.Add(c2);
            _colors.Add(c3);
        }
    }
}

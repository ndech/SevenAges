using UnityEngine;
using UnityEngine.UI;

namespace Map
{
    class HexGridChunk : MonoBehaviour
    {
        private Canvas _overlayCanvas;
        private HexMesh _mesh;
        private HexCell[] _cells;
        private bool _triangulationRequired = false;

        public void Awake()
        {
            _overlayCanvas = GetComponentInChildren<Canvas>();
            _mesh = GetComponentInChildren<HexMesh>();
            _cells = new HexCell[HexMetrics.ChunkSizeX * HexMetrics.ChunkSizeZ];
            _triangulationRequired = true;
        }

        public void LateUpdate()
        {
            if (_triangulationRequired)
            {
                _mesh.Triangulate(_cells);
                _triangulationRequired = false;
            }
        }

        public void RegisterCell(HexCell cell, int index, Text textPrefab)
        {
            cell.transform.SetParent(transform);
            _cells[index] = cell;
            cell.Chunk = this;
            cell.Label = Instantiate(textPrefab);
            cell.Label.rectTransform.SetParent(_overlayCanvas.transform, false);
            cell.Label.rectTransform.anchoredPosition = new Vector2(cell.transform.position.x, cell.transform.position.z);
            cell.Label.text = cell.Coordinates.ToStringOnSeparateLines();
        }

        public void Refresh()
        {
            _triangulationRequired = true;
        }
    }
}

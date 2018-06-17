using Boo.Lang;
using Map.MapComponents;
using UnityEngine;
using UnityEngine.UI;

namespace Map
{
    class HexGridChunk : MonoBehaviour
    {
        private Canvas _overlayCanvas;
        public List<ChunkComponent> Components;
        private HexCell[] _cells;
        private bool _triangulationRequired;

        public void Awake()
        {
            _overlayCanvas = GetComponentInChildren<Canvas>();
            Components = new List<ChunkComponent>
            {
                GetComponentInChildren<TerrainComponent>(),
                GetComponentInChildren<TreesComponent>()
            };
            _cells = new HexCell[HexMetrics.ChunkSizeX * HexMetrics.ChunkSizeZ];
            _triangulationRequired = true;
        }

        public void LateUpdate()
        {
            if (_triangulationRequired)
            {
                foreach (var component in Components)
                    component.Draw(_cells);
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

using Terrain;
using UnityEngine;
using UnityEngine.UI;

namespace Map
{
    class HexCell : MonoBehaviour
    {
        public HexCoordinates Coordinates;
        private TerrainType _terrainType;

        public TerrainType TerrainType
        {
            get { return _terrainType; }
            set
            {
                _terrainType = value;
                Refresh();
            }
        }

        private void Refresh()
        {
            Chunk.Refresh();
            foreach (HexCell cell in _neighbours)
                if(cell != null && cell.Chunk != Chunk) cell.Chunk.Refresh();
        }

        [SerializeField]
        private HexCell[] _neighbours = new HexCell[6];

        public Text Label { get; set; }
        public Unit Unit { get; set; }
        public HexGridChunk Chunk { get; set; }
        public HexCell GetNeighbour(HexDirection direction) => _neighbours[(int) direction];

        public void SetNeighbour(HexCell cell, HexDirection direction, bool setOppositeRelationship = true)
        {
            _neighbours[(int) direction] = cell;
            if(setOppositeRelationship)
                cell.SetNeighbour(this, direction.Opposite(), false);
        }

        public void Initialize(HexCoordinates coordinates, TerrainType terrainType)
        {
            Coordinates = coordinates;
            _terrainType = terrainType;
            name = $"Cell {coordinates}";
        }

        public void SetTerrain(TerrainType terrainType) => TerrainType = terrainType;
    }
}
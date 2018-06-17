using Map.CellFeatures;
using UnityEngine;
using UnityEngine.UI;
using Terrain = Map.CellFeatures.Terrain;

namespace Map
{
    class HexCell : MonoBehaviour
    {
        public HexCoordinates Coordinates;
        private Terrain _terrain;

        public Terrain Terrain
        {
            get { return _terrain; }
            set
            {
                _terrain = value;
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
            _terrain = TerrainCollection.Get(TerrainType.Ocean);
            name = $"Cell {coordinates}";
        }

        public void SetTerrain(Terrain terrain) => Terrain = terrain;
    }
}
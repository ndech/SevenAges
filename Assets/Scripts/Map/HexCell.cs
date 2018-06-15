using UnityEngine;
using UnityEngine.UI;

namespace Map
{
    class HexCell : MonoBehaviour
    {
        public HexCoordinates Coordinates;
        private Color _color;

        public Color Color
        {
            get { return _color; }
            set
            {
                _color = value;
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

        public void Initialize(HexCoordinates coordinates, Color color)
        {
            Coordinates = coordinates;
            _color = color;
            name = $"Cell {coordinates}";
        }

        public void SetColor(Color activeColor) => Color = activeColor;
    }
}
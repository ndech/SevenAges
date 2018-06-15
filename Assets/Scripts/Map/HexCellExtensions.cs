using UnityEngine;

namespace Map
{
    static class HexCellExtensions
    {
        public static Vector3 FirstVertex(this HexCell cell, HexDirection direction)
        {
            return HexMetrics.FirstVertex(cell.Coordinates, direction);
        }
        public static Vector3 SecondVertex(this HexCell cell, HexDirection direction)
        {
            return HexMetrics.SecondVertex(cell.Coordinates, direction);
        }
    }
}
using System;
using Map.CellFeatures;
using UnityEngine;

namespace Map.MapComponents
{
    class TerrainComponent : MeshComponent
    {
        protected override void Triangulate(HexCell cell)
        {
            foreach (HexDirection direction in Enum.GetValues(typeof(HexDirection)))
                AddTriangle(HexMetrics.Center(cell.Coordinates),
                    cell.FirstVertex(direction),
                    cell.SecondVertex(direction),
                    GetColor(cell));
            Bridge(cell, HexDirection.NE);
            Bridge(cell, HexDirection.E);
            Bridge(cell, HexDirection.SE);
            if (cell.GetNeighbour(HexDirection.E) && cell.GetNeighbour(HexDirection.NE))
                AddTriangle(cell.FirstVertex(HexDirection.E),
                    cell.GetNeighbour(HexDirection.NE).FirstVertex(HexDirection.SW),
                    cell.GetNeighbour(HexDirection.E).SecondVertex(HexDirection.W),
                    GetColor(cell),
                    GetColor(cell.GetNeighbour(HexDirection.NE)),
                    GetColor(cell.GetNeighbour(HexDirection.E)));
            if (cell.GetNeighbour(HexDirection.E) && cell.GetNeighbour(HexDirection.SE))
                AddTriangle(cell.SecondVertex(HexDirection.E),
                    cell.GetNeighbour(HexDirection.E).FirstVertex(HexDirection.W),
                    cell.GetNeighbour(HexDirection.SE).SecondVertex(HexDirection.NW),
                    GetColor(cell),
                    GetColor(cell.GetNeighbour(HexDirection.E)),
                    GetColor(cell.GetNeighbour(HexDirection.SE)));
        }

        private void Bridge(HexCell cell, HexDirection direction)
        {
            HexCell neighbour = cell.GetNeighbour(direction);
            if (neighbour == null) return;
            AddTriangle(cell.FirstVertex(direction), neighbour.FirstVertex(direction.Opposite()), cell.SecondVertex(direction),
                GetColor(cell), GetColor(neighbour), GetColor(cell));
            AddTriangle(neighbour.FirstVertex(direction.Opposite()), cell.FirstVertex(direction), neighbour.SecondVertex(direction.Opposite()),
                GetColor(neighbour), GetColor(cell), GetColor(neighbour));
        }

        public Color GetColor(HexCell cell)
        {
            switch (cell.Terrain.Type)
            {
                case TerrainType.Ocean: return Color.blue;
                case TerrainType.Grassland: return Color.green;
                case TerrainType.Desert: return Color.yellow;
                case TerrainType.Coast: return Color.cyan;
                default: return Color.gray;
            }
        }
    }
}

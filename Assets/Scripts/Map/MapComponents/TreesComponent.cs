using Boo.Lang;
using Map.CellFeatures;
using UnityEngine;

namespace Map.MapComponents
{
    class TreesComponent : ChunkComponent
    {
        public Transform[] GrasslandForestTreePrefabs;
        public override void Draw(HexCell[] cells)
        {
            foreach (Transform child in GetComponentInChildren<Transform>())
                Destroy(child.gameObject);
            foreach (HexCell cell in cells)
            {
                if(cell.TerrainFeature == null) continue;
                if (cell.TerrainFeature.Type == TerrainFeatureType.Forest)
                    DrawForest(cell);
                else if (cell.TerrainFeature.Type == TerrainFeatureType.Jungle)
                    DrawJungle(cell);
            }
        }

        private void DrawForest(HexCell cell)
        {
            Instantiate(GrasslandForestTreePrefabs[0], transform).position = HexMetrics.Center(cell.Coordinates);
        }

        private void DrawJungle(HexCell cell)
        {
            throw new System.NotImplementedException();
        }
    }
}

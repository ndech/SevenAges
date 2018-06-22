using System.Collections.Generic;
using System.Linq;

namespace Map.CellFeatures
{
    static class TerrainCollection
    {
        private static readonly Dictionary<TerrainType, Terrain> Terrains;

        static TerrainCollection()
        {
            var terrains = new List<Terrain>
            {
                new Terrain(TerrainType.Ocean),
                new Terrain(TerrainType.Coast),
                new Terrain(TerrainType.Desert),
                new Terrain(TerrainType.Grassland),
                new Terrain(TerrainType.Lake),
                new Terrain(TerrainType.Plains),
                new Terrain(TerrainType.Snow),
                new Terrain(TerrainType.Tundra)
            };
            Terrains = terrains.ToDictionary(t => t.Type);
        }

        public static Terrain Get(TerrainType type)
        {
            return Terrains[type];
        }
    }
}

using System.Collections.Generic;
using System.Linq;

namespace Map.CellFeatures
{
    static class TerrainCollection
    {
        private static Dictionary<TerrainType, Terrain> _terrains;

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
            _terrains = terrains.ToDictionary(t => t.Type);
        }

        public static Terrain Get(TerrainType type)
        {
            return _terrains[type];
        }
    }
}

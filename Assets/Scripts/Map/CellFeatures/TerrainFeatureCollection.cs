using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Map.CellFeatures;

namespace Assets.Scripts.Map.CellFeatures
{
    class TerrainFeatureCollection
    {
        private static readonly Dictionary<TerrainFeatureType, TerrainFeature> TerrainFeatures;

        static TerrainFeatureCollection()
        {
            var terrainsFeatures = new List<TerrainFeature>
            {
                new TerrainFeature(TerrainFeatureType.Forest),
                new TerrainFeature(TerrainFeatureType.Jungle),
                new TerrainFeature(TerrainFeatureType.Ice),
                new TerrainFeature(TerrainFeatureType.Marsh),
                new TerrainFeature(TerrainFeatureType.Oasis)
            };
            TerrainFeatures = terrainsFeatures.ToDictionary(t => t.Type);
        }

        public static TerrainFeature Get(TerrainFeatureType type)
        {
            return TerrainFeatures[type];
        }
    }
}

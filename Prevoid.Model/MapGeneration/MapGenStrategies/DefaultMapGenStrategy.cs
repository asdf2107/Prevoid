using System;
using System.Collections.Generic;
using System.Linq;

namespace Prevoid.Model.MapGeneration.MapGenStrategies
{
    public class DefaultMapGenStrategy : IMapGenStrategy
    {
        public void GenMap(Map map, int seed)
        {
            Noise2d.Reseed(seed);
            float frequency = 0.1f;

            for (int j = 0; j < map.Height; j++)
            {
                for (int i = 0; i < map.Width; i++)
                {
                    map.TerrainTypes[i, j] = GetTerrainType(Noise2d.Noise(i * frequency, j * frequency));
                }
            }

            Noise2d.Reseed(seed + 1);

            for (int j = 0; j < map.Height; j++)
            {
                for (int i = 0; i < map.Width; i++)
                {
                    if (map.TerrainTypes[i, j] != TerrainType.Water)
                        map.TerrainTypes[i, j] = Noise2d.Noise(i * frequency, j * frequency) > 0.4f ? 
                            TerrainType.Mountain : map.TerrainTypes[i, j];
                }
            }
        }

        private TerrainType GetTerrainType(float val)
        {
            return val switch
            {
                < -0.3f => TerrainType.Water,
                < 0.0f => TerrainType.Flat,
                < 0.2f => TerrainType.SparceForest,
                _ => TerrainType.DeepForest
            };
        }
    }
}

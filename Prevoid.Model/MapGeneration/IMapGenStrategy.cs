using System;
using System.Collections.Generic;
using System.Linq;

namespace Prevoid.Model.MapGeneration
{
    public interface IMapGenStrategy
    {
        void GenMap(Map map, int seed);
    }
}

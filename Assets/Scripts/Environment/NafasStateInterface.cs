using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Chiro
{
    public interface INafasStateInterface
    {
        public void UpdateNafas();
        public void ToInitialState();
        public void ToGrindState();
        public void ToVictoryLapState();
        public void ToTrapHouseState();
    }
}

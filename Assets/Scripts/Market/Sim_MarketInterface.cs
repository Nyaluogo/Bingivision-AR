using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Chiro
{
    public interface Sim_MarketInterface
    {
        public void UpdateMarket();
        public void ToNullMarket();
        public void ToVacantMarket();
        public void ToHostingMarket();
        public void ToClosedMarket();
        public void ToInViewMarket();
        
    }
}

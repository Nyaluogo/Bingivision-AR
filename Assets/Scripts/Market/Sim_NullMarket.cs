using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Chiro
{
    public class Sim_NullMarket : Sim_MarketInterface
    {
        private readonly Sim_MarketPattern pattern;
        private Sim_MarketPattern sim_MarketPattern;

        public Sim_NullMarket(Sim_MarketPattern sim_MarketPattern)
        {
            this.sim_MarketPattern = sim_MarketPattern;
        }

        public void LoadContent()
        {

        }

        public void UpdateMarket()
        {
            throw new System.NotImplementedException();
        }

        public void ToVacantMarket()
        {
            throw new System.NotImplementedException();
        }

        public void ToClosedMarket()
        {
            throw new System.NotImplementedException();
        }

        public void ToHostingMarket()
        {
            throw new System.NotImplementedException();
        }

        public void ToInViewMarket()
        {
            throw new System.NotImplementedException();
        }

        public void ToNullMarket()
        {
            throw new System.NotImplementedException();
        }

        
    }
}

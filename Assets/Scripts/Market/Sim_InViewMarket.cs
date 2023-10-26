using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Chiro.Sim_MarketPattern;

namespace Chiro
{
    public class Sim_InViewMarket : Sim_MarketInterface
    {
        private Sim_MarketPattern market;

        public Sim_InViewMarket(Sim_MarketPattern sim_MarketPattern)
        {
            this.market = sim_MarketPattern;
        }


        public void CheckPresence()
        {
            market.CheckCustomerPresence();
        }

        public void WashaStima()
        {
            var stima = market.mwangaza;
            if (stima.washaOnEnter)
            {
                stima.Toggle(true);
            }
        }

        public void ToggleObjects()
        {
            market.hostingRef.ToggleObj();
        }

        public void EnableAds()
        {
            if (market.adManager != null)
            {
                var ads = market.adManager;
                ads.SetMarketAds();

            }
        }

        public void AlertKiosks()
        {
            if (market.kiosks.Count > 0)
            {
                foreach (Kiosk kiosk in market.kiosks)
                {
                    if (kiosk.kioskPattern != null && !kiosk.kioskPattern.customerIsNear)
                    {
                        kiosk.kioskPattern.customerIsNear = true;
                    }
                }
            }
        }

        public void UpdateMarket()
        {
            AlertKiosks();
            ToggleObjects();
            EnableAds();
            WashaStima();
            CheckPresence();
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

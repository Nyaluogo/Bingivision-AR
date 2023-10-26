using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Chiro.Sim_MarketPattern;

namespace Chiro
{
    public class Sim_HostingMarket : Sim_MarketInterface
    {
        private Sim_MarketPattern market;

        public Sim_HostingMarket(Sim_MarketPattern sim_MarketPattern)
        {
            market = sim_MarketPattern;
        }

        public void CheckPresence()
        {
            market.CheckCustomerPresence();
        }

        public void WashaStima()
        {
            var stima = market.mwangaza;
            if(stima.washaOnEnter)
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
            if(market.adManager != null)
            {
                var ads = market.adManager;
                ads.SetMarketAds();
                
            }
        }

        public void AlertKiosks()
        {
            if(market.kiosks.Count > 0)
            {
                foreach (Kiosk kiosk in market.kiosks)
                {
                    if(kiosk.kioskPattern != null && !kiosk.kioskPattern.customerIsNear)
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
            market.ActivateVacantMarket(); 
        }

        public void ToClosedMarket()
        {
            market.ActivateClosedMarket();
        }

        public void ToHostingMarket()
        {
            throw new System.NotImplementedException();
        }

        public void ToInViewMarket()
        {
            market.ActivateInViewMarket();
        }

        public void ToNullMarket()
        {
            throw new System.NotImplementedException();
        }

        
    }
}

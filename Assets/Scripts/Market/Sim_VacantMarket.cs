using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Chiro.Sim_MarketPattern;

namespace Chiro
{
    public class Sim_VacantMarket : Sim_MarketInterface
    {
        private Sim_MarketPattern market;

        public Sim_VacantMarket(Sim_MarketPattern sim_MarketPattern)
        {
            this.market = sim_MarketPattern;
        }

        public void CheckPresence()
        {
            market.CheckCustomerPresence();
        }

        public void ZimaStima()
        {
            var stima = market.mwangaza;
            if (stima.zimaOnExit)
            {
                stima.Toggle(false);
            }
        }

        public void ToggleObjects()
        {
            market.vacancyRef.ToggleObj();
        }

        public void DisableAds()
        {
            if (market.adManager != null)
            {
                var ads = market.adManager;
                ads.UnsetMarketAds();

            }
        }

        public void CloseKiosks()
        {
            if (market.kiosks.Count > 0)
            {
                foreach (Kiosk kiosk in market.kiosks)
                {
                    if (kiosk.kioskPattern != null && !kiosk.kioskPattern.customerIsNear)
                    {
                        kiosk.kioskPattern.customerIsNear = false;
                    }
                }
            }
        }


        public void UpdateMarket()
        {
            CloseKiosks();
            ToggleObjects();
            DisableAds();
            ZimaStima();
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Chiro
{
    public class Kiosk_OccupiedState : IKiosk_StateInterface
    {
        private readonly Kiosk_StatePattern kiosk;

        public Kiosk_OccupiedState(Kiosk_StatePattern kiosk_StatePattern)
        {
            kiosk = kiosk_StatePattern;
        }

        public void CheckPresence()
        {
            if (!kiosk.customerIsPresent)
            {
                ToVacantState();
            }
            else if (kiosk.customerIsNear)
            {
                ToInViewState();
            }
        }

        public void WashaStima()
        {
            if (kiosk.mwangaza.toggleOnExit)
            {
                kiosk.mwangaza.Toggle(true);
            }
        }

        public void StartMedia()
        {
            if(kiosk.adManager != null)
            {
                var ads = kiosk.adManager;
                if(!ads.ad.isInitialised)
                {
                    ads.SetKioskAds();
                }
            }
            
        }

        public void ToggleObjects()
        {
            kiosk.occupiedRef.ToggleObj();
        }

        public void UpdateKiosk()
        {
            ToggleObjects();
            WashaStima();
            StartMedia();
            CheckPresence();
        }

        public void ToInViewState()
        {
            kiosk.ActivateInViewState();
        }

        public void ToOccupiedState()
        {
            throw new System.NotImplementedException();
        }

        public void ToVacantState()
        {
            kiosk.ActivateVacantState();
        }

        
    }
}

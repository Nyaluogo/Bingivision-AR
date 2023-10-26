using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Chiro
{
    public class Kiosk_InViewState : IKiosk_StateInterface
    {
        private Kiosk_StatePattern kiosk;

        public Kiosk_InViewState(Kiosk_StatePattern kiosk_StatePattern)
        {
            this.kiosk = kiosk_StatePattern;
        }

        public void CheckPresence()
        {
            if (kiosk.customerIsPresent)
            {
                ToOccupiedState();
            }
            else if (!kiosk.customerIsNear && !kiosk.customerIsPresent)
            {
                ToVacantState();
            }
        }

        public void WashaStima()
        {
            if (kiosk.mwangaza.toggleOnExit)
            {
                kiosk.mwangaza.Toggle(true);
            }
        }

        public void StopMedia()
        {
            kiosk.adManager.UnsetKioskAds();
            //TODO:Pause media instead
        }

        public void ToggleObjects()
        {
            kiosk.inViewRef.ToggleObj();
        }

        public void UpdateKiosk()
        {
            ToggleObjects();
            WashaStima();
            StopMedia();
            CheckPresence();
        }

        public void ToInViewState()
        {
            throw new System.NotImplementedException();
        }

        public void ToOccupiedState()
        {
            kiosk.ActivateOccupiedState();
        }

        public void ToVacantState()
        {
            kiosk.ActivateVacantState();
        }

        
    }
}

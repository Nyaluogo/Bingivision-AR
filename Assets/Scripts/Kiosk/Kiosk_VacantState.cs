using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Chiro
{
    public class Kiosk_VacantState : IKiosk_StateInterface
    {
        private Kiosk_StatePattern kiosk;

        public Kiosk_VacantState(Kiosk_StatePattern kiosk_StatePattern)
        {
            this.kiosk = kiosk_StatePattern;
        }

        public void CheckPresence()
        {
            if(kiosk.customerIsPresent)
            {
                ToOccupiedState();
            }
            else if(kiosk.customerIsNear)
            {
                ToInViewState();
            }
        }

        public void ZimaStima()
        {
            if(kiosk.mwangaza.toggleOnExit)
            {
                kiosk.mwangaza.Toggle(false);
            }
        }

        public void StopMedia()
        {
            kiosk.adManager.UnsetKioskAds();
        }

        public void ToggleObjects()
        {
            kiosk.vacantRef.ToggleObj();
        }

        public void UpdateKiosk()
        {
            ToggleObjects();
            //ZimaStima();
            //StopMedia();
            CheckPresence();
        }

        public void ToInViewState()
        {
            kiosk.ActivateInViewState();
        }

        public void ToOccupiedState()
        {
            kiosk.ActivateOccupiedState();
        }

        public void ToVacantState()
        {
            throw new System.NotImplementedException();
        }


    }
}

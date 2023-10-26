using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Chiro
{
    public class MarketMaster : MonoBehaviour
    {
        public delegate void MarketEventHandler();
        public MarketEventHandler NullEvent;
        public MarketEventHandler ClosedEvent;
        public MarketEventHandler InViewEvent;
        public MarketEventHandler HostingEvent;
        public MarketEventHandler VacantEvent;

        [System.Serializable]
        public class Details
        {
            public string name;
            public string description;
            public string tagline;
            public string email;
            public string phonenumber;
            public string website;
            public Texture logo;
        }
        public Details details;

        public void CallNullEvent()
        {
            if(NullEvent != null)
            {
                NullEvent();
            }
        }

        public void CallClosedEvent()
        {
            if (ClosedEvent != null)
            {
                ClosedEvent();
            }
        }

        public void CallInViewEvent()
        {
            if (InViewEvent != null)
            {
                InViewEvent();
            }
        }

        public void CallHostingEvent()
        {
            if (HostingEvent != null)
            {
                HostingEvent();
            }
        }

        public void CallVacantEvent()
        {
            if (VacantEvent != null)
            {
                VacantEvent();
            }
        }




        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        void SetInitialReferences()
        {

        }

        void SetUpdateReferences()
        {

        }
    }

}
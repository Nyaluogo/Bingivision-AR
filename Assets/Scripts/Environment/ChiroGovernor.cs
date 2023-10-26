using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static Chiro.CustomerMaster;

namespace Chiro
{
    public class ChiroGovernor : MonoBehaviour
    {
        public enum EnvironmentContext
        {
            SOFTWARE,
            EDUCATION,
            BOOKS,
            ART,
            GOVERNMENT
        }
        public EnvironmentContext context;


        [System.Serializable]
        public class Chiro
        {
            public Sim_MarketPattern marketPattern;
            public List<Attendance> attendances;
        }

        public List<Chiro> marketsList;
        public Chiro currentMarket;

        [System.Serializable]
        public class Details
        {
            public string name;
            public string description;
            public GameObject mainObj;

        }
        public Details details;
        public CustomerMaster customer;

        // Start is called before the first frame update
        void Start()
        {
            SetInitialReferences();
        }

        // Update is called once per frame
        void Update()
        {
            SetUpdateReferences();
        }

        void SetInitialReferences()
        {

        }

        void SetUpdateReferences()
        {
            RegulateMarkets();
        }

        public void RegulateMarkets()
        {
            if(marketsList.Count > 0)
            {
                currentMarket = GetCurrentMarket();
                MonitorAttendance();
            }
        }

        public Chiro GetCurrentMarket()
        {
            if(marketsList.Count > 0)
            {
                var mrkt = marketsList.Where(m=>m.marketPattern.customerIsPresent);
                return mrkt.FirstOrDefault();
            }
            return null;
        }

        public Kiosk_StatePattern GetHostKiosk()
        {
            if(GetCurrentMarket() != null)
            {
                if(GetCurrentMarket().marketPattern != null)
                {
                    if(GetCurrentMarket().marketPattern.GetHostKiosk() != null)
                    {
                        return GetCurrentMarket().marketPattern.GetHostKiosk().kioskPattern;
                    }
                }
            }
            return null;
        }

        public void FastTravel(Sim_MarketPattern marketPattern)
        {
            if (marketPattern.spawnPoint == null) return;
            customer.SpawnAt(marketPattern.spawnPoint);

        }

        public void MonitorAttendance()
        {

        }
    }
}

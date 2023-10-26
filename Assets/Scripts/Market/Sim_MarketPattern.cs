using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Chiro
{
    public class Sim_MarketPattern : MonoBehaviour
    {

        [Header("Decision making")]

        private float checkRate = 0.1f;
        private float nextCheck;


        [System.Serializable]
        public class Attendance
        {
            public CustomerMaster attendee;
            public float duration;
        }

        [System.Serializable]
        public class Kiosk
        {
            public string name;
            public Kiosk_StatePattern kioskPattern;
            public List<Attendance> attendances;
        }
        public List<Kiosk> kiosks;
        public Kiosk hostKiosk;


        [System.Serializable]
        public class VacancyReference
        {
            public List<GameObject> enableObjs;
            public List<GameObject> disableObjs;
            public bool shouldInitAds = false;

            public void ToggleObj()
            {
                if (enableObjs.Count > 0)
                {
                    foreach (GameObject obj in enableObjs)
                    {
                        if (obj != null)
                        {
                            obj.SetActive(true);
                        }
                    }

                    foreach (GameObject obj in disableObjs)
                    {
                        if (obj != null)
                        {
                            obj.SetActive(false);
                        }
                    }
                }
            }

        }
        public VacancyReference vacancyRef;


        [System.Serializable]
        public class ClosedReference
        {
            public List<GameObject> enableObjs;
            public List<GameObject> disableObjs;
            public bool shouldInitAds = false;

            public void ToggleObj()
            {
                if (enableObjs.Count > 0)
                {
                    foreach (GameObject obj in enableObjs)
                    {
                        if (obj != null)
                        {
                            obj.SetActive(true);
                        }
                    }

                    foreach (GameObject obj in disableObjs)
                    {
                        if (obj != null)
                        {
                            obj.SetActive(false);
                        }
                    }
                }
            }

        }
        public ClosedReference closedRef;


        [System.Serializable]
        public class HostingReference
        {
            public Transform customer;
            public int duration;
            public List<GameObject> enableObjs;
            public List<GameObject> disableObjs;
            public bool shouldInitAds = false;

            public void ToggleObj()
            {
                if (enableObjs.Count > 0)
                {
                    foreach (GameObject obj in enableObjs)
                    {
                        if (obj != null)
                        {
                            obj.SetActive(true);
                        }
                    }

                    foreach (GameObject obj in disableObjs)
                    {
                        if (obj != null)
                        {
                            obj.SetActive(false);
                        }
                    }
                }
            }

        }
        public HostingReference hostingRef;


        [System.Serializable]
        public class InViewReference
        {
            public float distance = 5f;
            public List<GameObject> enableObjs;
            public List<GameObject> disableObjs;
            public bool shouldInitAds = false;

            public void ToggleObj()
            {
                if (enableObjs.Count > 0)
                {
                    foreach (GameObject obj in enableObjs)
                    {
                        if (obj != null)
                        {
                            obj.SetActive(true);
                        }
                    }

                    foreach (GameObject obj in disableObjs)
                    {
                        if (obj != null)
                        {
                            obj.SetActive(false);
                        }
                    }
                }
            }

        }
        public InViewReference inViewRef;


        [System.Serializable]
        public class Null_Reference
        {
            public List<GameObject> enableObjs;
            public List<GameObject> disableObjs;
            public bool shouldInitAds = false;

            public void ToggleObj()
            {
                if (enableObjs.Count > 0)
                {
                    foreach (GameObject obj in enableObjs)
                    {
                        if (obj != null)
                        {
                            obj.SetActive(true);
                        }
                    }

                    foreach (GameObject obj in disableObjs)
                    {
                        if (obj != null)
                        {
                            obj.SetActive(false);
                        }
                    }
                }
            }

        }
        public Null_Reference nullRef;


        [System.Serializable]
        public class Mwangaza
        {
            public List<Light> lights;
            public bool washaOnEnter = false;
            public bool zimaOnExit = true;

            public void Toggle(bool option)
            {
                foreach (Light light in lights)
                {
                    if (light != null)
                    {
                        light.enabled = option;
                    }
                }
            }
        }
        public Mwangaza mwangaza;

        public Transform customer;
        public MarketMaster marketMaster;
        public AD_Manager adManager;
        public List<Attendance> attendanceList;

        [Header("State AI")]
        public Sim_MarketInterface currentMarket;
        public Sim_MarketInterface capturedMarket;
        public Sim_VacantMarket vacantMarket;
        public Sim_ClosedMarket closedMarket;
        public Sim_HostingMarket hostingMarket;
        public Sim_InViewMarket inViewMarket;
        public Sim_NullMarket nullMarket;

        [Header("For fast travel")]
        public Transform spawnPoint;

        public bool customerIsPresent = false;
        public bool customerIsNear = false;

        private void Awake()
        {
            SetupMarketReferences();
        }

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

        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Player")
            {
                Attendance attendance = new Attendance();
                attendance.attendee = other.transform.root.GetComponent<CustomerMaster>();
                //todo:start counting duration
                attendanceList.Add(attendance);


                customer = other.transform.root;
                customerIsPresent = true;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.tag == "Player")
            {
                customer = null;
                customerIsPresent = false;
            }
        }

        void SetupMarketReferences()
        {
            vacantMarket = new Sim_VacantMarket(this);
            closedMarket = new Sim_ClosedMarket(this);
            hostingMarket = new Sim_HostingMarket(this);
            inViewMarket = new Sim_InViewMarket(this);
            nullMarket = new Sim_NullMarket(this);
        }

        void SetInitialReferences()
        {
            CheckCustomerPresence();
        }

        void SetUpdateReferences()
        {
            if (Time.time > nextCheck)
            {
                nextCheck = Time.time + checkRate;

                CheckCustomerPresence();

                currentMarket.UpdateMarket();

                
            }
        }

        public void SetMarket(Sim_MarketInterface market)
        {
            if (currentMarket != market)
            {
                currentMarket = market;
            }
        }

        void SaveState()
        {
            capturedMarket = currentMarket;
        }

        public void CheckCustomerPresence()
        {
            if(kiosks.Count < 0)
            {
                ActivateNullMarket();
                return;
            }

            //set host kiosk
            SetHostKiosk(GetHostKiosk());

            //set market state
            if(customerIsPresent)
            {
                ActivateHostingMarket();
                return;
            }
            else
            {
                if (!customerIsPresent && customerIsNear)
                {
                    ActivateInViewMarket();
                    return;
                }
                else if (!customerIsPresent && !customerIsNear)
                {
                    ActivateClosedMarket();
                    return;
                }
            }
        }

        public Kiosk GetHostKiosk()
        {
            if(kiosks.Count > 0)
            {
                var host = kiosks.Where(f => f.kioskPattern.customerIsPresent);
                return host.FirstOrDefault();
            }
            return null;
        }

        public void SetHostKiosk(Kiosk kiosk)
        {
            hostKiosk = kiosk;
        }

        public void ActivateVacantMarket()
        {
            SetMarket(vacantMarket);
        }

        public void ActivateClosedMarket()
        {
            SetMarket(closedMarket);
        }

        public void ActivateHostingMarket()
        {
            SetMarket(hostingMarket);
        }

        public void ActivateInViewMarket()
        {
            SetMarket(inViewMarket);
        }

        public void ActivateNullMarket()
        {
            SetMarket(nullMarket);
        }

        public void OnCustomerEnter()
        {

        }

        public void OnCustomerExit()
        {

        }
    }
}

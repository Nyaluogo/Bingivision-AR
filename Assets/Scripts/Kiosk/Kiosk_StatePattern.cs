using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static Chiro.Ad_PictureController;

namespace Chiro
{
    public class Kiosk_StatePattern : MonoBehaviour
    {

        [Header("Decision making")]

        private float checkRate = 0.1f;
        private float nextCheck;


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
        public class OccupiedReference
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
        public OccupiedReference occupiedRef;


        [System.Serializable]
        public class VacantReference
        {
            public List<GameObject> enableObjs;
            public List<GameObject> disableObjs;
            public bool shouldInitAds = false;

            public void ToggleObj()
            {
                if(enableObjs.Count > 0)
                {
                    foreach(GameObject obj in enableObjs)
                    {
                        if(obj != null)
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
        public VacantReference vacantRef;

        
        

        [System.Serializable]
        public class Mwangaza
        {
            public List<Light> lights;
            public bool toggleOnEnter = false;
            public bool toggleOnExit = false;

            public void Toggle(bool option)
            {
                foreach(Light light in lights)
                {
                    if(light != null)
                    {
                        light.enabled = option;
                    }
                }
            }
        }
        public Mwangaza mwangaza;

        public KioskMaster kioskMaster;
        public AD_Manager adManager;
        public Transform customer;

        [Header("State AI")]
        public IKiosk_StateInterface currentState;
        public IKiosk_StateInterface capturedState;
        public Kiosk_InViewState viewState;
        public Kiosk_OccupiedState occupiedState;
        public Kiosk_VacantState vacantState;

        public Transform defaultPosition;
        public Transform defaultLookAtPos;
        public Transform demoPosition;
        public Transform demoLookAtPos;

        public bool customerIsPresent = true;
        public bool customerIsNear = true;

        private void Awake()
        {
            SetupStateReferences();
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


        void SetupStateReferences()
        {
            viewState = new Kiosk_InViewState(this);
            occupiedState = new Kiosk_OccupiedState(this);
            vacantState = new Kiosk_VacantState(this);
        }

        void SetInitialReferences()
        {
            ActivateOccupiedState();
        }

        void SetUpdateReferences()
        {
            if (Time.time > nextCheck)
            {
                nextCheck = Time.time + checkRate;

                currentState.UpdateKiosk();
            }
        }

        public void CheckCustomerPresence()
        {
            if(customerIsPresent)
            {
                ActivateOccupiedState();
            }
            else
            {
                if(!customerIsPresent && customerIsNear)
                {
                    ActivateInViewState();
                }
                else if(!customerIsPresent && !customerIsNear)
                {
                    ActivateVacantState();
                }
            }
        }

        public void SetState(IKiosk_StateInterface state)
        {
            if(currentState != state)
            {
                currentState = state;
            }
        }

        public void SaveState()
        {
            capturedState = currentState;
        }

        public void ActivateInViewState()
        {
            SetState(viewState);
        }

        public void ActivateOccupiedState()
        {
            SetState(occupiedState);
        }

        public void ActivateVacantState()
        {
            SetState(vacantState);
        }

        public void SetDemoPosition()
        {
            if(demoPosition != null && customer != null && demoLookAtPos != null)
            {
                customer.SetPositionAndRotation(demoPosition.position,demoPosition.rotation);
                
            }
        }

        public void SetDefaultPosition()
        {
            if (defaultPosition != null && customer != null && defaultLookAtPos != null)
            {
                customer.SetPositionAndRotation(defaultPosition.position, defaultPosition.rotation);
                
            }
        }
    }
}

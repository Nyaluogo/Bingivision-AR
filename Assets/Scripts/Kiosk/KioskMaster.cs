using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Chiro
{
    public class KioskMaster : MonoBehaviour
    {

        [System.Serializable]
        public class Details
        {
            public string name;
            public string about;
            public string tagline;
            public string email;
            public string phonenumber;
            public string website;
            public string location;
            public string category;
            
            public string progress;
            public string yearFounded;
            public string userDescription;
            public string revenueStatus;
            public string investmentStatus;
            public string reasonForExistence;
            public string competition;
            public string financialProjection;


            public string legalDetails;
            public string fundraisingDetails;

            public string founderName;
            public string founderDetails;

            public string relatedProjects;
            public string faq;

            public Texture logo;
        }
        public Details details;


        public delegate void KioskEventHandler();
        public KioskEventHandler VacantEvent;
        public KioskEventHandler InCustomerViewEvent;
        public KioskEventHandler OccupiedEvent;

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

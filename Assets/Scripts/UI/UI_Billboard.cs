using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Chiro
{
    public class UI_Billboard : MonoBehaviour
    {
        public Camera cam;
        public Canvas billboardCanvas;

        [System.Serializable]
        public class Icon
        {
            public GameObject farObj;
            public GameObject nearObj;

            public void Toggle(bool isNear)
            {
                if (farObj != null && nearObj != null)
                { 
                    if(isNear)
                    {
                        nearObj.SetActive(true);
                        farObj.SetActive(false);
                    }
                    else
                    {
                        nearObj.SetActive(false);
                        farObj.SetActive(true);
                    }

                }
            }
        }
        public Icon icon;

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
            if(GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>() != null)
            {
                cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
            }
            if(GetComponent<Camera>() != null)
            {
                billboardCanvas = GetComponent<Canvas>();
                billboardCanvas.worldCamera = cam;
            }

            icon.Toggle(false);
        }

        void SetUpdateReferences()
        {
            transform.LookAt(transform.position + cam.transform.rotation * Vector3.forward, cam.transform.rotation * Vector3.up);

        }

        public void SetBillboardLabel(string label_text)
        {
            if(billboardCanvas != null)
            {
                if(billboardCanvas.GetComponentInChildren<Text>() != null)
                {
                    var txt = billboardCanvas.GetComponentInChildren<Text>();
                    txt.text = label_text;
                }
            }
        }
    }

}
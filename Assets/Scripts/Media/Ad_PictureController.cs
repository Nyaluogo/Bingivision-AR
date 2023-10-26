using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Chiro
{
    public class Ad_PictureController : MonoBehaviour
    {
        public enum PictureType 
        {
            BANNER,
            LOGO,
            NAMETAG,
            ART
        }

        [System.Serializable]
        public class Picha
        {
            public string name;
            public string description;
            public PictureType Type;
            public MeshRenderer meshRenderer;
            public Material material;
            public bool initOnStart = false;
            public bool isInitialised = false;

            public void Init()
            {
                if(meshRenderer != null && material != null)
                {
                    meshRenderer.material = material;
                    isInitialised = true;
                }
                else
                {
                    Debug.LogError("Picture resources missing");
                }
            }
        }

        public List<Picha> mapicha;
        public Material defaultMaterial;
        
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

        }
    }
}

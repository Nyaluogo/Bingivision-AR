using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Chiro
{
    public class CoridoorController : MonoBehaviour
    {
        [System.Serializable]
        public class WallAesthetic
        {
            public List<GameObject> wallList;
            
        }
        public List<WallAesthetic> walls;


        [System.Serializable]
        public class Poster
        {
            public GameObject frame;
            public MeshRenderer meshRenderer;

            public void Toggle(bool option)
            {
                if (frame != null)
                {
                    frame.SetActive(option);
                }
            }

            public void SetAd(Material mat)
            {
                if(meshRenderer != null)
                {
                    meshRenderer.sharedMaterial = mat;
                }
            }
        }
        public List<Poster> posters;
        public List<Material> ads;
        public bool shouldEnablePosters = true;


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
            if(posters.Count > 0)
            {
                if(shouldEnablePosters)
                {
                    foreach(Poster poster in posters)
                    {
                        poster.Toggle(shouldEnablePosters);

                        if(ads.Count > 0)
                        {
                            int index = Random.Range(0, ads.Count);
                            var selected = ads[index];

                            poster.SetAd(selected);
                        }
                    }
                }
            }
        }

        void SetUpdateReferences()
        {

        }
    }

}
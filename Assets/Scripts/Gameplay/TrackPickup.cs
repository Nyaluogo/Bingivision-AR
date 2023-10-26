using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Chiro.Ad_AudioController;
using static Chiro.Customer_PlaylistController;

namespace Chiro
{
    public class TrackPickup : MonoBehaviour
    {
        [System.Serializable]
        public class Pickup
        {
            public string name;
            public Track track;
            public bool isInitialised = false;
            public bool isCollected = false;
            public bool isFinalGoal = false;
        }
        public Pickup pickupTrack;


        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        private void OnTriggerEnter(Collider other)
        {
            if(other.transform.root.tag == "Player")
            {
                var playlist = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Customer_PlaylistController>();
                if (playlist != null)
                {
                    

                    playlist.OnPickpTrack(pickupTrack);
                    gameObject.SetActive(false);
                    //add health
                    Debug.Log("Track acquired");

                }
                CheckCompleteness();
            }
        }

        public void CheckCompleteness()
        {
            if(pickupTrack.isFinalGoal)
            {
                Debug.Log("GAME COMPLETE");
            }
        }

    }
}

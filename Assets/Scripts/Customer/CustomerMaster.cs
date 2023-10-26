
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Chiro
{
    public class CustomerMaster : MonoBehaviour
    {
        public enum PlayerControlState
        {
            MANUAL,
            AUTO
        }
        public PlayerControlState state = PlayerControlState.MANUAL;

        [System.Serializable]
        public class Details
        {
            public string name;
            public int age;
            public GameObject avatar;//TODO
        }
        public Details details;


        [System.Serializable]
        public class Attendance
        {
            public Sim_MarketPattern soko;
            public float duration;
            public bool isPresent = false;

            //todo: check ads stats
        }


        [System.Serializable]
        public class WindowShopping
        {
            public List<Transform> waypoints;
            public int currentDestination;
            public float delay = 1f;
            public float speed = 3.5f;
        }
        public WindowShopping windowShopping;

        public NavMeshAgent navMesh;

        

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
            switch (state)
            {
                case PlayerControlState.MANUAL:
                    break;
                case PlayerControlState.AUTO:
                    Patrol();
                    break;
                default:
                    break;
            }
        }

        
        public void ToggleLocomotion(bool toggle)
        {
            if (toggle)
            {
                KeepMoving();
            }
            else
            {
                StopMoving();
            }
        }

        public void KeepMoving()
        {

        }

        public void StopMoving()
        {

        }

        public void Patrol()
        {
            if(navMesh != null)
            {
                
                if(windowShopping.waypoints.Count <= 0) return;

                int index = Random.Range(0,windowShopping.waypoints.Count);
                //todo:random or linear series option
                navMesh.enabled = true;

                if (navMesh.remainingDistance <= navMesh.stoppingDistance)
                {
                    StartCoroutine(WaitAtPoint());
                }
                /*
                navMesh.SetDestination(windowShopping.waypoints[index].position);

                var points = windowShopping.waypoints;
                var curr = windowShopping.currentDestination;

                transform.position = Vector3.MoveTowards(transform.position, points[curr].position, windowShopping.speed * Time.deltaTime);

                if (Vector3.Distance(transform.position, points[curr].position) < 0.1f)
                {
                    curr = (curr + 1) % points.Count;
                }
                */
            }
        }

        private IEnumerator WaitAtPoint()
        {
            yield return new WaitForSeconds(windowShopping.delay);

            var points = windowShopping.waypoints;
            var curr = windowShopping.currentDestination;
            curr = (curr + 1) % points.Count;
            navMesh.SetDestination(points[curr].position);
        }

        public void SpawnAt(Transform spawnPoint)
        {
            
        }



    }
}

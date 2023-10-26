using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Chiro
{
    public class NafasController : MonoBehaviour
    {
        public enum NafasState
        {
            LOADING,
            SEARCHING,
            VICTORYLAP,
            TRAP
        }
        public NafasState currentState;

        
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        void SetInitialReferencees()
        {

        }

        void SetUpdateReferencees()
        {

        }

        public void SetState(NafasState state)
        {
            currentState = state;
        }

        public void StateMachine()
        {

        }
    }
}

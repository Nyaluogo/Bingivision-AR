using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Chiro
{
    public class Nafas_InitialState : INafasStateInterface
    {
        private NafasStatePattern nafas;

        public Nafas_InitialState(NafasStatePattern nafasStatePattern)
        {
            this.nafas = nafasStatePattern;
        }

        public void LosNafas()
        {
            

        }

        public void InitPlayer()
        {
            //TODO: set player position
            if(nafas != null)
            {
                if (nafas.mapLoaded)
                {
                    nafas.customer = nafas.perspective.GetCharacter().GetComponent<CustomerMaster>();
                    nafas.OnPlayerSpawn();
                }
            }
        }

        public void UpdateNafas()
        {
           LosNafas();
            InitPlayer();
            ToGrindState();
        }

        public void ToGrindState()
        {
            nafas.ActivateGrindState();
        }

        public void ToInitialState()
        {
            throw new System.NotImplementedException();
        }

        public void ToTrapHouseState()
        {
            throw new System.NotImplementedException();
        }

        public void ToVictoryLapState()
        {
            nafas.ActivateVictoryLapState();
        }

    }
}

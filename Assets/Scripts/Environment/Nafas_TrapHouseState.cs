using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Chiro
{
    public class Nafas_TrapHouseState : INafasStateInterface
    {
        private NafasStatePattern nafasStatePattern;

        public Nafas_TrapHouseState(NafasStatePattern nafasStatePattern)
        {
            this.nafasStatePattern = nafasStatePattern;
        }

        public void SuccessMessage()
        {
            
        }

        public void MoveToTraphouse()
        {

        }

        public void UpdateNafas()
        {
            SuccessMessage();
        }

        public void ToGrindState()
        {
            throw new System.NotImplementedException();
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
            throw new System.NotImplementedException();
        }

    }

}
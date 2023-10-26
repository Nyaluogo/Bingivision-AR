using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Chiro
{
    public class Nafas_VictoryLapState : INafasStateInterface
    {
        private NafasStatePattern nafas;
        bool trapIsActive = true;
        bool haterIsActive = false;

        public Nafas_VictoryLapState(NafasStatePattern nafasStatePattern)
        {
            this.nafas = nafasStatePattern;
        }

        public void ActivateHaters()
        {
            var victory = nafas.victoryRef;
            

        }

        public void SpawnFinalGoal()
        {
            var victory = nafas.victoryRef;
        }

        public void ActivateTraps()
        {
            var grind = nafas.grindRef;
            //grind.InitTraps(nafas.nafas);

            if (grind.totalTrapFloors.Count > 0)
            {
                if (grind.activatedtrapFloors.Count < grind.defaultNumActiveTraps)
                {
                    int bal = grind.defaultNumActiveTraps - grind.activatedtrapFloors.Count;

                    for (int i = 0; i < bal; i++)
                    {
                        int index = Random.Range(0, grind.totalTrapFloors.Count);
                        var selected = grind.totalTrapFloors[index];
                        if (!grind.activatedtrapFloors.Contains(selected))
                        {
                            selected.isChosen = true;
                            grind.activatedtrapFloors.Add(selected);
                        }
                    }
                }
            }
        }

        public void RefreshTraps()
        {
            var grind = nafas.grindRef;

            if (grind.totalTrapFloors.Count > 0)
            {
                if (grind.activatedtrapFloors.Count > 0)
                {
                    grind.activatedtrapFloors.RemoveAll(t => t.isChosen == false);

                }
            }
        }

        public void UpdateNafas()
        {
            CheckProgress();
            if(!haterIsActive)
            {
                ActivateHaters();
                haterIsActive = true;
            }
            trapIsActive = true;
            ActivateTraps();
            RefreshTraps();



        }

        public void CheckProgress()
        {
            if (nafas.playlistController != null)
            {
                var playlist = nafas.playlistController;

                if (playlist.PlaylistComplete())
                {
                    SpawnFinalGoal();
                    //ToTrapHouseState();
                }
            }
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
            nafas.ActivateTraphouseState();
        }

        public void ToVictoryLapState()
        {
            throw new System.NotImplementedException();
        }

    }
}

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Chiro
{
    public class Nafas_GrindState : INafasStateInterface
    {
        private NafasStatePattern nafas;
        private bool trapIsActive = false;

        public Nafas_GrindState(NafasStatePattern nafasStatePattern)
        {
            this.nafas = nafasStatePattern;
        }

        public void ActivateTraps()
        {
            var grind = nafas.grindRef;
            //grind.InitTraps(nafas.nafas);

            if(grind.totalTrapFloors.Count > 0)
            {
                if(grind.activatedtrapFloors.Count < grind.defaultNumActiveTraps)
                {
                    int bal = grind.defaultNumActiveTraps - grind.activatedtrapFloors.Count;

                    for(int i = 0; i < bal; i++)
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
                    grind.activatedtrapFloors.RemoveAll(t=>t.isChosen==false);
                    
                }
            }
        }

        public void CheckProgress()
        {
            if(nafas.playlistController != null)
            {
                var playlist = nafas.playlistController;

                if (playlist.GetUnlockedPlaylist().Count > (playlist.goalTracks.Count() * 0.5))
                {
                    ToVictoryLapState();
                    Debug.Log("Haters activated");
                    return;
                }

                if (playlist.GetUnlockedPlaylist().Count > (playlist.goalTracks.Count() * 0.2))
                {
                    if(!trapIsActive)
                    {
                        trapIsActive = true;
                        ActivateTraps();
                        return;
                    }
                }
                
            }
            
        }

        public void UpdateNafas()
        {
            CheckProgress();

            if (trapIsActive)
            {
                ActivateTraps();
            }
            RefreshTraps();
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
            nafas.ActivateVictoryLapState();
        }

    }

}
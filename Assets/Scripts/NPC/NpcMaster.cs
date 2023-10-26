using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Chiro
{
    public class NpcMaster : MonoBehaviour
    {
       
        [System.Serializable]
        public enum NpcType
        {
            VISITOR,
            KIOSK_ATTENDANT,
            MARKET_ATTENDANT,
            RAURA,
        }

        [System.Serializable]
        public enum MovementType
        {
            CRAWL,
            WALK,
            RUN,
            JUMP,
            SLIDE,
            FLY,
            TELEPORT
        }

        public NpcType type;
        public MovementType movementType;

        public delegate void GeneralEventHandler();
        public event GeneralEventHandler IdleEvent;
        public event GeneralEventHandler PatrolEvent;
        public event GeneralEventHandler AlertEvent;
        public event GeneralEventHandler InvestigationEvent;
        public event GeneralEventHandler TauntEvent;
        public event GeneralEventHandler MoveEvent;
        public event GeneralEventHandler PursueEvent;
        public event GeneralEventHandler MeleeAttackEvent;
        public event GeneralEventHandler RangeAttackEvent;
        public event GeneralEventHandler DefendEvent;
        public event GeneralEventHandler StruckEvent;
        public event GeneralEventHandler EventNpcRecoveredAnim;
        public event GeneralEventHandler FleeEvent;
        public event GeneralEventHandler EventNpcLowHealth;
        public event GeneralEventHandler EventNpcHealAnim;
        public event GeneralEventHandler EventNpcHealthRecovered;
        public event GeneralEventHandler DeadEvent;

        public delegate void HealthEventHandler(int health);
        public event HealthEventHandler EventNpcDeductHealth;
        public event HealthEventHandler EventNpcIncreaseHealth;

        public delegate void NPCRelationsChangeEventHandler();
        public event NPCRelationsChangeEventHandler EventNPCRelationsChange;



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

        public void CallEventNpcDie()
        {
            if (DeadEvent != null)
            {
                DeadEvent();
            }
        }

        public void CallEventNpcTaunt()
        {
            if (TauntEvent != null)
            {
                TauntEvent();
            }
        }
        public void CallEventNpcLowHealth()
        {
            if (EventNpcLowHealth != null)
            {
                EventNpcLowHealth();
            }
        }

        public void CallEventNpcHealthRecovered()
        {
            if (EventNpcHealthRecovered != null)
            {
                EventNpcHealthRecovered();
            }
        }

        //this should actually be a "run" animation
        public void CallEventNpcMove()
        {
            if (MoveEvent != null)
            {
                MoveEvent();
            }
        }

        public void CallEventNpcPatrolAnim()
        {
            if (PatrolEvent != null)
            {
                PatrolEvent();
            }
        }

        public void CallEventNpcPursueAnim()
        {
            if (PursueEvent != null)
            {
                PursueEvent();
            }
        }

        public void CallEventNpcFleeAnim()
        {
            if (FleeEvent != null)
            {
                FleeEvent();
            }
        }

        public void CallEventNpcRecoveredAnim()
        {
            if (EventNpcRecoveredAnim != null)
            {
                EventNpcRecoveredAnim();
            }
        }

        public void CallEventNpcHealAnim()
        {
            if (EventNpcHealAnim != null)
            {
                EventNpcHealAnim();
            }
        }

        public void CallEventNpcStruckAnim()
        {
            if (StruckEvent != null)
            {
                StruckEvent();
            }
        }

        public void CallEventNpcDieAnim()
        {
            if (DeadEvent != null)
            {
                DeadEvent();
            }
        }

        public void CallEventNpcMeleeAttackAnim()
        {
            if (MeleeAttackEvent != null)
            {
                MeleeAttackEvent();
            }
        }

        public void CallEventNpcRangeAttackAnim()
        {
            if (RangeAttackEvent != null)
            {
                RangeAttackEvent();
            }
        }



        public void CallEventNpcIdleAnim()
        {
            if (IdleEvent != null)
            {
                IdleEvent();
            }
        }

        public void CallEventNpcDefendAnim()
        {
            if (DefendEvent != null)
            {
                DefendEvent();
            }
        }

        public void CallEventInvestigationAnim()
        {
            if (InvestigationEvent != null)
            {
                InvestigationEvent();
            }
        }

        public void CallEventNpcDeductHealth(int health)
        {
            if (EventNpcDeductHealth != null)
            {
                EventNpcDeductHealth(health);
            }
        }

        public void CallEventNpcIncreaseHealth(int health)
        {
            if (EventNpcIncreaseHealth != null)
            {
                EventNpcIncreaseHealth(health);
            }
        }

        public void CallEventNPCRelationsChange()
        {
            if (EventNPCRelationsChange != null)
            {
                EventNPCRelationsChange();
            }
        }
    }
}

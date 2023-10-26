using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Chiro
{
    public interface INpcStateInterface
    {
        void UpdateState();
        void ToIdleState();
        void ToReceptionState();
        void ToDanceState();
        void ToPatrolState();
        void ToAlertState();
        void ToPursueState();
        void ToCloseConvoState();
        void ToFarConvoState();

        void ToFollowState();

        void ToInsaneState();
        void ToFleeState();

        void ToHideState();

        void ToInvestigationState();
    }
}

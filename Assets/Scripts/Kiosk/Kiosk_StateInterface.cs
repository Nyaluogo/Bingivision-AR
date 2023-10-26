using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Chiro
{
    public interface IKiosk_StateInterface
    {
        public void UpdateKiosk();
        public void ToVacantState();
        public void ToInViewState();
        public void ToOccupiedState();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Chiro
{
    public class UI_TransformButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        public bool buttonPressed;
        public bool buttonReleased;
        public void OnPointerDown(PointerEventData eventData)
        {
            buttonPressed = true;
            buttonReleased = false;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            buttonPressed = false;
            buttonReleased = true;
        }

        
    }

}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Chiro
{
    public class UI_Master : MonoBehaviour
    {
        public delegate void GeneralUIEventHandler();
        public event GeneralUIEventHandler MainMenuEvent;
        public event GeneralUIEventHandler PauseToggleEvent;
        public event GeneralUIEventHandler LoadingEvent;
        public event GeneralUIEventHandler MovieEvent;
        public event GeneralUIEventHandler PlaytimeEvent;
        public event GeneralUIEventHandler TutorialToggleEvent;
        public event GeneralUIEventHandler MapToggleEvent;
        public event GeneralUIEventHandler ErrorEvent;

        public delegate void KioskUIEventHandler();
        public event KioskUIEventHandler KioskReceptionEvent;
        public event KioskUIEventHandler KioskVideoEvent;
        public event KioskUIEventHandler KioskChatEvent;
        public event KioskUIEventHandler KioskSlideDeckEvent;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }


        public void CallEventMenuScene()
        {
            MainMenuEvent?.Invoke();
        }

        public void CallEventPause()
        {
            PauseToggleEvent?.Invoke();
        }

        public void CallEventLoading()
        {
            LoadingEvent?.Invoke();
        }

        public void CallEventPlaytime()
        {
            PlaytimeEvent?.Invoke();
        }

        public void CallEventTutorial()
        {
            TutorialToggleEvent?.Invoke();
        }

        public void CallEventMap()
        {
            MapToggleEvent?.Invoke();
        }

        public void CallEventError()
        {
            ErrorEvent?.Invoke();
        }

        public void CallEventMovie()
        {
            MovieEvent?.Invoke();
        }

        public void CallEventChat()
        {
            KioskChatEvent?.Invoke();
        }

        public void CallEventReception()
        {
            KioskReceptionEvent?.Invoke();
        }

        public void CallEventSlideDeck()
        {
            KioskSlideDeckEvent?.Invoke();
        }

        public void CallEventKioskVideo()
        {
            KioskVideoEvent?.Invoke();
        }

        public void QuitGame()
        {
            Application.Quit();
        }
    }
}

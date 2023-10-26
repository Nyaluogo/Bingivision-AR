using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Chiro
{
    public class UI_PauseMenuState : IUI_StateInterface
    {
        private UI_StatePattern pattern;

        public UI_PauseMenuState()
        {
        }

        public UI_PauseMenuState(UI_StatePattern uI_StatePattern)
        {
            this.pattern = uI_StatePattern;
            pattern.ToggleCustomerInput(false);

        }

        public void PauseTime()
        {
            if (Time.timeScale > 0)
            {
                Time.timeScale = 0;
            }
            
        }

        public void CheckInput()
        {
            if (pattern.inputRef.TogglePause())
            {
                

                pattern.ActivatePlaytime();
            }
        }

        public void UpdateState()
        {
            PauseTime();
            pattern.ResetUI();
            pattern.pauseRef.Init(true);
            CheckInput();
        }

        public void ToErrorState()
        {
            throw new System.NotImplementedException();
        }

        public void ToKioskChatState()
        {
            throw new System.NotImplementedException();
        }

        public void ToKioskReceptionState()
        {
            throw new System.NotImplementedException();
        }

        public void ToKioskSlideDeckState()
        {
            throw new System.NotImplementedException();
        }

        public void ToLoadingState()
        {
            throw new System.NotImplementedException();
        }

        public void ToMainMenuState()
        {
            pattern.ActivateMainMenu();
        }

        public void ToProductState()
        {
            throw new System.NotImplementedException();
        }

        public void ToPauseState()
        {
            throw new System.NotImplementedException();
        }

        public void ToPlaytimeState()
        {
            pattern.ActivatePlaytime();
        }

        public void ToTutorialState()
        {
            throw new System.NotImplementedException();
        }

        public void ToKioskMovieState()
        {
            throw new System.NotImplementedException();
        }

        public void ToRadioState()
        {
            throw new System.NotImplementedException();
        }
    }
}

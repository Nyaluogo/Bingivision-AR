using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Chiro
{
    public class UI_TutorialState : IUI_StateInterface
    {
        private UI_StatePattern pattern;

        public UI_TutorialState(UI_StatePattern uI_StatePattern)
        {
            this.pattern = uI_StatePattern;
        }


        public void HelpTime()
        {
            if (Time.timeScale > 0)
            {
                Time.timeScale = 0;
            }
        }

        public void CheckInput()
        {
            var input = pattern.inputRef; if (input == null) return;
            if (pattern.inputRef.TogglePause() || pattern.inputRef.ToggleTutorial())
            {
                ToPlaytimeState();
            }

            
        }

        public void UpdateState()
        {
            HelpTime();
            pattern.ResetUI();
            pattern.helpRef.Init(true);
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

        public void ToKioskMovieState()
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
            throw new System.NotImplementedException();
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

        public void ToRadioState()
        {
            throw new System.NotImplementedException();
        }

        public void ToTutorialState()
        {
            throw new System.NotImplementedException();
        }

        
    }
}

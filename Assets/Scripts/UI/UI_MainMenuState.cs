using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Chiro
{
    public class UI_MainMenuState : IUI_StateInterface
    {
        private UI_StatePattern pattern;

        public UI_MainMenuState(UI_StatePattern uI_StatePattern)
        {
            this.pattern = uI_StatePattern;
        }

        public void MainMenuTime()
        {
            if (Time.timeScale > 0)
            {
                Time.timeScale = 0;
            }
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.Confined;
            
        }

        public void CheckSettings()
        {
            var menu = pattern.mainMenuRef;
            var settings = pattern.mainMenuRef.settingsUI;
            if(settings.isRendering)
            {

            }
        }


        public void UpdateState()
        {
            MainMenuTime();
            pattern.ResetUI();
            pattern.mainMenuRef.Init(true);
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
            throw new System.NotImplementedException();
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

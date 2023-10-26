using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Chiro
{
    public class UI_LoadingState : IUI_StateInterface
    {
        private readonly UI_StatePattern pattern;
        private int minDuration = 5;
        private int maxDuration = 10;
        private bool isLoading = true;

        public UI_LoadingState(UI_StatePattern uI_StatePattern)
        {
            this.pattern = uI_StatePattern;
            pattern.ToggleCustomerInput(false);
        }

        public void LoadingTime()
        {
            if(Time.timeScale <= 0)
            {
                Time.timeScale = 1;
            }
        }

        public void UpdateState()
        {
            LoadingTime();
            if (!isLoading && pattern.loadingRef.duration <= 0)
            {
                ToPlaytimeState();
            }
            else
            {
                //pattern.player.gameObject.SetActive(false);
                pattern.ResetUI();
                pattern.loadingRef.Init(true);
                pattern.LoadGame();
            }
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

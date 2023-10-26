using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Chiro
{
    public class UI_KioskSlideDeckState : IUI_StateInterface
    {
        private UI_StatePattern pattern;

        public UI_KioskSlideDeckState(UI_StatePattern uI_StatePattern)
        {
            this.pattern = uI_StatePattern;
        }

        public void CheckInput()
        {
            if (pattern.inputRef.TogglePause() || pattern.inputRef.ToggleSlides())
            {
                ToPlaytimeState();
            }

            if (pattern.inputRef.PlaySlideshow())
            {
                Play();
            }
            else if (pattern.inputRef.PauseSlideshow())
            {
                Pause();
            }
            else if (pattern.inputRef.NextSlide())
            {
                Next();
            }
            else if (pattern.inputRef.PreviousSlide())
            {
                Previous();
            }
            
            else if (pattern.inputRef.ToggleFullScreen())
            {
                Fullscreen();
            }
        }

        public void Play()
        {
            try
            {
                pattern.movieRef.currentKiosk.adManager.GetFocusDeck().PlaySlideshow();
            }
            catch (System.Exception e)
            {
                Debug.LogError(e);
                throw;
            }
        }

        public void Pause()
        {
            try
            {
                pattern.movieRef.currentKiosk.adManager.GetFocusDeck().PauseSlideshow();
            }
            catch (System.Exception e)
            {
                Debug.LogError(e);
                throw;
            }
        }

        public void Next()
        {
            try
            {
                pattern.movieRef.currentKiosk.adManager.GetFocusDeck().NextSlide();
            }
            catch (System.Exception e)
            {
                Debug.LogError(e);
                throw;
            }
        }

        public void Previous()
        {
            try
            {
                pattern.movieRef.currentKiosk.adManager.GetFocusDeck().PreviousSlide();
            }
            catch (System.Exception e)
            {
                Debug.LogError(e);
                throw;
            }
        }

        public void Fullscreen()
        {
            try
            {
                pattern.movieRef.currentKiosk.adManager.GetFocusDeck().ToggleFullscreen();
            }
            catch (System.Exception e)
            {
                Debug.LogError(e);
                throw;
            }
        }

        public void SlideshowTime()
        {
            Time.timeScale = 1f;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }

        public void UpdateState()
        {
            pattern.ResetUI();
            pattern.deckRef.Toggle(true);
            SlideshowTime();
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
            pattern.ActivatePauseState();
        }

        public void ToPlaytimeState()
        {
            Cursor.visible = false;
            pattern.ActivatePlaytime();
        }

        public void ToTutorialState()
        {
            throw new System.NotImplementedException();
        }

        public void ToRadioState()
        {
            throw new System.NotImplementedException();
        }
    }
}

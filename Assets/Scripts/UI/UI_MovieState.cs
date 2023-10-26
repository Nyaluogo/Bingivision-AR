using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Chiro
{
    public class UI_MovieState : IUI_StateInterface
    {
        private UI_StatePattern pattern;

        public UI_MovieState(UI_StatePattern uI_StatePattern)
        {
            this.pattern = uI_StatePattern;
        }

        public void MovieTime()
        {
            Time.timeScale = 1.0f;
        }

        public void CheckInput()
        {
            if (pattern.inputRef.TogglePause() || pattern.inputRef.ToggleFilm())
            {
                ToPlaytimeState();
            }

            if(pattern.inputRef.PlayFilm())
            {
                Play();
            }
            else if (pattern.inputRef.PauseFilm())
            {
                Pause();
            }
            else if(pattern.inputRef.NextFilm())
            {
                Next();
            }
            else if (pattern.inputRef.PreviousFilm())
            {
                Previous();
            }
            else if (pattern.inputRef.MuteFilm())
            {
                Mute();
            }
            else if (pattern.inputRef.FullScreenFilm())
            {
                Fullscreen();
            }
        }

        public void Play()
        {
            try
            {
                pattern.movieRef.currentKiosk.adManager.GetFocusVideo().PlayFilm();
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
                pattern.movieRef.currentKiosk.adManager.GetFocusVideo().PauseFilm();
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
                pattern.movieRef.currentKiosk.adManager.GetFocusVideo().NextFilm();
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
                pattern.movieRef.currentKiosk.adManager.GetFocusVideo().PreviousFilm();
            }
            catch (System.Exception e)
            {
                Debug.LogError(e);
                throw;
            }
        }

        public void Mute()
        {
            try
            {
                pattern.movieRef.currentKiosk.adManager.GetFocusVideo().MuteFilm();
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
                pattern.movieRef.currentKiosk.adManager.GetFocusVideo().ToggleFullscreen();
            }
            catch (System.Exception e)
            {
                Debug.LogError(e);
                throw;
            }
        }


        public void UpdateState()
        {
            pattern.ResetUI();
            pattern.movieRef.Toggle(true);
            MovieTime();
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

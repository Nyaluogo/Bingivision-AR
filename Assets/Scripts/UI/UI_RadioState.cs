using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Chiro
{
    public class UI_RadioState : IUI_StateInterface
    {
        private UI_StatePattern pattern;

        public UI_RadioState(UI_StatePattern uI_StatePattern)
        {
            this.pattern = uI_StatePattern;
        }


        public void RadioTime()
        {
            Time.timeScale = 0.0f;
            
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.Confined;
        }

        public void CheckInput()
        {
            if (pattern.inputRef.TogglePause() || pattern.inputRef.ToggleRadio())
            {
                ToPlaytimeState();
            }

            if (pattern.inputRef.PlayTape())
            {
                Play();
            }
            else if (pattern.inputRef.PauseTape())
            {
                Pause();
            }
            else if (pattern.inputRef.NextTape())
            {
                Next();
            }
            else if (pattern.inputRef.PreviousTape())
            {
                Previous();
            }
            else if (pattern.inputRef.MuteRadio())
            {
                Mute();
            }
            else if (pattern.inputRef.ToggleLyrics())
            {
                var radio = pattern.radioRef;
                 radio.lyricsPanel.Toggle(!radio.lyricsPanel.panel.activeSelf);
            }
            else if (pattern.inputRef.IncreaseVolume())
            {
                IncreaseVolume();
            }
            else if (pattern.inputRef.DecreaseVolume())
            {
                DecreaseVolume();
            }

        }

        public void CreateUpdatePlaylist()
        {
            var radio = pattern.radioRef;

            if(radio.playlistController != null)
            {
                radio.CreatePlaylist();

                if(radio.playlistPanel.playlist.Count > 0)
                {
                    foreach(Button btn in radio.playlistPanel.playlist)
                    {

                    }
                }
            }
        }

        public void PrintNowPlaying()
        {
            var radio = pattern.radioRef;
            var track = radio.playlistController.currentTrack;

            if(track != null)
            {
                radio.nowPlayingPanel.Print(track);
            }
        }

        public void PrintLyrics()
        {
            var radio = pattern.radioRef;
            var track = radio.playlistController.currentTrack;

            if (track != null)
            {
                radio.lyricsPanel.SetLyrics(track);
            }
        }

        public void Play()
        {
            var radio = pattern.radioRef;
            var track = radio.playlistController.currentTrack;

            if (track != null)
            {
                radio.playlistController.PlayTape();
            }
        }

        public void Pause()
        {
            var radio = pattern.radioRef;
            var track = radio.playlistController.currentTrack;

            if (track != null)
            {
                radio.playlistController.PauseTape();
            }
        }

        public void Next()
        {
            var radio = pattern.radioRef;
            var track = radio.playlistController.currentTrack;

            if (track != null)
            {
                radio.playlistController.NextTape();
            }
        }

        public void Previous()
        {
            var radio = pattern.radioRef;
            var track = radio.playlistController.currentTrack;

            if (track != null)
            {
                radio.playlistController.PreviousTape();
            }
        }

        public void Mute()
        {
            var radio = pattern.radioRef;
            var track = radio.playlistController.currentTrack;

            if (track != null)
            {
                radio.playlistController.MuteTape();
            }
        }

        public void IncreaseVolume()
        {
            var radio = pattern.radioRef;
            var track = radio.playlistController.currentTrack;

            if (track != null)
            {
                var volume = radio.playlistController.audioSource.volume;
                volume = volume + 0.1f;
            }
        }

        public void DecreaseVolume()
        {
            var radio = pattern.radioRef;
            var track = radio.playlistController.currentTrack;

            if (track != null)
            {
                var volume = radio.playlistController.audioSource.volume;
                volume = volume - 0.1f;
            }
        }

        public void UpdateState()
        {
            pattern.ResetUI();
            pattern.radioRef.Toggle(true);
            RadioTime();
            CreateUpdatePlaylist();
            PrintNowPlaying();
            PrintLyrics();
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
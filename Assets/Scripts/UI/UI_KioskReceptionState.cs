using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Chiro
{
    public class UI_KioskReceptionState : IUI_StateInterface
    {
        private UI_StatePattern pattern;

        public UI_KioskReceptionState(UI_StatePattern uI_StatePattern)
        {
            this.pattern = uI_StatePattern;
        }

        public void ReceptionTime()
        {
            Time.timeScale = 1f;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }

        public void CheckInput()
        {
            if(pattern.inputRef.TogglePause()/* || pattern.inputRef.ToggleReception()*/)
            {
                ToPlaytimeState();
            }

            if(pattern.receptionRef.exitButton != null )
            {
                if(pattern.player != null)
                {
                    pattern.receptionRef.exitButton.onClick.RemoveAllListeners();
                    pattern.receptionRef.exitButton.onClick.AddListener(delegate
                    {
                        pattern.player.BackToLobby();
                        ToPlaytimeState();
                    });
                }
            }
        }

        public void SetChatUI(bool option)
        {
            if(pattern.player != null)
            {
                if(option)
                {
                    pattern.player.InitialiseUIRequest();
                }
                else
                {
                    pattern.player.BackToLobby();
                }
            }
        }


        public void UpdateState()
        {
            pattern.ResetUI();
            pattern.receptionRef.Toggle(true);
            //SetChatUI(true);
            ReceptionTime();
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

            Cursor.visible = false;
            //Cursor.lockState = CursorLockMode.None;
            SetChatUI(false);
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

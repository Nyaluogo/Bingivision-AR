using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Chiro
{
    public class UI_PlaytimeState : IUI_StateInterface
    {
        private readonly UI_StatePattern pattern;
        public UI_PlaytimeState(UI_StatePattern _pattern)
        {
            pattern = _pattern;
            PlayTime();
        }

        public void PlayTime()
        {
            Time.timeScale = 1;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            //ToggleDpad(true);
            CheckProduct();
        }

        public void CheckInput()
        {
            if (pattern.inputRef.TogglePause())
            {
                //ToggleDpad(false);

                ToPauseState();
                return;
            }
            else if (pattern.inputRef.ToggleMap())
            {
                //ToggleDpad(false);
                ToProductState();
                return;
            }
            
            else if(pattern.inputRef.ToggleFilm() && pattern.movieRef.CanInteract())
            {
                ToKioskMovieState();
                return;
            }
            else if(pattern.inputRef.ToggleRadio() )
            {
                //ToggleDpad(false);
                ToRadioState();
                return;
            }
            else if(pattern.inputRef.ToggleSlides() && pattern.deckRef.CanInteract())
            {
                ToKioskSlideDeckState();
                return;
            }
            else if (pattern.inputRef.ToggleReception() && pattern.receptionRef.CanInteract())
            {
                ToKioskReceptionState();
                return;
            }
        }

        public void ToggleDpad(bool option)
        {
            if (pattern.inputRef.mobipad == null) return;
            if (pattern.inputRef.mobipad.controlFreakCanvas.activeSelf != option)
            {
                pattern.inputRef.mobipad.Toggle(option);
            }
            else
            {
                //Debug.LogError("Controlf Freak Dpad Error");
            }
        }


        public void CheckProduct()
        {
            if(pattern.kiosk != null)
            {
                if(pattern.kiosk.adManager != null)
                {
                    if(pattern.kiosk.adManager.ad.productController != null)
                    {
                        if(pattern.kiosk.adManager.ad.productController.currentProduct != null)
                        {
                            pattern.kiosk.adManager.ad.productController.ResetProducts();
                        }
                    }
                }
            }
        }


        public void UpdateState()
        {
            pattern.ResetUI();
            pattern.hudRef.Init(true);
            PlayTime();
            //pattern.MarketConnection();
            CheckInput();
        }

        public void ToErrorState()
        {
            throw new System.NotImplementedException();
        }

        public void ToKioskChatState()
        {
            pattern.ActivateChatState();
        }

        public void ToKioskReceptionState()
        {
            pattern.ActivateReceptionState();
        }

        public void ToKioskSlideDeckState()
        {
            pattern.ActivateDeckState();
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
            pattern.ActivateProductState();
        }

        public void ToPauseState()
        {
            pattern.ActivatePauseState();
        }

        public void ToPlaytimeState()
        {
            throw new System.NotImplementedException();
        }

        public void ToTutorialState()
        {
            pattern.ActivateTutorialState();
        }

        public void ToKioskMovieState()
        {
            pattern.ActivateMovieState();
        }

        public void ToRadioState()
        {
            pattern.ActivateRadioState();
        }
    }
}

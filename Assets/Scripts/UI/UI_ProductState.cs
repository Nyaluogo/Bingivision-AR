using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Chiro
{
    public class UI_ProductState : IUI_StateInterface
    {
        private UI_StatePattern pattern;

        public bool isRotatingRight = false;

        public UI_ProductState(UI_StatePattern uI_StatePattern)
        {
            this.pattern = uI_StatePattern;
        }


        public void ProductTime()
        {
            Time.timeScale = 1;
        }

        public void CheckInput()
        {
            var prodRef = pattern.productRef; if (prodRef == null) return;
            var ad = prodRef.currentKiosk.adManager; if (ad == null) return;
            var prod = ad.GetFocusProduct(); 

            if(prod.currentProduct == null)
            {
                prod.SetProduct(prod.products[0]);
            }

            if (pattern.inputRef.TogglePause() )
            {
                ToPlaytimeState();
            }

            if (prodRef.RotationUpInput())
            {
                if (prod != null)
                {
                    prod.RotateUp();
                }
            }

            if (prodRef.RotationDownInput())
            {
                if(prod != null)
                {
                    prod.RotateDown();
                }
            }

            if (prodRef.RotationRightInput())
            {
                if (prod != null)
                {
                    prod.RotateRight();
                }
            }

            if (prodRef.RotationLeftInput())
            {
                if (prod != null)
                {
                    prod.RotateLeft();
                }
            }
            var resetBtn = prodRef.resetBtn;

            if (resetBtn != null)
            {
                //resetBtn.onClick.RemoveAllListeners();
                resetBtn.onClick.AddListener(delegate
                {
                    
                    if (prod != null)
                    {
                        prod.ResetProducts();
                        prod.SetProduct(prod.products[0]);
                    }
                });
            }

        }


        public void FastTravel(int mkt)
        {
            if (mkt < 0) return;
            try
            {
                ToPlaytimeState();
            }
            catch (System.Exception)
            {

                throw;
            }
        }

        public void UpdateState()
        {
            ProductTime();
            pattern.ResetUI();
            pattern.productRef.Toggle(true);
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Chiro
{
    public interface IUI_StateInterface
    {
        public void ToMainMenuState();
        public void ToPauseState();
        public void ToLoadingState();
        public void ToPlaytimeState();
        public void ToErrorState();
        public void ToTutorialState();
        public void ToProductState();
        public void ToKioskChatState();
        public void ToKioskReceptionState();
        public void ToKioskSlideDeckState();
        public void ToRadioState();
        public void ToKioskMovieState();
        public void UpdateState();
    }
}

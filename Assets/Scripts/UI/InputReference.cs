using ControlFreak2;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Chiro
{
    public class InputReference : MonoBehaviour
    {
        [System.Serializable]
        public class GamePadDpad
        {
            
            public bool IsLeft, IsRight, IsUp, IsDown;
            private float _LastX, _LastY;

            public void Check()
            {
                float x = Input.GetAxis("Horizontal");
                float y = Input.GetAxis("Vertical");

                IsLeft = false;
                IsRight = false;
                IsUp = false;
                IsDown = false;

                if (_LastX != x)
                {
                    if (x == -1)
                        IsLeft = true;
                    else if (x == 1)
                        IsRight = true;
                }

                if (_LastY != y)
                {
                    if (y == -1)
                        IsDown = true;
                    else if (y == 1)
                        IsUp = true;
                }

                _LastX = x;
                _LastY = y;
            }
        }
        public GamePadDpad dPad;

        [System.Serializable]
        public class MobileDPad
        {
            public GameObject controlFreakCanvas;//controlFreakPanel;
            public TouchButton actionButton;
            public TouchButton pauseButton;

            public void Toggle(bool option)
            {
                if (controlFreakCanvas != null)
                {
                    controlFreakCanvas.GetComponentInChildren<Canvas>().enabled = option;
                    controlFreakCanvas.gameObject.SetActive(option);
                }
            }

            public bool ClickAction()
            {
                if(actionButton != null)
                {
                   return actionButton.JustReleased();
                }
                return false;
            }

            public bool ClickPause()
            {
                if (pauseButton != null)
                {
                    return pauseButton.JustReleased();
                }
                return false;
            }
        }

        public MobileDPad mobipad;

        [Header("pause")]
        public KeyCode togglePauseKeyboard = KeyCode.Escape;
        public KeyCode togglePauseJoystick = KeyCode.Joystick1Button7;
        public KeyCode toggleHelpKeyboard = KeyCode.F1;
        public KeyCode toggleHelpJoystick;

        [Header("reception")]
        public KeyCode toggleReceptionKeyboard = KeyCode.R;
        public KeyCode toggleReceptionJoystick = KeyCode.Joystick1Button3;

        [Header("Slide deck")]
        public KeyCode toggleDeckKeyboard = KeyCode.R;
        public KeyCode toggleDeckJoystick = KeyCode.Joystick1Button3;
        public KeyCode playSlideshowKeyboard = KeyCode.P;
        public KeyCode playSlideshowJoystick;
        public KeyCode pauseSlideshowKeyboard = KeyCode.P;
        public KeyCode pauseSlideshowJoystick;
        public KeyCode nextSlideKeyboard = KeyCode.N;
        public KeyCode nextSlideJoystick = KeyCode.Joystick1Button5;
        public KeyCode previousSlideKeyboard = KeyCode.B;
        public KeyCode previousSlideJoystick = KeyCode.Joystick1Button4;
        public KeyCode fullScreenSlideKeyboard = KeyCode.F;
        public KeyCode fullScreenSlideJoystick = KeyCode.Joystick1Button9;

        [Header("Films")]
        public KeyCode toggleFilmKeyboard = KeyCode.R;
        public KeyCode toggleFilmJoystick = KeyCode.Joystick1Button3;
        public KeyCode nextFilmKeyboard = KeyCode.N;
        public KeyCode nextFilmJoystick = KeyCode.Joystick1Button5;
        public KeyCode previousFilmKeyboard = KeyCode.B;
        public KeyCode previousFilmJoystick = KeyCode.Joystick1Button4;
        public KeyCode playFilmKeyboard = KeyCode.P;
        public KeyCode playFilmJoystick;
        public KeyCode pauseFilmKeyboard = KeyCode.P;
        public KeyCode pauseFilmJoystick;
        public KeyCode muteFilmKeyboard = KeyCode.M;
        public KeyCode muteFilmJoystick;
        public KeyCode fullScreenFilmKeyboard = KeyCode.F;
        public KeyCode fullScreenFilmJoystick = KeyCode.Joystick1Button9;

        [Header("Radio")]
        public KeyCode toggleRadioKeyboard = KeyCode.R;
        public KeyCode toggleRadioJoystick = KeyCode.Joystick1Button3;
        public KeyCode nextRadioKeyboard = KeyCode.N;
        public KeyCode nextRadioJoystick = KeyCode.Joystick1Button5;
        public KeyCode previousRadioKeyboard = KeyCode.B;
        public KeyCode previousRadioJoystick = KeyCode.Joystick1Button4;
        public KeyCode playRadioKeyboard = KeyCode.P;
        public KeyCode playRadioJoystick;
        public KeyCode pauseRadioKeyboard = KeyCode.P;
        public KeyCode pauseRadioJoystick;
        public KeyCode lyricsRadioKeyboard = KeyCode.L;
        public KeyCode lyricsRadioJoystick;
        public KeyCode muteRadioJoystick;
        public KeyCode muteRadioKeyboard = KeyCode.M;
        public KeyCode increaseVolumeKeyboard = KeyCode.F8;
        public KeyCode decreaseVolumeKeyboard = KeyCode.F9;
        public KeyCode toggleProToolsKeyboard = KeyCode.F9;
        public KeyCode toggleProToolsJoystick = KeyCode.Joystick1Button9;
        

        [Header("Map")]
        public KeyCode toggleMapKeyboard = KeyCode.Tab;
        public KeyCode toggleMapJoystick = KeyCode.Joystick1Button6;

        // Start is called before the first frame update
        void Start()
        {
            SetInitialReferences();
        }

        // Update is called once per frame
        void Update()
        {
            SetUpdateReferences();
        }

        void SetInitialReferences()
        {

        }

        void SetUpdateReferences()
        {
            dPad.Check();
        }


        public bool TogglePause()
        {
            return mobipad.ClickPause() || Input.GetKeyDown(togglePauseKeyboard) || Input.GetKeyDown(togglePauseJoystick);
        }

        public bool ToggleTutorial()
        {
            return Input.GetKeyDown(toggleHelpKeyboard) || Input.GetKeyDown(toggleHelpJoystick);
        }

        public bool ToggleReception()
        {
            return mobipad.ClickAction() || Input.GetKeyDown(toggleReceptionKeyboard) || Input.GetKeyDown(toggleReceptionJoystick);
        }

        public bool ToggleFilm()
        {
            return mobipad.ClickAction() || Input.GetKeyDown(toggleFilmKeyboard) || Input.GetKeyDown(toggleFilmJoystick);
        }

        public bool ToggleSlides()
        {
            return mobipad.ClickAction() || Input.GetKeyDown(toggleDeckKeyboard) || Input.GetKeyDown(toggleDeckJoystick);
        }

        public bool ToggleRadio()
        {

            return mobipad.ClickAction() || Input.GetKeyDown(toggleRadioKeyboard) || Input.GetKeyDown(toggleRadioJoystick);
        }

        public bool ToggleMap()
        {

            return Input.GetKeyDown(toggleMapKeyboard) || Input.GetKeyDown(toggleMapJoystick);
        }

        public bool PlayFilm()
        {
            return Input.GetKeyDown(playFilmKeyboard) || dPad.IsUp;
        }

        public bool PauseFilm()
        {
            return Input.GetKeyDown(pauseFilmKeyboard) || dPad.IsDown;
        }

        public bool NextFilm()
        {
            return Input.GetKeyDown(nextFilmKeyboard) || Input.GetKeyDown(nextFilmJoystick);
        }

        public bool PreviousFilm()
        {
            return Input.GetKeyDown(previousFilmKeyboard) || Input.GetKeyDown(previousFilmJoystick);
        }

        public bool MuteFilm()
        {
            return Input.GetKeyDown(muteFilmKeyboard) || Input.GetKeyDown(muteFilmJoystick);
        }

        /*
         RADIO
         */
        public bool PlayTape()
        {
            return Input.GetKeyDown(playRadioKeyboard) || dPad.IsUp;
        }

        public bool PauseTape()
        {
            return Input.GetKeyDown(pauseRadioKeyboard) || dPad.IsDown;
        }

        public bool NextTape()
        {
            return Input.GetKeyDown(nextRadioKeyboard) || Input.GetKeyDown(nextRadioJoystick);
        }

        public bool PreviousTape()
        {
            return Input.GetKeyDown(previousRadioKeyboard) || Input.GetKeyDown(previousRadioJoystick);
        }

        public bool MuteRadio()
        {
            return Input.GetKeyDown(muteRadioKeyboard) || Input.GetKeyDown(muteRadioJoystick);
        }

        public bool ToggleLyrics()
        {
            return Input.GetKeyDown(lyricsRadioKeyboard) || Input.GetKeyDown(lyricsRadioJoystick);
        }

        public bool ToggleProtools()
        {
            return Input.GetKeyDown(toggleProToolsKeyboard) || Input.GetKeyDown(toggleProToolsJoystick);
        }

        public bool IncreaseVolume()
        {
            return Input.GetKeyDown(increaseVolumeKeyboard) || dPad.IsRight;
        }

        public bool DecreaseVolume()
        {
            return Input.GetKeyDown(decreaseVolumeKeyboard) || dPad.IsLeft;
        }

        /*MOVIE*/

        public bool FullScreenFilm()
        {
            return Input.GetKeyDown(fullScreenFilmKeyboard) || Input.GetKeyDown(fullScreenFilmJoystick);
        }

        public bool PlaySlideshow()
        {
            return Input.GetKeyDown(playSlideshowKeyboard) || dPad.IsUp;
        }

        public bool PauseSlideshow()
        {
            return Input.GetKeyDown(pauseSlideshowKeyboard) || dPad.IsDown;
        }

        public bool NextSlide()
        {
            return Input.GetKeyDown(nextSlideKeyboard) || Input.GetKeyDown(nextSlideJoystick);
        }

        public bool PreviousSlide()
        {
            return Input.GetKeyDown(previousSlideKeyboard) || Input.GetKeyDown(previousSlideJoystick);
        }

        public bool ToggleFullScreen()
        {
            return Input.GetKeyDown(fullScreenSlideKeyboard) || Input.GetKeyDown(fullScreenSlideJoystick);
        }

        //MAP
        public bool Teleport2Market_1()
        {
            return Input.GetKeyDown(KeyCode.Alpha1);
        }

        public bool Teleport2Market_2()
        {
            return Input.GetKeyDown(KeyCode.Alpha2);
        }

        public bool Teleport2Market_3()
        {
            return Input.GetKeyDown(KeyCode.Alpha3);
        }

        public bool Teleport2Market_4()
        {
            return Input.GetKeyDown(KeyCode.Alpha4);
        }

        public bool Teleport2Market_5()
        {
            return Input.GetKeyDown(KeyCode.Alpha5);
        }

    }
}


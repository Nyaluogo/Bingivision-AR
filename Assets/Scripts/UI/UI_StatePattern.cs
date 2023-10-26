using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.UI;
using UnityEngine.Video;
using UnityEngine.InputSystem;
using ControlFreak2;
using TMPro;
using static Chiro.UI_StatePattern.HUDReference.Notification;
using static Chiro.Ad_AudioController;
using static Chiro.Customer_PlaylistController;
using static Chiro.NafasStatePattern.GraphicsQty;
using System.Linq;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using Inworld.Sample;
using Unity.VisualScripting;
//using OpenAI;

namespace Chiro
{
    public class UI_StatePattern : MonoBehaviour
    {
        [Header("Decision making")]
        public float checkRate = 0.1f;
        public float nextCheck;

        public Kiosk_StatePattern kiosk;
        public InputReference inputRef;


        [System.Serializable]
        public class SettingsUI
        {
            public GameObject settingsPanel;

            [System.Serializable]
            public class Game
            {
                public GameObject gamePanel;
                public Button gameButton;
                public TMP_Dropdown difficultyDropdown;
                public TMP_Dropdown dhokDropdown;
                public TMP_Dropdown perspectiveDropdown;
                public TMP_Dropdown aestheticDropdown;
            }
            public Game game;

            [System.Serializable]
            public class Sound
            {
                public Button soundButton;
                public GameObject soundPanel;
                public TMP_Dropdown albumDropdown;
                public TMP_Dropdown ambienceDropdown;
                public Scrollbar volumeScrollbar;
                public Scrollbar pitchScrollbar;
            }
            public Sound sound;

            [System.Serializable]
            public class Graphics
            {
                public Button graphicsButton;
                public GameObject graphicsPanel;
                public TMP_Dropdown qualityDropdown;
                public TMP_Dropdown hudDropdown;
            }
            public Graphics graphics;
            
            public bool isRendering = false;

            public void Init(NafasStatePattern nafas)
            {
                //init dfficulty button
                if(game.difficultyDropdown != null)
                {
                    var drop = game.difficultyDropdown;

                    drop.onValueChanged.RemoveAllListeners();
                    drop.onValueChanged.AddListener(delegate
                    {
                        if(drop.value == 0)
                        {
                            nafas.SetDifficulty(NafasStatePattern.Difficulty.TekoLevel.FRESHER);
                        }
                        else if (drop.value == 1)
                        {
                            nafas.SetDifficulty(NafasStatePattern.Difficulty.TekoLevel.TRAPPER);
                        }
                        else if (drop.value == 2)
                        {
                            nafas.SetDifficulty(NafasStatePattern.Difficulty.TekoLevel.TRAP_LORD);
                        }
                        else
                        {
                            nafas.SetDifficulty(NafasStatePattern.Difficulty.TekoLevel.TRAPPER);
                        }
                    });
                }

                //init perspextive dropdown button
                if (game.perspectiveDropdown != null)
                {
                    var drop = game.perspectiveDropdown;

                    drop.onValueChanged.RemoveAllListeners();
                    drop.onValueChanged.AddListener(delegate
                    {
                        if (drop.value == 0)
                        {
                            nafas.perspective.SetPerspective(NafasStatePattern.Perspective.PerspectiveType.TPS);
                        }
                        else if (drop.value == 1)
                        {
                            nafas.perspective.SetPerspective(NafasStatePattern.Perspective.PerspectiveType.FPS);
                        }
                        else
                        {
                            nafas.perspective.SetPerspective(NafasStatePattern.Perspective.PerspectiveType.TPS);
                        }
                    });
                }

                //init aesthetic dropdown button
                if (game.aestheticDropdown != null)
                {
                    var drop = game.aestheticDropdown;

                    drop.onValueChanged.RemoveAllListeners();
                    drop.onValueChanged.AddListener(delegate
                    {
                        if(nafas.kits.Count > 0)
                        {
                            var kits = nafas.kits;
                            if (drop.value == 0 && kits[0] != null)
                            {
                                nafas.SetAesthetic(kits[0]);
                            }
                            else if ( kits[drop.value] != null)
                            {
                                nafas.SetAesthetic(kits[drop.value]);
                            }
                            else
                            {
                                nafas.SetAesthetic(kits[0]);
                            }
                        }
                        
                    });
                }

                //init album dropdown button
                if (sound.albumDropdown != null)
                {
                    var drop = sound.albumDropdown;

                    drop.onValueChanged.RemoveAllListeners();
                    drop.onValueChanged.AddListener(delegate
                    {
                        if(nafas.playlistController != null)
                        {
                            var playCtrl = nafas.playlistController;
                            var playlists = playCtrl.rawPlaylists;
                            

                            if (drop.value == 0 && playlists[0] != null)
                            {
                                playCtrl.SetPlaylist(playlists[0]);
                            }
                            else if (playlists[drop.value] != null)
                            {
                                playCtrl.SetPlaylist(playlists[drop.value]);
                            }
                            else
                            {
                                playCtrl.SetPlaylist(playlists[0]);
                            }
                            
                        }

                    });
                }

                //set volume
                if (sound.volumeScrollbar != null)
                {
                    var volumeScrollbar = sound.volumeScrollbar;

                    volumeScrollbar.onValueChanged.RemoveAllListeners();
                    volumeScrollbar.onValueChanged.AddListener(delegate
                    {
                        if (nafas.playlistController != null)
                        {
                            var playCtrl = nafas.playlistController;
                            var audio_source = playCtrl.audioSource;


                            audio_source.volume = volumeScrollbar.value;

                        }
                    });
                }

                //set pitch
                if (sound.pitchScrollbar != null)
                {
                    var pitchScrollbar = sound.pitchScrollbar;


                    pitchScrollbar.onValueChanged.RemoveAllListeners();
                    pitchScrollbar.onValueChanged.AddListener(delegate
                    {
                        if (nafas.playlistController != null)
                        {
                            var playCtrl = nafas.playlistController;
                            var audio_source = playCtrl.audioSource;

                            audio_source.pitch = pitchScrollbar.value;
                        }
                    });
                }


                //init graphics quality dropdown button
                if (graphics.qualityDropdown != null)
                {
                    var drop = graphics.qualityDropdown;

                    drop.onValueChanged.RemoveAllListeners();
                    drop.onValueChanged.AddListener(delegate
                    {
                        if (drop.value == 0)
                        {
                            nafas.SetGraphicsQuality(GraphicsLevel.LOW);
                        }
                        else if (drop.value == 1)
                        {
                            nafas.SetGraphicsQuality(GraphicsLevel.MID);
                        }
                        else if (drop.value == 2)
                        {
                            nafas.SetGraphicsQuality(GraphicsLevel.HIGH);
                        }
                        else
                        {
                            nafas.SetGraphicsQuality(GraphicsLevel.MID);
                        }

                    });
                }
            }
           
        }

        [System.Serializable]
        public class PlayPauseBtn
        {
            public Button playPauseButton;
            public Sprite playUI;
            public Sprite pauseUI;

            public void SetPlaySprite()
            {
                if(playPauseButton != null && playUI != null)
                {
                    playPauseButton.GetComponent<Image>().sprite = playUI;
                }
            }

            public void SetPauseSprite()
            {
                if (playPauseButton != null && pauseUI != null)
                {
                    playPauseButton.GetComponent<Image>().sprite = pauseUI;
                }
            }
        }

        [System.Serializable]
        public class MainMenuReference
        {
            public Canvas canvas;
            public Button startButton;
            public Button settingsButton;
            public Button aboutButton;
            public Button exitButton;

            public SettingsUI settingsUI;
            

            public void Init(bool option)
            {
                if(canvas != null)
                {
                    canvas.gameObject.SetActive(option);
                    canvas.enabled = option;
                }
            }

        }
        public MainMenuReference mainMenuRef;

        [System.Serializable]
        public class LoadingReference
        {
            public Canvas loadingScreen;
            public Slider slider;
            public List<AudioClip> audioClipList;
            public int duration = 5;
            public bool isLoading = false;

            public void Init(bool option)
            {
                if (loadingScreen != null)
                {
                    loadingScreen.gameObject.SetActive(option);
                    loadingScreen.enabled = option;
                }
            }
        }
        public LoadingReference loadingRef;

        [System.Serializable]
        public class PauseReference
        {
            public Canvas canvas;
            public Button continueButton;
            public Button settingsButton;
            public Button aboutButton;
            public Button quitButton;
            public bool isPaused = false;

            public void Init(bool option)
            {
                if (canvas != null)
                {
                    canvas.gameObject.SetActive(option);
                    canvas.enabled = option;
                }
            }

            
        }
        public PauseReference pauseRef;


        [System.Serializable]
        public class TutorialReference
        {
            public Canvas canvas;
            public UnityEngine.UI.Text helpText;
            public Button closeButton;
            public bool isPaused = false;

            public void Init(bool option)
            {
                if (canvas != null)
                {
                    canvas.gameObject.SetActive(option);
                    canvas.enabled = option;
                }
            }


        }
        public TutorialReference helpRef;

        [System.Serializable]
        public class GameOverReference
        {
            public Canvas canvas;
            public UnityEngine.UI.Text helpText;
            public Button retryButton;
            public Button exitButton;
            public bool shouldToggleOnEnd = false;
            public bool isPaused = false;

            public void Init(bool option)
            {
                if (canvas != null)
                {
                    canvas.gameObject.SetActive(option);
                    canvas.enabled = option;
                }
            }


        }
        public GameOverReference gameOverRef;

        [System.Serializable]
        public class MovieUI
        {
            public Canvas movieCanvas;
            public Transform controlPanel;
            public VideoPlayer videoPlayer;
            public TextMeshProUGUI titleText;
            public Button collapseScreenButton;
            public PlayPauseBtn playPauseBtn;
            
            public Button nextButton;
            public Button previousButton;
            public Button muteButton;
            public Slider volumeSlider;
            public bool isInitialised = false;
            public bool isFullScreen = false;

            public Kiosk_StatePattern currentKiosk;

            public void Init()
            {
                if (currentKiosk == null) return;
                

                InitButtons();
            }

            public void SetTitle(string title)
            {
                if(titleText != null)
                {
                    titleText.text = title;
                }
            }

            public void Toggle(bool option)
            {
                if (movieCanvas != null)
                {
                    movieCanvas.gameObject.SetActive(option);
                    movieCanvas.enabled = option;

                    ToggleControlPanel(option);
                }
            }

            public void ToggleControlPanel(bool option)
            {
                if(controlPanel != null)
                {
                    controlPanel.gameObject.SetActive(option);
                }
            }

            public void InitButtons()
            {
                if (currentKiosk == null) return;
                if (currentKiosk.adManager == null) return;

                var ad = currentKiosk.adManager;
                //set playpause btn
                
                if (playPauseBtn.playPauseButton != null)
                {
                    playPauseBtn.playPauseButton.onClick.RemoveAllListeners();
                    playPauseBtn.playPauseButton.onClick.AddListener(delegate
                    {
                        if(ad.GetFocusVideo() != null )
                        {
                            
                            
                            if(videoPlayer.isPlaying)
                            {
                                ad.GetFocusVideo().PauseFilm();
                                playPauseBtn.SetPlaySprite();
                            }
                            else
                            {
                                ad.GetFocusVideo().PlayFilm();
                                playPauseBtn.SetPauseSprite();
                            }

                            SetTitle(ad.GetFocusVideo().currentFilm.name);
                        }
                        
                    });
                }

                

                //set next btn
                if (nextButton != null)
                {
                    nextButton.onClick.RemoveAllListeners();
                    nextButton.onClick.AddListener(delegate
                    {
                        if (ad.GetFocusVideo() != null)
                        {
                            ad.GetFocusVideo().NextFilm();

                            SetTitle(ad.GetFocusVideo().currentFilm.name);
                        }
                    });
                }

                //set previous btn
                if (previousButton != null)
                {
                    previousButton.onClick.RemoveAllListeners();
                    previousButton.onClick.AddListener(delegate
                    {
                        if (ad.GetFocusVideo() != null)
                        {
                            ad.GetFocusVideo().PreviousFilm();

                            SetTitle(ad.GetFocusVideo().currentFilm.name);
                        }
                    });
                }

                //set play btn
                if (muteButton != null)
                {
                    muteButton.onClick.RemoveAllListeners();
                    muteButton.onClick.AddListener(delegate
                    {
                        if (ad.GetFocusVideo() != null)
                        {
                            ad.GetFocusVideo().MuteFilm();

                            SetTitle(ad.GetFocusVideo().currentFilm.name);
                        }
                    });
                }

                if(volumeSlider != null)
                {
                    volumeSlider.onValueChanged.RemoveAllListeners();
                    volumeSlider.onValueChanged.AddListener(delegate
                    {
                        if (ad.GetFocusVideo() != null)
                        {
                            if(ad.GetFocusVideo().mediaPlayer != null)
                            {
                                if(ad.GetFocusVideo().mediaPlayer.canSetDirectAudioVolume)
                                {
                                    ad.GetFocusVideo().SetVolume(volumeSlider.value);
                                }
                            }

                        }
                    });
                }
                
            }

            public bool CanInteract()
            {
                if (currentKiosk != null)
                {
                    if (currentKiosk.adManager != null)
                    {
                        if (currentKiosk.adManager.GetFocusVideo() != null)
                        {
                            return true;
                        }
                    }
                }
                return false;
            }
        }
        public MovieUI movieRef;

        [System.Serializable]
        public class RadioUI
        {
            public Transform radioRoot;
            public Canvas radioCanvas;
            public Transform controlPanel;
            public GameObject mediaPlayerRoot;
            public GameObject proToolsRoot;

            [System.Serializable]
            public class PlaylistPanel
            {
                public GameObject panel;
                public GameObject trackPrefab;
                public Transform trackRoot;
                public List<Button> playlist;//the spawned tracks
                public bool isInitialised = false;
                
            }
            public PlaylistPanel playlistPanel;

            [System.Serializable]
            public class LyricsPanel
            {
                public GameObject panel;
                public TextMeshProUGUI lyricsText;
                public Button lyricsButton;


                public void Toggle(bool option)
                {
                    if(panel != null)
                    {
                        panel.SetActive(option);
                    }
                }

                public void SetLyrics(Track track)
                {
                    if(lyricsText != null)
                    {
                        lyricsText.text = track.lyrics;
                    }
                    else
                    {
                        lyricsText.text = "Onge Lyrics.";
                    }
                }
            }
            public LyricsPanel lyricsPanel;

            [System.Serializable]
            public class NowPlayingPanel
            {
                public TextMeshProUGUI titleTextMesh;
                public TextMeshProUGUI artistTextMesh;

                public void Print(Track track)
                {
                    if(titleTextMesh != null)
                    {
                        if(track.clip != null)
                        {
                            titleTextMesh.text = track.clip.name;
                        }
                        
                    }

                    if(artistTextMesh != null)
                    {
                        artistTextMesh.text = track.artistName;
                    }
                }
            }
            public NowPlayingPanel nowPlayingPanel;

            
            public PlayPauseBtn playPauseBtn;
            
            public Button nextButton;
            public Button previousButton;
            
            public Button shuffleButton;
            public Scrollbar volumeScrollbar;

            public bool isInitialised = false;

            public Customer_PlaylistController playlistController;

            

            public void Init(Customer_PlaylistController playlist)
            {
                playlistController = playlist;

            }

            public void Toggle(bool option)
            {
                if (radioCanvas != null)
                {
                    radioRoot.gameObject.SetActive(option);
                    radioCanvas.gameObject.SetActive(option);
                    radioCanvas.enabled = option;
                    if (radioCanvas.GetComponent<CanvasGroup>() != null)
                    {
                        radioCanvas.GetComponent<CanvasGroup>().interactable = option;
                    }
                    //ToggleMediaPlayer(option);
                    ToggleControlPanel(option);
                }
            }

            public void ToggleControlPanel(bool option)
            {
                if (controlPanel != null)
                {
                    controlPanel.gameObject.SetActive(option);
                }
            }

            public void ToggleMediaPlayer(bool option)
            {
                if (mediaPlayerRoot != null)
                {
                    mediaPlayerRoot.gameObject.SetActive(option);
                }
            }

            public void ToggleProdTools(bool option)
            {
                if (proToolsRoot != null)
                {
                    proToolsRoot.gameObject.SetActive(option);

                }
            }

            public void SwitchToProdTools()
            {
                ToggleProdTools(true);
                ToggleMediaPlayer(false);
            }

            public void SwitchToMediaPlayer()
            {
                ToggleProdTools(false);
                ToggleMediaPlayer(true);
            }

            public void CreatePlaylist()
            {
                if(playlistController != null)
                {
                    if(playlistPanel.trackPrefab != null & playlistPanel.trackRoot != null)
                    {
                        var curated_list = playlistController.GetCuratedPlaylist();
                        if(!playlistPanel.isInitialised)
                        {
                            for(int i = 0;i<curated_list.Count;i++)
                            {
                                var curatedTrack = curated_list[i];
                                if (curatedTrack != null)
                                {
                                    GameObject btnObj = Instantiate(playlistPanel.trackPrefab, playlistPanel.trackRoot);
                                    var btn = btnObj.GetComponent<Button>();
                                    if (!playlistPanel.playlist.Contains(btn))
                                    {
                                        btn.onClick.AddListener(delegate
                                        {
                                            if(!curatedTrack.isLocked)
                                            {
                                                playlistController.SetTape(curatedTrack);
                                            }
                                        });

                                        btn.GetComponentInChildren<TextMeshProUGUI>().text = curatedTrack.clip.name + " - " + curatedTrack.artistName;
                                        playlistPanel.playlist.Add(btn);
                                    }
                                }
                            }

                            playlistPanel.isInitialised = true;
                        }
                    }
                }
            }

            public void InitButtons()
            {
                //set play btn
                if (playPauseBtn.playPauseButton != null)
                {
                    var btn = playPauseBtn.playPauseButton;

                    btn.onClick.RemoveAllListeners();
                    btn.onClick.AddListener(delegate
                    {
                        if(playlistController.audioSource.isPlaying)
                        {
                            playlistController.PauseTape();
                            btn.GetComponent<Image>().sprite = playPauseBtn.playUI;
                        }
                        else
                        {
                            playlistController.PlayTape();
                            btn.GetComponent<Image>().sprite = playPauseBtn.pauseUI;
                        }
                        
                    });
                }

                

                //set next btn
                if (nextButton != null)
                {
                    nextButton.onClick.RemoveAllListeners();
                    nextButton.onClick.AddListener(delegate
                    {
                        playlistController.NextTape();
                    });
                }

                //set previous btn
                if (previousButton != null)
                {
                    previousButton.onClick.RemoveAllListeners();
                    previousButton.onClick.AddListener(delegate
                    {
                        playlistController.PreviousTape();
                    });
                }

                //set volume slider
                if(volumeScrollbar != null)
                {
                    volumeScrollbar.onValueChanged.RemoveAllListeners();
                    volumeScrollbar.onValueChanged.AddListener(delegate
                    {
                        if(playlistController != null)
                        {
                            if(playlistController.audioSource != null)
                            {
                                var audio = playlistController.audioSource;

                                audio.volume = volumeScrollbar.value;
                            }
                        }
                    });
                }

                //set lyrics button
                if(lyricsPanel.lyricsButton != null)
                {
                    var btn = lyricsPanel.lyricsButton;
                    btn.onClick.RemoveAllListeners();
                    btn.onClick.AddListener(delegate
                    {
                        lyricsPanel.Toggle(!lyricsPanel.panel.activeSelf);
                    });
                }

            }

        }
        public RadioUI radioRef;

        [System.Serializable]
        public class HUDReference
        {
            public Canvas canvas;
            public Button productButton;
            public Button videoButton;
            public Button deckButton;
            public Button chatButton;
            public TextMeshProUGUI scoreText;

            [System.Serializable]
            public class Notification
            {
                public enum NotificationType
                {
                    INFO,
                    WARNING,
                    SUCCESS,
                    DAMGER
                }
                public NotificationType type;
                public GameObject panel;
                public TextMeshProUGUI text;
                public float duration;

                
            }
            public Notification notification;

            public bool isPlaying = false;

            public void Init(bool option)
            {
                if (canvas != null)
                {
                    canvas.enabled = option;
                    canvas.gameObject.SetActive(option);
                }
            }
        }
        public HUDReference hudRef;

        [System.Serializable]
        public class ReceptionReference
        {
            public Canvas canvas;

            public Button emailButton;
            public Button phoneButton;
            public Button urlButton;
            public Button exitButton;
            public RawImage logoRawImage;
            public Text nameText;
            public Text aboutText;
            public Button collapseButton;
            public InputField inputField;
            //public ChatGPT chat;
            public Kiosk_StatePattern currentKiosk;
            public bool isInitialised = false;

            public void Init()
            {
                if (currentKiosk != null)
                {
                    var details = currentKiosk.kioskMaster.details;
                    //set name
                    if (nameText != null)
                    {
                        nameText.text = details.name;
                    }

                    //set about
                    if (aboutText != null)
                    {
                        aboutText.text = details.about;
                    }

                    //setLogo
                    if (logoRawImage != null)
                    {
                        logoRawImage.texture = details.logo;
                    }

                    //email
                    if (emailButton != null)
                    {
                        //onclick events
                        emailButton.onClick.RemoveAllListeners();
                        emailButton.onClick.AddListener(delegate
                        {
                            SendMail();
                        });

                        //set email text
                        if(emailButton.GetComponentInChildren<Text>() != null)
                        {
                            emailButton.GetComponentInChildren<Text>().text = details.email;
                        }
                    }

                    //website
                    if (urlButton != null)
                    {
                        urlButton.onClick.RemoveAllListeners();
                        urlButton.onClick.AddListener(delegate
                        {
                            OpenUrl();
                        });

                        //set url text
                        if (urlButton.GetComponentInChildren<Text>() != null)
                        {
                            urlButton.GetComponentInChildren<Text>().text = details.website;
                        }
                    }

                    //phone
                    if (phoneButton != null)
                    {
                        phoneButton.onClick.RemoveAllListeners();
                        phoneButton.onClick.AddListener(delegate
                        {
                            Call();
                        });

                        //set tel text
                        if (phoneButton.GetComponentInChildren<Text>() != null)
                        {
                            phoneButton.GetComponentInChildren<Text>().text = details.phonenumber;
                        }
                    }

                    if(inputField != null)
                    {
                        inputField.Select();
                        inputField.ActivateInputField();
                    }

                    isInitialised = true;
                }
                else
                {
                    Debug.LogError("Kiosk missing");
                }
            }

            public bool CanInteract()
            {
                if(currentKiosk != null)
                {
                    if(currentKiosk.adManager != null)
                    {
                        if(currentKiosk.adManager.ad.receptionController != null)
                        {
                            return currentKiosk.adManager.ad.receptionController.customerInAreaOfInfluence;
                        }
                    }
                }
                return false;
            }

            public void Toggle(bool option)
            {
                if (canvas != null)
                {
                    if (canvas.enabled == option) return;
                    canvas.gameObject.SetActive(option);
                    canvas.enabled = option;

                    if(option == true)
                    {
                        
                    }
                }
            }

            public void SendMail()
            {
                //sanitize
                string url = UnityWebRequest.EscapeURL(currentKiosk.kioskMaster.details.email);
                //open
                Application.OpenURL("mailto://" + url);
            }

            public void OpenUrl()
            {
                //sanitize
                string url = UnityWebRequest.EscapeURL(currentKiosk.kioskMaster.details.website);
                //open
                Application.OpenURL(url);
            }

            public void Call()
            {
                //sanitize
                string url = UnityWebRequest.EscapeURL(currentKiosk.kioskMaster.details.phonenumber);
                //open
                Application.OpenURL("tel://" + url);
            }
        }
        public ReceptionReference receptionRef;

        [System.Serializable]
        public class SlideDeckReference
        {
            public Canvas canvas;
            public GameObject controlPanel;
            public RawImage fullScreenRawImage;
            public TextMeshProUGUI titleText;
            public Button fullScreenButton;
            public PlayPauseBtn playPauseBtn;
            public Button playButton;
            public Button pauseButton;
            public Button nextButton;
            public Button previousButton;

            public bool isInitialised = false;
            public bool isFullScreen = false;

            public Kiosk_StatePattern currentKiosk;

            public void Init()
            {
                if (currentKiosk == null) return;

                if (fullScreenRawImage != null)
                {
                    if(currentKiosk.adManager.GetFocusDeck() != null)
                    {
                        if(currentKiosk.adManager.GetFocusDeck().screenRawImage != null)
                        {
                            fullScreenRawImage.texture = currentKiosk.adManager.GetFocusDeck().screenRawImage.texture;
                        }
                    }

                }

                InitButtons();


            }

            public void SetTitle(string title)
            {
                if (titleText != null)
                {
                    titleText.text = title;
                }
            }

            public void InitButtons()
            {
                var deck_control = currentKiosk.adManager.GetFocusDeck();


                if (playPauseBtn.playPauseButton != null)
                {
                    playPauseBtn.playPauseButton.onClick.RemoveAllListeners();
                    playPauseBtn.playPauseButton.onClick.AddListener(delegate
                    {
                        if (deck_control != null)
                        {


                            if (deck_control.isPlaying)
                            {
                                deck_control.PauseSlideshow();
                                
                                playPauseBtn.SetPlaySprite();
                            }
                            else
                            {
                                deck_control.PlaySlideshow();
                                playPauseBtn.SetPauseSprite();
                            }

                            SetTitle(deck_control.currentSlide.name);
                        }

                    });
                }


                //set next btn
                if (nextButton != null)
                {
                    nextButton.onClick.RemoveAllListeners();
                    nextButton.onClick.AddListener(delegate
                    {
                        deck_control.NextSlide();
                    });
                }

                //set previous btn
                if (previousButton != null)
                {
                    previousButton.onClick.RemoveAllListeners();
                    previousButton.onClick.AddListener(delegate
                    {
                        deck_control.PreviousSlide();
                    });
                }



                //set fullscreen btn
                if (fullScreenButton != null)
                {
                    fullScreenButton.onClick.RemoveAllListeners();
                    fullScreenButton.onClick.AddListener(delegate
                    {
                        deck_control.ToggleFullscreen();
                    });
                }

            }

            public void Toggle(bool option)
            {
                if (canvas != null)
                {
                    canvas.gameObject.SetActive(option);
                    canvas.enabled = option;
                    if(controlPanel!=null)controlPanel.SetActive(option);

                }
            }

            public bool CanInteract()
            {
                if (currentKiosk != null)
                {
                    if (currentKiosk.adManager != null)
                    {
                        if (currentKiosk.adManager.GetFocusDeck() != null)
                        {
                            return true;
                        }
                    }
                }
                return false;
            }
        }
        public SlideDeckReference deckRef;


        [System.Serializable]
        public class ProductReference
        {
            public Canvas canvas;
            public TextMeshProUGUI nameText;
            public UI_TransformButton rotateUpBtn;
            public UI_TransformButton rotateDownBtn;
            public UI_TransformButton rotateRightBtn;
            public UI_TransformButton rotateLeftBtn;
            public Slider scaleSlider;
            public Button resetBtn;

            public Kiosk_StatePattern currentKiosk;


            public void Init()
            {
                if (currentKiosk == null) return;
                

                if(scaleSlider != null)
                {
                    scaleSlider.onValueChanged.AddListener(delegate
                    {
                        var ad = currentKiosk.adManager;
                        var prod = ad.GetFocusProduct();
                        if (prod != null)
                        {
                            prod.SetScale(scaleSlider.value);
                        }
                    });
                }

                
            }

            public void Toggle(bool option)
            {
                if (canvas != null)
                {
                    canvas.enabled = option;
                    canvas.gameObject.SetActive(option);
                }
            }

            public bool RotationUpInput()
            {
                if(rotateUpBtn != null)
                {
                    return rotateUpBtn.buttonPressed;
                }
                return false;
            }

            public bool RotationDownInput()
            {
                if (rotateDownBtn != null)
                {
                    return rotateDownBtn.buttonPressed;
                }
                return false;
            }

            public bool RotationRightInput()
            {
                if (rotateRightBtn != null)
                {
                    return rotateRightBtn.buttonPressed;
                }
                return false;
            }

            public bool RotationLeftInput()
            {
                if (rotateLeftBtn != null)
                {
                    return rotateLeftBtn.buttonPressed;
                }
                return false;
            }


            public float ScalingInput()
            {
                if (scaleSlider != null)
                {
                    return scaleSlider.value;
                }
                return 0f;
            }

        }
        public ProductReference productRef;



        public InworldPlayer player;

        [Header("State AI")]
        public IUI_StateInterface currentState;
        public IUI_StateInterface capturedState=null;
        public UI_KioskChatState chatState;
        public UI_KioskReceptionState receptionState;
        public UI_KioskSlideDeckState deckState;
        public UI_MovieState movieState;
        public UI_RadioState radioState;
        public UI_LoadingState loadingState;
        public UI_MainMenuState mainMenuState;
        public UI_ProductState productState;
        public UI_PauseMenuState pauseState;
        public UI_PlaytimeState playtimeState;
        public UI_TutorialState tutorialState;

        private void Awake()
        {
            SetupStateReferences();
            ActivatePlaytime();
        }


        // Start is called before the first frame update
        void Start()
        {
            SetInitialReferences();
            ResetUI();
        }

        // Update is called once per frame
        void Update()
        {
            SetUpdateReferences();

        }

        private void OnEnable()
        {

        }

        private void OnDisable()
        {
            StopAllCoroutines();
        }

        void SetupStateReferences()
        {
            chatState = new UI_KioskChatState(this);
            receptionState = new UI_KioskReceptionState(this);
            deckState = new UI_KioskSlideDeckState(this);
            radioState = new UI_RadioState(this);
            movieState = new UI_MovieState(this);
            loadingState = new UI_LoadingState(this);
            mainMenuState = new UI_MainMenuState(this);
            productState = new UI_ProductState(this);
            pauseState = new UI_PauseMenuState(this);
            playtimeState = new UI_PlaytimeState(this);
            tutorialState = new UI_TutorialState(this);
        }

        void SetInitialReferences()
        {
            //Application.targetFrameRate = 60;
            
            //inputRef.mobipad.Toggle(false);
            //load HUD
            

            InitPauseButtons();
            InitHUDButtons();

        }

        void SetUpdateReferences()
        {
            
            if (Time.time > nextCheck)
            {
                nextCheck = Time.time + checkRate;

                currentState.UpdateState();
                MarketConnection();
                //Debug.Log("currbent ui state:-" + currentState);
                
                if(kiosk != null)
                {
                    if (currentState == productState)
                    {
                        kiosk.SetDemoPosition();
                    }
                    else
                    {
                        kiosk.SetDefaultPosition();
                    }
                }
                
            }

        }

        public void SetState(IUI_StateInterface stateInterface)
        {
            if (currentState != stateInterface)
            {
                this.currentState = stateInterface;
            }
        }

        void SaveState()
        {
            if(capturedState != currentState)
            {
                capturedState = currentState;
            }
            
        }

        public void ToggleCustomerInput(bool option)
        {
           // EventHandler.ExecuteEvent(customer.gameObject, "OnEnableGameplayInput", option);//event handler grom Opsive.Shared
        }

        public void MarketConnection()
        {
            
            productRef.Init();
            receptionRef.Init();
            movieRef.Init();
            deckRef.Init();
        }

        public void InitHUDButtons()
        {
            if(hudRef.canvas != null)
            {
                if(hudRef.chatButton != null)
                {
                    hudRef.chatButton.onClick.RemoveAllListeners();
                    hudRef.chatButton.onClick.AddListener(delegate
                    {
                        StopAllCoroutines();
                        if(player != null)
                        {
                            player.InitialiseUIRequest();
                        }
                        ActivateReceptionState();
                    });
                }

                if (hudRef.videoButton != null)
                {
                    hudRef.videoButton.onClick.RemoveAllListeners();
                    hudRef.videoButton.onClick.AddListener(delegate
                    {
                        StopAllCoroutines();
                        ActivateMovieState();
                    });
                }

                if (hudRef.deckButton != null)
                {
                    hudRef.deckButton.onClick.RemoveAllListeners();
                    hudRef.deckButton.onClick.AddListener(delegate
                    {
                        StopAllCoroutines();
                        ActivateDeckState();
                    });
                }

                if (hudRef.productButton != null)
                {
                    hudRef.productButton.onClick.RemoveAllListeners();
                    hudRef.productButton.onClick.AddListener(delegate
                    {
                        StopAllCoroutines();
                        ActivateProductState();
                    });
                }
            }
        }

        public void InitMainMenuButtons()
        {
            if (mainMenuRef.startButton != null)
            {
                mainMenuRef.startButton.onClick.RemoveAllListeners();
                mainMenuRef.startButton.onClick.AddListener(delegate
                {
                    StopAllCoroutines();
                    ActivateLoadingState();
                });
            }

            if(mainMenuRef.settingsButton != null)
            {
                mainMenuRef.settingsButton.onClick.RemoveAllListeners();
                mainMenuRef.settingsButton.onClick.AddListener(delegate
                {
                    //TODO:: language switch and shit
                });
            }
            

            if (mainMenuRef.aboutButton != null)
            {
                mainMenuRef.aboutButton.onClick.RemoveAllListeners();
                mainMenuRef.aboutButton.onClick.AddListener(delegate
                {
                    //TODO:: credits and shit
                });
            }

            if (mainMenuRef.exitButton != null)
            {
                mainMenuRef.exitButton.onClick.RemoveAllListeners();
                mainMenuRef.exitButton.onClick.AddListener(delegate
                {
                    Application.Quit();
                });
            }
        }

        public void InitPauseButtons()
        {
            if(pauseRef.continueButton != null)
            {
                pauseRef.continueButton.onClick.AddListener(delegate
                {
                    ActivatePlaytime();
                });
            }

            if (pauseRef.settingsButton != null)
            {
                pauseRef.settingsButton.onClick.AddListener(delegate
                {
                    //ActivateTutorialState();
                });
            }

            if (pauseRef.aboutButton != null)
            {
                pauseRef.aboutButton.onClick.AddListener(delegate
                {
                    ActivateTutorialState();
                });
            }

            if (pauseRef.quitButton != null)
            {
                pauseRef.quitButton.onClick.AddListener(delegate
                {
                    Application.Quit();
                });
            }
        }

        public void LoadGame()
        {

            if (loadingRef.isLoading == false && loadingRef.duration > 0)
            {
                StartCoroutine(LoadingTime());
            }
            else if (loadingRef.isLoading == false && loadingRef.duration <= 0)
            {
                ActivatePlaytime();
            }

        }

        public IEnumerator LoadingTime()
        {
            loadingRef.isLoading = true;
            yield return new WaitForSeconds(1);
            loadingRef.duration -= 1;
            loadingRef.slider.value += 1;
            loadingRef.isLoading = false;
        }

        public IEnumerator ToggleNotification(string message, NotificationType type)
        {
            var panel = hudRef.notification.panel;
            var text = hudRef.notification.text;
            if (panel != null)
            {
                if (text != null)
                {
                    text.text = message;

                    switch (type)
                    {
                        case NotificationType.INFO:
                            break;
                        case NotificationType.WARNING:
                            break;
                        case NotificationType.SUCCESS:
                            break;
                        case NotificationType.DAMGER:
                            break;
                        default:
                            break;
                    }
                }
                panel.SetActive(true);
                yield return new WaitForSeconds(1);
                panel.SetActive(false);
            }
        }

        public void ActivateLoadingState()
        {
            StopAllCoroutines();

            if (currentState != loadingState)
            {
                SaveState();
            }

            SetState(loadingState);
            loadingRef.slider.maxValue = loadingRef.duration;
            loadingRef.slider.value = 0;
        }

        public void ActivateMainMenu()
        {
            StopAllCoroutines();

            SaveState();

            SetState(mainMenuState);
        }

        public void ActivatePlaytime()
        {
            StopAllCoroutines();

            SaveState();

            SetState(playtimeState);
        }

        public void ActivatePauseState()
        {
            StopAllCoroutines();

            SaveState();

            SetState(pauseState);
        }

        public void ActivateDeckState()
        {
            StopAllCoroutines();

            SaveState();

            SetState(deckState);
        }

        public void ActivateReceptionState()
        {
            StopAllCoroutines();

            SaveState();

            SetState(receptionState);
        }

        public void ActivateChatState()
        {
            StopAllCoroutines();

            SaveState();

            SetState(chatState);
        }

        public void ActivateProductState()
        {
            StopAllCoroutines();

            SaveState();

            SetState(productState);
        }

        public void ActivateTutorialState()
        {
            StopAllCoroutines();

            SaveState();

            SetState(tutorialState);
        }

        public void ActivateMovieState()
        {
            StopAllCoroutines();

            SaveState();

            SetState(movieState);
        }

        public void ActivateRadioState()
        {
            StopAllCoroutines();

            SaveState();

            SetState(radioState);
        }

        public void ResetUI()
        {
            if(currentState != mainMenuState)
            {
                mainMenuRef.Init(false);
            }
            
            if(currentState != loadingState)
            {
                loadingRef.Init(false);
            }
            
            
            if(currentState != productState)
            {
                productRef.Toggle(false);
            }

            if(currentState != movieState)
            {
                movieRef.Toggle(false);
            }
            
            if(currentState != radioState)
            {
                radioRef.Toggle(false);
            }

            if (currentState != pauseState)
            {
                pauseRef.Init(false);
            }

            if(currentState != playtimeState)
            {
                hudRef.Init(false);
            }
            
            //helpRef.Init(false);
            if(currentState != receptionState)
            {
                receptionRef.Toggle(false);
            }
            
            if(currentState != deckState)
            {
                deckRef.Toggle(false);
            }
            
        }

        public void OpenURL(string url)
        {
            Application.OpenURL(url);
        }
    }
}

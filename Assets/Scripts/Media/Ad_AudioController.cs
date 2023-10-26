using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static Chiro.Customer_PlaylistController;

namespace Chiro
{
    public class Ad_AudioController : MonoBehaviour
    {
        
        public List<Track> tracks;

        public Track currentTape;

        public AudioSource audioSource;
        public UI_Billboard billboard;

        public UI_StatePattern ui;
        public bool playOnEntry = false;
        public bool playOnToggle = false;

        
        public bool customerInAreaOfInfluence = false;
        
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

        private void OnTriggerEnter(Collider other)
        {
            if(other.tag == "Player")
            {
                customerInAreaOfInfluence = true;

                if(billboard != null)
                {
                    billboard.icon.Toggle(true);
                }
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.tag == "Player")
            {
                customerInAreaOfInfluence = false;

                if (billboard != null)
                {
                    billboard.icon.Toggle(false);
                }
            }
        }

        void SetInitialReferences()
        {
            if(audioSource == null && GetComponent<AudioSource>() != null)
            {
                audioSource = GetComponent<AudioSource>();
            }
        }

        void SetUpdateReferences()
        {
            if( customerInAreaOfInfluence)
            {
                if(CheckInput())
                {
                    ui.radioRef.SwitchToProdTools();
                    ui.ActivateRadioState();
                }
                
            }
            else
            {
                ui.radioRef.SwitchToMediaPlayer();
            }
            //StateMachine();
        }

        public bool CheckInput()
        {
            if(ui != null)
            {
                if(ui.inputRef != null)
                {
                    return ui.inputRef.ToggleProtools();
                    
                }
            }
            return false;
        }

        public void SetTape(Track tape)
        {
            if(audioSource != null)
            {
                if(tape.clip != null)
                {
                    audioSource.clip = tape.clip;
                    audioSource.volume = tape.volume;
                    audioSource.loop = tape.shouldLoop;

                    //reset list
                    ResetTapes();
                    tape.isSet = true;

                    currentTape = tape;

                    if(playOnEntry)
                    {
                        PlayTape();
                    }
                }
            }
        }

        public void ResetTapes()
        {
            if(audioSource != null)
            {
                audioSource.Stop();
            }
            currentTape = null;
            foreach (Track tp in tracks.Where(t => t.isSet))
            {
                if (tp != null)
                {
                    tp.isSet = false;
                }
            }
        }

        public bool CanPlay()
        {
            if(audioSource != null && currentTape != null)
            {
                if(tracks.Count > 0)return true;
            }
            return false;
        }

        

        

        public void PlayTape()
        {
            if(currentTape == null || audioSource == null)return;
            audioSource.clip = currentTape.clip;
            if(!audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }

        public void PauseTape()
        {
            if (currentTape == null || audioSource == null) return;
            if(audioSource.isPlaying)
            {
                audioSource.Pause();
            }
        }

        public void NextTape()
        {
            if (audioSource != null)
            {
                if (tracks.Count > 0)
                {
                    int current_index = tracks.IndexOf(currentTape);

                    if (current_index != tracks.Count - 1)
                    {
                        SetTape(tracks[current_index + 1]);
                        PlayTape();
                    }
                    else
                    {
                        SetTape(tracks[0]);
                        PlayTape();
                    }
                }

            }
        }

        public void PreviousTape()
        {
            if (audioSource != null)
            {
                if (tracks.Count > 0)
                {
                    int current_index = tracks.IndexOf(currentTape);

                    if (current_index != 0)
                    {
                        SetTape(tracks[current_index - 1]);
                        PlayTape();
                    }
                    else
                    {
                        SetTape(tracks[tracks.Count - 1]);
                        PlayTape();
                    }
                }

            }
        }

        public void MuteTape()
        {
            if(audioSource != null)
            {
                audioSource.mute = true;
            }
        }
    }
}

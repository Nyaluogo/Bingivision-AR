using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Video;

namespace Chiro
{
    public class Ad_VideoController : MonoBehaviour
    {
        public enum VideoType
        {
            PITCH,
            TEAM,
            WELCOME,
        }

        [System.Serializable]
        public class Film
        {
            public string name;
            public string description;
            public VideoType type;
            public VideoClip clip;
            public int playCount;
            public float volume=0.5f;
            public bool shouldLoop = false;
            public bool isInitialised = false;

        }

        
        public Film currentFilm=null;
        public List<Film> filmList;
        public VideoPlayer mediaPlayer;
        public AudioSource audioSource;
        public UI_Billboard billboard;

        
        public bool playOnEntry = false;
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

        

        void SetInitialReferences()
        {
            if (filmList.Count > 0)
            {
                if (filmList[0] != null)
                {
                    SetFilm(filmList[0]);
                }
            }
        }

        void SetUpdateReferences()
        {
            if(Time.timeScale > 0)
            {
                /* how the fuck will I resume this fucker */
                //PlayFilm();
            }
            else
            {
                if(mediaPlayer.isPlaying)
                {
                    PauseFilm();
                }
                
            }
        }

        public void SetFilm(Film film)
        {
            if(mediaPlayer != null)
            {
                if(film.clip != null)
                {
                    mediaPlayer.clip = film.clip;
                    mediaPlayer.SetDirectAudioVolume(0,film.volume);
                    mediaPlayer.isLooping = film.shouldLoop;

                    //reset 
                    ResetFilms();
                    film.isInitialised= true;

                    currentFilm = film;

                    if(playOnEntry)
                    {
                        PlayFilm();
                    }
                }
            }
        }

        public void ResetFilms()
        {
            if(mediaPlayer != null)
            {
                mediaPlayer.Stop();
            }
            currentFilm = null;
            foreach (Film flm in filmList.Where(f => f.isInitialised == true))
            {
                if (flm != null)
                {
                    flm.isInitialised = false;
                }
            }
        }

        public void PlayFilm()
        {
            if (mediaPlayer != null)
            {
                if (currentFilm == null || mediaPlayer.clip == null) return;
                if (!mediaPlayer.isPlaying) mediaPlayer.Play();
            }
        }

        public void PauseFilm()
        {
            if(mediaPlayer != null)
            {
                if (currentFilm == null || mediaPlayer.clip == null) return;
                if (mediaPlayer.isPlaying)mediaPlayer.Pause();
            }
        }

        public void NextFilm()
        {
            
            if (mediaPlayer != null)
            {
                if (filmList.Count > 0)
                {
                    int current_index = filmList.IndexOf(currentFilm);

                    if(current_index != filmList.Count-1)
                    {
                        SetFilm(filmList[current_index + 1]);
                        PlayFilm();
                    }
                    else
                    {
                        SetFilm(filmList[0]);
                        PlayFilm();
                    }
                }

            }
        }

        public void PreviousFilm()
        {

            if (mediaPlayer != null)
            {
                if (filmList.Count > 0)
                {
                    int current_index = filmList.IndexOf(currentFilm);

                    if (current_index != 0)
                    {
                        SetFilm(filmList[current_index - 1]);
                        PlayFilm();
                    }
                    else
                    {
                        SetFilm(filmList[filmList.Count-1]);
                        PlayFilm();
                    }
                }

            }
        }

        public void MuteFilm()
        {
            if(mediaPlayer != null)
            {
                mediaPlayer.SetDirectAudioMute(mediaPlayer.audioTrackCount,!mediaPlayer.GetDirectAudioMute(mediaPlayer.audioTrackCount));
            }
        }

        public void SetVolume(float volume)
        {
            if (audioSource != null)
            {
                audioSource.volume = volume;
            }
        }

        public void ToggleFullscreen()
        {

        }

        public void StateMachine()
        {
            if(currentFilm == null)return;

            

            switch (currentFilm.type)
            {
                case VideoType.PITCH:
                    //PlayFilm();
                    break;
                case VideoType.TEAM:
                    //PlayFilm();
                    break;
                case VideoType.WELCOME:
                    //PlayFilm();
                    break;
                default:
                    break;
            }
        }
    }
}


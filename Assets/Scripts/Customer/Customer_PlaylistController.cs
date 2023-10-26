using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static Chiro.Ad_AudioController;
using static Chiro.TrackPickup;
using static Chiro.UI_StatePattern.HUDReference.Notification;

namespace Chiro
{
    public class Customer_PlaylistController : MonoBehaviour
    {
        [System.Serializable]
        public class Track
        {
            public string songName;
            public string artistName = "Msaani";
            public string lyrics = "Lyrics onge";
            public AudioClip clip;
            public int playCount;
            public float volume;
            public float pitch;
            public bool shouldLoop;
            public bool isSet = false;
            public bool isLoadedinMap = false;
            public bool isLocked = true;


        }

        [System.Serializable]
        public class Playlist//the album
        {
            public string name;
            public List<Track> tracks;
            public bool isComplete = false;
            public bool isInitialised = false;
        }
        public Playlist currentPlaylist;


        public Track currentTrack=null;

        public AudioSource audioSource;
        public List<Playlist> rawPlaylists;
        public List<TrackPickup> goalTracks;
        public int totalTracksAvailable;
        public int totalTracksCollected;

        public UI_StatePattern ui;

        

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
            if (audioSource == null && GetComponent<AudioSource>() != null)
            {
                audioSource = GetComponent<AudioSource>();
            }

            if(currentPlaylist == null)
            {
                SetPlaylist(rawPlaylists[0]);
            }
        }

        void SetUpdateReferences()
        {

        }

        public void DistributeMusicPlaylist()
        {
            if (rawPlaylists.Count == 0)
            {
                Debug.Log("Raw Playlist empty!");
                return;
            }
            else if (GetTrackPickups().Count == 0)
            {
                Debug.Log("Pickup Playlist empty!");
                return;
            }
            else
            {
                //TODO::Hii code ni shit as Fuck. Work on it.
                var tracks = GetPlaylist().tracks;
                
                if(tracks.Count > 0)
                {
                    System.Random rng = new System.Random();
                    var shuffled_list = tracks.OrderBy(a => rng.Next()).ToList();

                    

                    for (int i = 0; i < goalTracks.Count; i++)
                    {

                        var currentGoal = goalTracks[i];
                        var selected_track = shuffled_list[i];
                        //var curated_list = shuffled_list.Where(t => t.isLoadedinMap==false).ToList();
                        Debug.Log(shuffled_list.Count + " total tracks for" + currentGoal.pickupTrack.track.clip.ToString());
                        if (shuffled_list.Count > 0)
                        {
                            
                            if (selected_track != null)
                            {
                                currentGoal.pickupTrack.track = selected_track;
                                currentGoal.pickupTrack.track.clip = selected_track.clip;
                                selected_track.isLoadedinMap = true;
                            }
                            else
                            {
                                Debug.LogError("Track missing");
                            }
                        }
                        else
                        {
                            Debug.LogError("Playlist empty");
                        }



                    }

                }
                

            }
        }


        public void OnPickpTrack(Pickup pickupObj)
        {
            
            AddToPlaylist(pickupObj.track);
            pickupObj.isCollected = true;
        }

        public List<TrackPickup> GetTrackPickups()
        {
            if(GameObject.FindGameObjectsWithTag("Coin").Length > 0)
            {
                foreach(GameObject pickup in GameObject.FindGameObjectsWithTag("Coin"))
                {
                    if(pickup.GetComponent<TrackPickup>() != null)
                    {
                        if(!goalTracks.Contains(pickup.GetComponent<TrackPickup>()))
                        {
                            goalTracks.Add(pickup.GetComponent<TrackPickup>());
                        }
                    }
                }
            }
            return goalTracks;
        }

        public void SetPlaylist(Playlist p)
        {
            if(rawPlaylists.Count > 0)
            {
                foreach (var playlist in rawPlaylists)
                {
                    if(playlist != null)
                    {
                        if(playlist == p)
                        {
                            currentPlaylist = playlist;
                            currentPlaylist.isInitialised = true;
                        }
                        else
                        {
                            playlist.isInitialised = false;
                        }
                    }
                }
            }
        }

        public Playlist GetPlaylist()
        {
            if(rawPlaylists.Count == 0) return null;

            var play = rawPlaylists.Where(t => t.isInitialised).FirstOrDefault();

            return play;

        }

        public List<Track> GetCuratedPlaylist()
        {
            if (GetPlaylist().tracks.Count == 0) return null;

            var play = GetPlaylist().tracks.Where(t=>t.isLoadedinMap).ToList();

            return play;
        }

        public List<Track> GetUnlockedPlaylist()
        {
            if (GetPlaylist().tracks.Count == 0) return null;

            var play = GetCuratedPlaylist().Where(t => !t.isLocked).ToList();

            return play;
        }

        public void AddToPlaylist(Track track)
        {
            if (GetPlaylist().tracks.Contains(track))
            {
                track.isLocked = false;
                SetTape(track);

                if (ui != null)
                {
                    if(PlaylistComplete())
                    {
                        string message = track.clip.name + " added to playlist. FIND THE ALBUM!";
                        StartCoroutine(ui.ToggleNotification(message, NotificationType.INFO));
                    }
                    else
                    {
                        string message = track.clip.name + " added to playlist.";
                        StartCoroutine(ui.ToggleNotification(message, NotificationType.INFO));
                    }
                    
                }
                
            }
        }

        public string GetScoreText()
        {
            string scoreTxt = "/";

            var collected = goalTracks.Where(t=>t.pickupTrack.isCollected);
            var total = goalTracks.Count();

            scoreTxt = collected.Count()+" / "+total.ToString()+" ";

            return scoreTxt;
        }

        public bool PlaylistComplete()
        {
            var collected = goalTracks.Where(trk => trk.pickupTrack.isCollected).ToList();
            var total = goalTracks.Count();
            if(total <= 0)return false;
            if(collected.Count() >= total)return true;//uncomment
            //if(collected.Count() >= 5)return true;//test
            return collected.Count() == total;

        }

        public void SetTape(Track tape)
        {
            if (audioSource != null)
            {
                if (tape.clip != null)
                {
                    audioSource.clip = tape.clip;
                    //audioSource.volume = tape.volume;
                    audioSource.loop = tape.shouldLoop;

                    //reset list
                    ResetTapes();
                    tape.isSet = true;

                    currentTrack = tape;

                    PlayTape();
                }
            }
        }

        public void ResetTapes()
        {
            if (audioSource != null)
            {
                audioSource.Stop();
            }
            currentTrack = null;
            foreach (Track tp in GetPlaylist().tracks.Where(t => t.isSet))
            {
                if (tp != null)
                {
                    tp.isSet = false;
                }
            }
        }

        public bool CanPlay()
        {
            if (audioSource != null && currentTrack != null)
            {
                if (GetCuratedPlaylist().Count > 0) return true;
            }
            return false;
        }



        public void StateMachine()
        {
            if (currentTrack == null) return;
            if (CanPlay())
            {
                PlayTape();
            }
        }

        public void PlayTape()
        {
            if (currentTrack == null || audioSource == null) return;
            audioSource.clip = currentTrack.clip;
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }

        public void PauseTape()
        {
            if (currentTrack == null || audioSource == null) return;
            if (audioSource.isPlaying)
            {
                audioSource.Pause();
            }
        }

        public void NextTape()
        {
            if (audioSource != null)
            {
                var tracks = GetUnlockedPlaylist();
                if (tracks.Count > 0)
                {
                    
                    int current_index = tracks.IndexOf(currentTrack);

                    //check shuffle


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
                var tracks = GetUnlockedPlaylist();
                if (tracks.Count > 0)
                { 
                    int current_index = tracks.IndexOf(currentTrack);

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
            if (audioSource != null)
            {
                audioSource.mute = true;
            }
        }

        public void LoadBandoPlaylist()
        {
            if(goalTracks.Count > 0)
            {
                foreach (var goal in goalTracks)
                {
                    if(goal != null)
                    {
                        OnPickpTrack(goal.pickupTrack);
                    }
                }
            }
        }

        public void PlayTrack(int trackNum)
        {
            if(GetPlaylist().tracks[trackNum] != null)
            {
                SetTape(GetPlaylist().tracks[trackNum]);
            }
            
        }

        public void ResetTracks()
        {
            if (GetPlaylist() != null)
            {
                foreach (var track in GetPlaylist().tracks)
                {
                    track.isLocked = true;
                }
            }

            if (goalTracks.Count > 0)
            {
                foreach (var pickup in goalTracks)
                {
                    if (pickup != null)
                    {
                        pickup.pickupTrack.isCollected = false;
                        pickup.gameObject.SetActive(true);
                    }
                }
            }


        }
    }
}

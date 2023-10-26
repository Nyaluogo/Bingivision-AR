using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

namespace Chiro
{
    [System.Serializable]
    public class Volume
    {
        public string name;
        public float volume=1f;
        public float tempVolume=1f;
    }

    public class SoundSettings
    {
        public static UI_SoundProfile profile;

    }

    [CreateAssetMenu(menuName ="Nafas/Create Audio Profile")]
    public class UI_SoundProfile : ScriptableObject
    {
        public bool saveInPlayerPrefs = true;
        public string prefPrefix = "AudioSettings_";
        
        public AudioMixer audioMixer;
        public Volume[] volumeControl;

        public void SetProfile(UI_SoundProfile profile)
        {
            SoundSettings.profile = profile;
        }

        public float GetAudioLevels(string name)
        {
            float volume = 1f;

            if(audioMixer == null)
            {
                Debug.LogError("There is no Audiomixer defined in the profiles file");
                return volume;
            }

            for(int i=0;i<volumeControl.Length; i++)
            {
                if (volumeControl[i].name != name)
                {
                    continue;
                }
                else
                {
                    if(saveInPlayerPrefs)
                    {
                        if(PlayerPrefs.HasKey(prefPrefix + volumeControl[i].name)) 
                        {
                            volumeControl[i].volume = PlayerPrefs.GetFloat(prefPrefix + volumeControl[i].name);
                        }
                    }

                    volumeControl[i].tempVolume = volumeControl[i].volume;

                    if (audioMixer)
                    {
                        audioMixer.SetFloat(volumeControl[i].name, Mathf.Log(volumeControl[i].volume) * 20f);
                    }

                    volume = volumeControl[i].volume;
                    break;
                }
            }

            return volume;
        }

        public void GetAudioLevels()
        {
            if (audioMixer == null)
            {
                Debug.LogError("There is no Audiomixer defined in the profiles file");
                return;
            }

            for (int i = 0; i < volumeControl.Length; i++)
            {
                Debug.Log(volumeControl[i].name);
                if (saveInPlayerPrefs)
                {
                    if (PlayerPrefs.HasKey(prefPrefix + volumeControl[i].name))
                    {
                        volumeControl[i].volume = PlayerPrefs.GetFloat(prefPrefix + volumeControl[i].name);
                    }
                }

                //reset the audio
                volumeControl[i].tempVolume = volumeControl[i].volume;

                //set the mxer to match the volume
                audioMixer.SetFloat(volumeControl[i].name, Mathf.Log(volumeControl[i].volume) * 20f);
            }
        }

        public void SetAudioLevels(string name, float volume)
        {
            if (audioMixer == null)
            {
                Debug.LogError("There is no Audiomixer defined in the profiles file");
                return;
            }

            for(int i = 0; i < volumeControl.Length;i++)
            {
                
                if (volumeControl[i].name != name)
                {
                    continue;
                }
                else
                {
                    audioMixer.SetFloat(volumeControl[i].name, Mathf.Log(volume) * 20f);
                    volumeControl[i].tempVolume = volume;
                    break;
                }
            }

        }

        public void SaveAudioLevels()
        {
            if (audioMixer == null)
            {
                Debug.LogError("There is no Audiomixer defined in the profiles file");
                return;
            }
            float volume = 0f;

            for (int i = 0; i < volumeControl.Length; i++)
            {
                volume = volumeControl[i].tempVolume;

                if (saveInPlayerPrefs)
                {
                    PlayerPrefs.SetFloat(prefPrefix + volumeControl[i].name, volume);
                }

                audioMixer.SetFloat(volumeControl[i].name, Mathf.Log(volume) * 20f);
                volumeControl[i].volume = volume;
            }
        }
    }
}

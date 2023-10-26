using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Chiro
{
    public class AudioController : MonoBehaviour
    {
        [SerializeField]
        private UI_SoundProfile m_profiles;

        [SerializeField]
        private List<UI_VolumeSlider> m_volumeSliders = new List<UI_VolumeSlider>();

        /*public UI_SoundProfile profile
        { 
            get { return m_profiles; } 
            set { SoundSettings.profile = m_profiles; }
        }*/

        private void Awake()
        {
            if (m_profiles != null)
                m_profiles.SetProfile(m_profiles);
        }

        // Start is called before the first frame update
        void Start()
        {
            if (SoundSettings.profile
                && SoundSettings.profile.audioMixer != null) SoundSettings.profile.GetAudioLevels();


        }

        // Update is called once per frame
        void Update()
        {

        }

       public void ApplyChanges()
        {
            if(SoundSettings.profile
                && SoundSettings.profile.audioMixer != null)
                    SoundSettings.profile.SaveAudioLevels();


        }

        public void CancelChanges()
        {
            for(int i =0;i<m_volumeSliders.Count;i++)
            {
                m_volumeSliders[i].ResetSliderValue();
            }
        }

    }

}
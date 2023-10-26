using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Chiro
{
    public class UI_VolumeSlider : MonoBehaviour
    {
        
        public Slider slider;

        [Header("Volume name")]
        [Tooltip("This is the name of the exposed parameter")]
        [SerializeField]
        private string volumeName;

        [Header("Volume label")]
        [SerializeField]
        private TextMeshProUGUI volumeText;

        // Start is called before the first frame update
        void Start()
        {
            ResetSliderValue();
            UpdateValueOnChange(slider.value);

            slider.onValueChanged.AddListener(delegate
            {
                UpdateValueOnChange(slider.value);
            });
        }

        
        public void UpdateValueOnChange(float value)
        {
            

            if(volumeText != null)
            {
                volumeText.text = Mathf.Round(100.0f*value).ToString()+"%";
            }

            if(SoundSettings.profile)
            {
                SoundSettings.profile.SetAudioLevels(volumeName, value);
            }
        }

        public void ResetSliderValue()
        {
            if(slider == null)
            {
                Debug.LogError("Onge slider kae");
            }
            if (SoundSettings.profile)
            {
                float volume = SoundSettings.profile.GetAudioLevels(volumeName);
                UpdateValueOnChange (volume);
                slider.value = volume;
            }
        }
    }
}

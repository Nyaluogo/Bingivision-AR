using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Localization.PropertyVariants.TrackedObjects;
using UnityEngine.Localization.PropertyVariants.TrackedProperties;
using UnityEngine.Localization.PropertyVariants;
using TMPro;
using UnityEngine.Localization.Settings;

namespace Chiro
{
    public class InterfaceTranslator : MonoBehaviour
    {
        public enum Dhok
        {
            DHOLUO,
            ENGLISH,
            UNDEFINED
        }
        public Dhok dhok;

        [System.Serializable]
        public class TextObject
        {
            public string dholuo;
            public string english;
        }



        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        void SetInitialReferences()
        {

        }

        void SetUpdateReferences()
        {
            
        }

#if UNITY_EDITOR
        [MenuItem("CONTEXT/TextMeshProUGUI/Localize (With Localized Properties)")]
        static void LocalizeTMProText(MenuCommand command)
        {
            var target = command.context as TextMeshProUGUI;

            var localizer = target.gameObject.GetComponent<GameObjectLocalizer>() ?? target.gameObject.AddComponent<GameObjectLocalizer>();
            var trackedObject = localizer.GetTrackedObject<TrackedUGuiGraphic>(target, create: true);
            var trackedProp = trackedObject.AddTrackedProperty<LocalizedStringProperty>("m_text");

            var table = LocalizationSettings.StringDatabase.GetTableAsync(trackedProp.LocalizedString.TableReference).Result;
            var entryName = table.SharedData.GetEntryFromReference(trackedProp.LocalizedString.TableEntryReference).Key;

            trackedProp.LocalizedString.SetReference("My New String Table", entryName);
        }
#endif

    }
}

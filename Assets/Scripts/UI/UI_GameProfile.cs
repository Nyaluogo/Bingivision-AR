using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Chiro.NafasStatePattern.Difficulty;

namespace Chiro
{
    [System.Serializable]
    public class Game
    {
        public string name;
        public string language;
        public string theme;
        public TekoLevel teko;

    }

    public class GameSettings
    {
        public static UI_GameProfile profile;
    }

    [CreateAssetMenu(menuName ="Nafas/Create Game Profile")]
    public class UI_GameProfile : ScriptableObject
    {
        public bool saveInPlayerPrefs = true;
        public string prefPrefix = "AudioSettings_";

        //public NafasStatePattern gamePattern;
        public Game game;

        public void SetProfile(UI_GameProfile profile)
        {
            GameSettings.profile = profile;
        }



    }
}

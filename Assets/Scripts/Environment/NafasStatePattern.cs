using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static Chiro.NafasStatePattern.Difficulty;
using static Chiro.NafasStatePattern.GraphicsQty;

namespace Chiro
{
    public class NafasStatePattern : MonoBehaviour
    {
        [Header("Decision making")]
        private float checkRate = 0.1f;
        private float nextCheck;

        public bool mapLoaded = false;


        [System.Serializable]
        public class HotSpot
        {
            public Transform playerSpawnPoint;
        }
        public HotSpot hotSpot;

        [System.Serializable]
        public class Difficulty
        {
            public enum TekoLevel { FRESHER, TRAPPER, TRAP_LORD}
            public TekoLevel level;

            [System.Serializable]
            public class Metrics
            {
                public float playerMaxHealth;
                public float hatersMaxHealth;
                public float hatersMaxDamage;
                public float hatersMaxSpeed;
                public int hatersMaxSpawned;
            }
            public Metrics fresherMetrics;
            public Metrics trapperMetrics;
            public Metrics traplordMetrics;


        }
        public Difficulty difficulty;

        [System.Serializable]
        public class Perspective
        {
            public enum PerspectiveType { FPS, TPS, NULL}
            public PerspectiveType type = PerspectiveType.TPS;
            public GameObject fpsCharacter;
            public GameObject tpsCharacter;

            public void SetPerspective(PerspectiveType perspective)
            {
                

            }

            

            public GameObject GetCharacter()
            {
                switch (type)
                {
                    case PerspectiveType.FPS:
                        if( fpsCharacter != null)
                        {
                            return fpsCharacter;
                        }
                        break;
                    case PerspectiveType.TPS:
                        if(tpsCharacter != null)
                        {
                            return tpsCharacter;
                        }
                        break;
                    case PerspectiveType.NULL:
                        break;
                    default:
                        break;
                }
                return null;
            }
        }
        public Perspective perspective;


        [System.Serializable]
        public class Kit
        {
            public string name = "Trap House";
            public GameObject floor = null;
            public GameObject wall = null;
            public GameObject pillar = null;
            public bool isInitialised = false;

            public void SetQuality(Setting setting)
            {
                if(floor != null)
                {
                    if(floor.GetComponentInChildren<MeshRenderer>() != null)
                    {
                        foreach(MeshRenderer renderer in  floor.GetComponentsInChildren<MeshRenderer>())
                        {
                            renderer.receiveShadows = setting.enableShadows;
                        }
                    }

                    if (wall.GetComponentInChildren<MeshRenderer>() != null)
                    {
                        foreach (MeshRenderer renderer in wall.GetComponentsInChildren<MeshRenderer>())
                        {
                            renderer.receiveShadows = setting.enableShadows;
                        }
                    }

                    if (pillar.GetComponentInChildren<MeshRenderer>() != null)
                    {
                        foreach (MeshRenderer renderer in pillar.GetComponentsInChildren<MeshRenderer>())
                        {
                            renderer.receiveShadows = setting.enableShadows;
                        }
                    }
                }
            }
        }
        public List<Kit> kits = null;

        [System.Serializable]
        public class GraphicsQty
        {
            public enum GraphicsLevel { LOW, MID, HIGH}
            public GraphicsLevel level;

            [System.Serializable]
            public class Setting
            {
                public bool enableShadows = false;
                public bool useLightProbes = false;
                public bool enablePosters = false;
                public bool enableFlashlights = false;
                public bool isInitialised = false;
            }
            public Setting LowSetting;
            public Setting MidSetting;
            public Setting HighSetting;
        }
        public GraphicsQty graphicsQty;


        [System.Serializable]
        public class InitReference
        {
            public List<GameObject> enableObjs;
            public List<GameObject> disableObjs;
            public bool shouldInitAds = false;

            public void ToggleObj()
            {
                if (enableObjs.Count > 0)
                {
                    foreach (GameObject obj in enableObjs)
                    {
                        if (obj != null)
                        {
                            obj.SetActive(true);
                        }
                    }

                    foreach (GameObject obj in disableObjs)
                    {
                        if (obj != null)
                        {
                            obj.SetActive(false);
                        }
                    }
                }
            }
        }
        public InitReference initRef;


        [System.Serializable]
        public class GrindReference
        {
            public List<GameObject> enableObjs;
            public List<GameObject> disableObjs;
            public List<FloorTrap> totalTrapFloors;
            public List<FloorTrap> activatedtrapFloors;
            
            public int minActiveTraps = 2;
            public int maxActiveTraps = 5;
            public int defaultNumActiveTraps;

            public bool trapInitialised = false;
            public bool shouldInitAds = false;
            public bool grindComplete = false;

            public void ToggleObj()
            {
                if (enableObjs.Count > 0)
                {
                    foreach (GameObject obj in enableObjs)
                    {
                        if (obj != null)
                        {
                            obj.SetActive(true);
                        }
                    }

                    foreach (GameObject obj in disableObjs)
                    {
                        if (obj != null)
                        {
                            obj.SetActive(false);
                        }
                    }
                }
            }

            public void InitTraps()
            {
                if (trapInitialised) return;
                
            }

        }
        public GrindReference grindRef;

        [System.Serializable]
        public class VictoryReference
        {
            public List<GameObject> enableObjs;
            public List<GameObject> disableObjs;
            public List<Transform> spawnPoints;
            public GameObject spawnVfxPrefab;
            public GameObject keyPrefab;
            public bool shouldInitAds = false;
            public bool hatersInitialised = false;
            public bool keysInitialised = false;


            

            public void ToggleObj()
            {
                if (enableObjs.Count > 0)
                {
                    foreach (GameObject obj in enableObjs)
                    {
                        if (obj != null)
                        {
                            obj.SetActive(true);
                        }
                    }

                    foreach (GameObject obj in disableObjs)
                    {
                        if (obj != null)
                        {
                            obj.SetActive(false);
                        }
                    }
                }
            }
        }
        public VictoryReference victoryRef;

        [System.Serializable]
        public class TrapHouseReference
        {
            public List<GameObject> enableObjs;
            public List<GameObject> disableObjs;
            public bool shouldInitAds = false;

            public void ToggleObj()
            {
                if (enableObjs.Count > 0)
                {
                    foreach (GameObject obj in enableObjs)
                    {
                        if (obj != null)
                        {
                            obj.SetActive(true);
                        }
                    }

                    foreach (GameObject obj in disableObjs)
                    {
                        if (obj != null)
                        {
                            obj.SetActive(false);
                        }
                    }
                }
            }
        }
        public TrapHouseReference trapHouseRef;


        [System.Serializable]
        public class Mwangaza
        {
            public List<Light> lights;
            public bool toggleOnEnter = false;
            public bool toggleOnExit = false;

            public void Toggle(bool option)
            {
                foreach (Light light in lights)
                {
                    if (light != null)
                    {
                        light.enabled = option;
                    }
                }
            }
        }
        public Mwangaza mwangaza;


        public CustomerMaster customer;
        public Customer_PlaylistController playlistController;

        [Header("State AI")]
        public INafasStateInterface currentState;
        public INafasStateInterface capturedState;
        public Nafas_InitialState initialState;
        public Nafas_GrindState grindState;
        public Nafas_VictoryLapState victoryLapState;
        public Nafas_TrapHouseState trapHouseState;



        private void Awake()
        {
            SetupStateReferences();
        }

        // Start is called before the first frame update
        void Start()
        {
            Application.targetFrameRate = 60;
            SetInitialReferences();
        }

        // Update is called once per frame
        void Update()
        {
            SetUpdateReferences();
        }
       

        void SetupStateReferences()
        {
            initialState = new Nafas_InitialState(this);
            grindState = new Nafas_GrindState(this);
            victoryLapState = new Nafas_VictoryLapState(this);
            trapHouseState = new Nafas_TrapHouseState(this);
        }

        void SetInitialReferences()
        {
            //LosNafas();
        }

        void SetUpdateReferences()
        {
            if (Time.time > nextCheck)
            {
                nextCheck = Time.time + checkRate;

                currentState.UpdateNafas();
            }
        }

        public void OnPlayerSpawn()
        {
            
            if(customer != null)
            {
                
                
            }
        }

        public void PlayerRespawn()
        {
            if (customer != null)
            {
                
            }
        }

        public void SetState(INafasStateInterface state)
        {
            if(currentState != state)
            {
                SaveState();
                currentState = state;
            }
        }

        public void SaveState()
        {
            capturedState = currentState;
        }

        
        public IEnumerator ActivateFloorTraps()
        {
            yield return null;
        }

        public void SetDifficulty(TekoLevel level)
        {
            switch (level)
            {
                case TekoLevel.FRESHER:
                    var player = playlistController.gameObject;
                    difficulty.level = level;
                    break;
                case TekoLevel.TRAPPER:

                    difficulty.level = level;
                    break;
                case TekoLevel.TRAP_LORD:

                    difficulty.level = level;
                    break;
                default:
                    break;
            }
        }

        public void SetPlayerPerspective()
        {
            if(customer != null)
            {

            }
        }

        public void SetAesthetic(Kit kt)
        {
            
        }

        public void SetGraphicsQuality(GraphicsLevel lvl)
        {
            switch (lvl)
            {
                case GraphicsLevel.LOW:
                    if(kits.Count > 0)
                    {
                        foreach(Kit kit in kits)
                        {
                            kit.SetQuality(graphicsQty.LowSetting);
                        }
                        
                    }
                    
                    break;
                case GraphicsLevel.MID:
                    if (kits.Count > 0)
                    {
                        foreach (Kit kit in kits)
                        {
                            kit.SetQuality(graphicsQty.MidSetting);
                        }

                    }
                    break;
                case GraphicsLevel.HIGH:
                    if (kits.Count > 0)
                    {
                        foreach (Kit kit in kits)
                        {
                            kit.SetQuality(graphicsQty.HighSetting);
                        }

                    }
                    break;
                default:
                    if (kits.Count > 0)
                    {
                        foreach (Kit kit in kits)
                        {
                            kit.SetQuality(graphicsQty.MidSetting);
                        }

                    }
                    break;
            }
            graphicsQty.level = lvl;
        }

        public void GameComplete()
        {

        }

        public void QuitGame()
        {
            Application.Quit();
        }

        public void RestartGame()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        public void LoadNextScene(int scene_num)
        {
            SceneManager.LoadScene(scene_num);
        }

        public void ActivateInitialState()
        {
            SetState(initialState);
        }

        public void ActivateGrindState()
        {
            SetState(grindState);
        }

        public void ActivateVictoryLapState()
        {
            SetState(victoryLapState);
        }

        public void ActivateTraphouseState()
        {
            SetState(trapHouseState);
        }
    }

}
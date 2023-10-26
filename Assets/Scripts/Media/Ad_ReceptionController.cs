using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Chiro
{
    public class Ad_ReceptionController : MonoBehaviour
    {
        public enum ReceptionState
        {
            IDLE,
            WAITING,
            ENGAGED,
            NULL
        }
        public ReceptionState currentState;

        [System.Serializable]
        public class Receptionist
        {
            public string name;
            public GameObject model;
            public AudioClip welcomeClip;
            public AudioClip pitchClip;
            public AudioClip byeClip;
        }
        public Receptionist receptionist;


        [System.Serializable]
        public class SoundByte
        {
            public AudioSource audioSource;
            public float volume;
            public float pitch;
            public float yaw;
            public List<AudioClip> idleClips;
            public List<AudioClip> pendingClips;
            public List<AudioClip> engagedClips;

            public void PlaySound(ReceptionState reception)
            {
                if (audioSource != null)
                {

                    switch (reception)
                    {
                        case ReceptionState.IDLE:
                            if (idleClips.Count > 0) break;
                            audioSource.clip = idleClips[Random.Range(0, idleClips.Count)];
                            break;
                        case ReceptionState.WAITING:
                            if (pendingClips.Count > 0) break;
                            audioSource.clip = pendingClips[Random.Range(0, pendingClips.Count)];
                            break;
                        case ReceptionState.ENGAGED:
                            if (engagedClips.Count > 0) break;
                            audioSource.clip = engagedClips[Random.Range(0, engagedClips.Count)];
                            break;
                        case ReceptionState.NULL:
                            break;
                        default:
                            break;
                    }


                    if (audioSource.clip != null)
                    {
                        audioSource.volume = volume;
                        audioSource.pitch = pitch;
                        audioSource.Play();
                    }

                }

            }
        }
        public SoundByte soundByte;

        public UI_Billboard billboard;
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
            if (other.tag == "Player")
            {
                customerInAreaOfInfluence = true;
            }

            if (billboard != null)
            {
                billboard.icon.Toggle(true);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.tag == "Player")
            {
                customerInAreaOfInfluence = false;
            }

            if (billboard != null)
            {
                billboard.icon.Toggle(false);
            }
        }

        void SetInitialReferences()
        {
            if (soundByte.audioSource == null && GetComponent<AudioSource>() != null)
            {
                soundByte.audioSource = GetComponent<AudioSource>();
            }
        }

        void SetUpdateReferences()
        {
            CheckState();
            StateMachine();
        }

        public void SetState(ReceptionState state)
        {
            currentState = state;
        }

        public void CheckState()
        {
            if(customerInAreaOfInfluence)
            {
                SetState(ReceptionState.ENGAGED);
            }
            else if(!customerInAreaOfInfluence)
            {
                SetState(ReceptionState.WAITING);
            }
        }

        public void StateMachine()
        {
            switch (currentState)
            {
                case ReceptionState.IDLE:
                    break;
                case ReceptionState.WAITING:
                    break;
                case ReceptionState.ENGAGED:
                    break;
                case ReceptionState.NULL:
                    break;
                default:
                    break;
            }
        }
    }
}

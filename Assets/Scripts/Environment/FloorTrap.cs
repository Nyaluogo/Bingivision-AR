using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Chiro
{
    public class FloorTrap : MonoBehaviour
    {
        public Animator animator;
        public string trappinBoolAnim = "IsTrapping";
        public string openBoolAnim = "IsOpen";

        public MeshRenderer trappinIndicator;
        public float trapDuration = 20.0f;
        public bool isTrapping = false;
        public bool isChosen = false;

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

        }

        void SetUpdateReferences()
        {
            if(animator != null)
            {
                if(isChosen)
                {
                    StartCoroutine(Trap());
                }
                else
                {
                    OpenDoor(false);
                    ToggleTraps(false);
                }
            }
        }

        public IEnumerator Trap()
        {
            isTrapping = true;
            OpenDoor(true);
            ToggleTraps(true);
            yield return new WaitForSeconds(trapDuration);
            isChosen = false;
            isTrapping = false;
        }

        public void OpenDoor(bool option)
        {
            if(animator != null)
            {
                animator.SetBool(openBoolAnim, option);
            }
        }

        public void ToggleTraps(bool option)
        {
            if (animator != null)
            {
                animator.SetBool(trappinBoolAnim, option);
            }
        }
    }
}

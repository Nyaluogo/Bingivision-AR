using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestNullCanvas : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Debug.LogError(GetNullPosition());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public string GetNullPosition()
    {
        if (transform.childCount > 0)
        {
            Transform[] kids = transform.GetComponentsInChildren<Transform>();
            foreach (Transform t in kids)
            {
                if (t.position == null)
                {
                    return t.name;
                }
                Debug.Log(t.position + " " + t.name);
            }
            
            //DestroyImmediate(transform.GetChild(0).gameObject);
        }
        
        return "not found";
    }
}

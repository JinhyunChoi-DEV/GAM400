using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class VFXTestPlayer : MonoBehaviour
{
    [SerializeField] private KeyCode key;
    [SerializeField] private VisualEffect[] vfxGraph;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(key))
        {
            for (int i = 0; i < vfxGraph.Length; ++i)
            {
                if (vfxGraph[i] != null)
                    vfxGraph[i].Play();
            }
        }
    }

}

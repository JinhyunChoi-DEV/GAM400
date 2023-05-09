using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class DissolveController : MonoBehaviour
{
    [Header("Modifiable Values")] 
    [SerializeField] private float dissolveRate = 0.0125f;   // rate of dissolve
    [SerializeField] private float refreshRate = 0.025f;    // rate of time which the dissolve visually updates
    
    [Header("Dependencies")]
    [SerializeField] private SkinnedMeshRenderer skinnedMesh;
    [SerializeField] private VisualEffect[] vfxGraph;
    

    private Material material;
    
    void Start()
    {
        // retrieves material from mesh
        if (skinnedMesh != null)
            material = skinnedMesh.material;
    }
    
    void Update()
    {
        // on dissolve event
        if (Input.GetKeyDown(KeyCode.X))
        {
            StartCoroutine(DissolveRoutine());
        }
    }

    // modifies the dissolve amount property of the material until it reaches 1 (fully dissolved)
    IEnumerator DissolveRoutine()
    {
        for(int i = 0; i < vfxGraph.Length; ++i)
        {
            if (vfxGraph[i] != null)
                vfxGraph[i].Play();
        }
        
        if (material != null)
        {
            float curr_dissolve_amount = 0f;

            while (material.GetFloat("_DissolveAmount") < 1f)
            {
                curr_dissolve_amount += dissolveRate;
                material.SetFloat("_DissolveAmount", curr_dissolve_amount);
                yield return new WaitForSeconds(refreshRate);
            }
        }
    }
}

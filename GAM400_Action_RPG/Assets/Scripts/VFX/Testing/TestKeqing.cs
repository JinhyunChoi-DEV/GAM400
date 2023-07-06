using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestKeqing : MonoBehaviour
{
    [SerializeField] private Material material;
    private MeshRenderer mesh_renderer;
    private MeshFilter mesh_filter;
    private List<GameObject> husks = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        mesh_renderer = GetComponent<MeshRenderer>();
        mesh_filter = GetComponent<MeshFilter>();
    }

    // creates mesh at current location
    void CreateHusk()
    {
        husks.Add(new GameObject());
        int index = husks.Count - 1;
        husks[index].transform.position = transform.position;

        MeshRenderer mr = husks[index].AddComponent<MeshRenderer>();
        MeshFilter mf = husks[index].AddComponent<MeshFilter>();

        mf.mesh = mesh_filter.mesh;
        mr.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
        mr.material = material;
    }

    void ClearHusks()
    {
        foreach (GameObject g in husks)
            GameObject.Destroy(g);
        husks.Clear();
    }
}

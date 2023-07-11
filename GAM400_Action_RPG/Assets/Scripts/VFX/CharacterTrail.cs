using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

namespace BattleZZang
{
    public class CharacterTrail : MonoBehaviour
    {
        [SerializeField] private float active_time = 2f;
        [SerializeField] private float mesh_refresh_rate = 0.1f;
        [SerializeField] private float mesh_destroy_time = 3f;

        [SerializeField] private Material material;
        [SerializeField] private string shader_ref;
        [SerializeField] private float shader_rate = 0.1f;
        [SerializeField] private float shader_refresh = 0.05f;
        
        private bool trail_active;
        private SkinnedMeshRenderer[] meshes;
        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKey(KeyCode.Tab) && !trail_active)
            {
                trail_active = true;
                StartCoroutine(ActivateTrail(active_time));
            }
        }

        IEnumerator ActivateTrail(float time_active)
        {
            while (time_active > 0)
            {
                time_active -= mesh_refresh_rate;

                if (meshes == null)
                    meshes = GetComponentsInChildren<SkinnedMeshRenderer>();

                foreach (var smesh in meshes)
                {
                    GameObject obj = new GameObject();
                    obj.transform.SetPositionAndRotation(transform.position, transform.rotation);
                        
                    MeshRenderer mr = obj.AddComponent<MeshRenderer>();
                    MeshFilter mf = obj.AddComponent<MeshFilter>();

                    Mesh mesh = new Mesh();
                    smesh.BakeMesh(mesh);

                    mf.mesh = mesh;
                    mr.material = material;
                    mr.shadowCastingMode = ShadowCastingMode.Off;

                    StartCoroutine(AnimateMaterialFloat(mr.material, 0, shader_rate, shader_refresh));
                    
                    Destroy(obj, mesh_destroy_time);
                }
            }
            yield return new WaitForSeconds(mesh_refresh_rate);

            trail_active = false;
        }

        IEnumerator AnimateMaterialFloat(Material mat, float goal, float rate, float refresh_rate)
        {
            float animate_value = mat.GetFloat(shader_ref);

            while (animate_value > goal)
            {
                animate_value -= rate;
                mat.SetFloat(shader_ref, animate_value);
                yield return new WaitForSeconds(refresh_rate);
            }
        }
    }
}

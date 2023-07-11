using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BattleZZang
{
    public class ObjectEnabler : MonoBehaviour
    {
        [SerializeField] private KeyCode key;
        [SerializeField] private GameObject[] objects;
        
        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(key))
            {
                for (int i = 0; i < objects.Length; ++i)
                {
                    objects[i].SetActive(!objects[i].activeSelf);
                }
            }
        }
    }
}

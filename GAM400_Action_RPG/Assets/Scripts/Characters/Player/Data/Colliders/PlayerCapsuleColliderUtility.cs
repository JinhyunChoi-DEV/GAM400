using System;
using UnityEngine;

namespace BattleZZang
{
    [Serializable]
    public class PlayerCapsuleColliderUtility
    {
        [field: SerializeField] public CapsuleColliderUtility CapsuleColliderUtility { get; private set; }
        [field: SerializeField] public PlayerTriggerColliderData TriggerColliderData { get; private set; }

        public void Initialize(GameObject gameObject)
        {
            CapsuleColliderUtility.Initialize(gameObject);
            TriggerColliderData.Initialize();
        }
    }
}

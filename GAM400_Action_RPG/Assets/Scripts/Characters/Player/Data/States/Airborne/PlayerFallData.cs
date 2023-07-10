using System;
using UnityEngine;

namespace BattleZZang
{
    [Serializable]
    public class PlayerFallData
    {
        [field: SerializeField] [field: Range(1.0f, 15.0f)] public float FallSpeedLimit { get; private set; } = 15.0f;
        [field: SerializeField] [field: Range(0.0f, 100.0f)] public float HardFallDistance { get; private set; } = 3.0f;

    }
}

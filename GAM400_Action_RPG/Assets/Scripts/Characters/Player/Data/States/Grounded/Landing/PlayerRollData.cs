using System;
using UnityEngine;

namespace BattleZZang
{
    [Serializable]
    public class PlayerRollData
    {
        [field: SerializeField] [field: Range(0.0f, 3.0f)] public float SpeedModifer { get; private set; } = 1.0f;
    }
}

using System;
using System.Collections.Generic;
using UnityEngine;

namespace BattleZZang
{
    [Serializable]
    public class PlayerWalkData
    {
        [field: SerializeField] [field: Range(0, 1.0f)] public float SpeedModifier { get; private set; } = 0.25f;
        [field: SerializeField] public List<PlayerCameraRecenteringData> BackwardsCameraRecenteringData { get; private set; }

    }
}

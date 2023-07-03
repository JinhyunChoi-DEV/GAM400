using System;
using UnityEngine;

namespace BattleZZang
{
    [Serializable]
    public class PlayerGroundedData
    {
        [field: SerializeField] [field: Range(0, 25.0f)] public float BaseSpeed { get; private set; } = 5.0f;
        [field: SerializeField] public AnimationCurve SlopeDecreaseSpeedByAngles { get; private set; }
        [field: SerializeField] public AnimationCurve SlopeIncreaseSpeedByAngles { get; private set; }
        [field: SerializeField] public PlayerRotationData BaseRotationData { get; private set; }
        [field: SerializeField] public PlayerWalkData WalkData { get; private set; }
        [field: SerializeField] public PlayerRunData RunData { get; private set; }
    }
}

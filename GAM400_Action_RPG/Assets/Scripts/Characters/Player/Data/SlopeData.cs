using System;
using UnityEngine;

namespace BattleZZang
{
    [Serializable]
    public class SlopeData
    {
        [field: SerializeField] [field: Range(0, 1)] public float StepHeightPercentage { get; private set; } = 0.25f;
        [field: SerializeField][field: Range(0, 5)] public float FloatDistance { get; private set; } = 2.0f;
        [field: SerializeField][field: Range(0, 50)] public float StepReachForce { get; private set; } = 25.0f;

    }
}

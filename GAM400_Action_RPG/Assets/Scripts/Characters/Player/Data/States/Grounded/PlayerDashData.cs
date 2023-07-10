using System;
using UnityEngine;

namespace BattleZZang
{
    [Serializable]
    public class PlayerDashData
    {
        [field: SerializeField][field: Range(1f, 3f)] public float SpeedModifer { get; private set; } = 2.0f;
        [field: SerializeField] public PlayerRotationData RotationData { get; private set; }
        [field: SerializeField][field: Range(0f, 2f)] public float TimeToConsideredConsecutive{ get; private set; } = 1.0f;

        [field: SerializeField] [field: Range(1, 10)] public int ConsecutiveLimitCount { get; private set; } = 2;
        [field: SerializeField][field: Range(0f, 5f)] public float DashLimitCooldown { get; private set; } = 1.75f;
    }
}

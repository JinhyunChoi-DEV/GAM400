using System;
using System.Collections.Generic;
using UnityEngine;

namespace BattleZZang
{
    [Serializable]
    public class PlayerGroundedData
    {
        [field: SerializeField] [field: Range(0f, 25.0f)] public float BaseSpeed { get; private set; } = 5.0f;
        [field: SerializeField][field: Range(0f, 5.0f)] public float FallRayDistance { get; private set; } = 1.0f;
        [field: SerializeField] public AnimationCurve SlopeDecreaseSpeedByAngles { get; private set; }
        [field: SerializeField] public AnimationCurve SlopeIncreaseSpeedByAngles { get; private set; }
        [field: SerializeField] public PlayerRotationData BaseRotationData { get; private set; }
        [field: SerializeField] public PlayerIdleData IdleData  { get; private set; }
        [field: SerializeField] public PlayerWalkData WalkData { get; private set; }
        [field: SerializeField] public PlayerRunData RunData { get; private set; }
        [field: SerializeField] public PlayerDashData DashData { get; private set; }
        [field: SerializeField] public PlayerSprintData SprintData { get; private set; }
        [field: SerializeField] public PlayerStopData StopData { get; private set; }
        [field: SerializeField] public PlayerRollData RollData { get; private set; }
        [field: SerializeField] public List<PlayerCameraRecenteringData> SidewaysCameraRecenteringData { get; private set; }
        [field: SerializeField] public List<PlayerCameraRecenteringData> BackwardsCameraRecenteringData { get; private set; }
    }
}

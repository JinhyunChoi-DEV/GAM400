using System;
using UnityEngine;

namespace BattleZZang
{
    [Serializable]
    public class PlayerCameraRecenteringData
    {
        [field: SerializeField][field:Range(0f, 360.0f)] public float MinAngle { get; private set; }
        [field: SerializeField][field:Range(0f, 360.0f)] public float MaxAngle { get; private set; }
        [field: SerializeField][field:Range(-1f, 20.0f)] public float WaitTime { get; private set; }
        [field: SerializeField][field:Range(-1f, 20.0f)] public float RecenteringTime { get; private set; }

        public bool IsValidRange(float angle)
        {
            return angle >= MinAngle && angle <= MaxAngle;
        }

    }
}

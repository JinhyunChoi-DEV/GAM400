using System.Collections.Generic;
using UnityEngine;

namespace BattleZZang
{
    public class PlayerMovementShareData
    {
        public Vector2 MovementInput { get; set; }
        public float MoveSpeedModifier { get; set; } = 1.0f;
        public float MoveDecelerationForce { get; set; } = 1.0f;
        public bool IsWalk { get; set; }
        public bool IsSprint { get; set; }
        public float RotationAngle { get; set; }
        public int RotationDirection { get; set; } //-1: left, 1: right, base: 0

        public Vector3 CurrentJumpForce { get; set; }
        public List<PlayerCameraRecenteringData> SidewaysCameraRecenteringData { get; set; }
        public List<PlayerCameraRecenteringData> BackwardsCameraRecenteringData { get; set; }

        private Vector3 currentTargetRotation;
        private Vector3 timeToReachTargetRotation;
        private Vector3 dampedTargetRotationCurrentVelocity;
        private Vector3 dampedTargetRotationPassedTime;

        public ref Vector3 CurrentTargetRotation
        {
            get
            {
                return ref currentTargetRotation;
            }
        }

        public ref Vector3 TimeToReachTargetRotation
        {
            get
            {
                return ref timeToReachTargetRotation;
            }
        }

        public ref Vector3 DampedTargetRotationCurrentVelocity
        {
            get
            {
                return ref dampedTargetRotationCurrentVelocity;
            }
        }

        public ref Vector3 DampedTargetRotationPassedTime
        {
            get
            {
                return ref dampedTargetRotationPassedTime;
            }
        }

        public PlayerRotationData RotationData { get; set; }
    }
}

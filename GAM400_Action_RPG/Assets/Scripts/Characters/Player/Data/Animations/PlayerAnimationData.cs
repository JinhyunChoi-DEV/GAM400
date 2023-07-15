using System;
using UnityEngine;

namespace BattleZZang
{
    [Serializable]
    public class PlayerAnimationData
    {
        [Header("State Group Parameter Names")] 
        [SerializeField] private string groundedParameterName = "Grounded";
        [SerializeField] private string movingParameterName = "Moving";
        [SerializeField] private string stoppingParameterName = "Stopping";
        [SerializeField] private string landingParameterName = "Landing";
        [SerializeField] private string airborneParameterName = "Airborne";

        [Header("Grounded Parameter Names")]
        [SerializeField] private string idleParameterName = "IsIdle";
        [SerializeField] private string walkParameterName = "IsWalk";
        [SerializeField] private string runParameterName = "IsRun";
        [SerializeField] private string sprintParameterName = "IsSprint";
        [SerializeField] private string mediumStopParameterName = "IsMediumStop";
        [SerializeField] private string hardStopParameterName = "IsHardStop";
        [SerializeField] private string rollParameterName = "IsRoll";
        [SerializeField] private string hardLandParameterName = "IsHardLand";

        [Header("Airborne Parameter Names")]
        [SerializeField] private string fallParameterName = "IsFall";

        public int GroundedParameterHash { get; private set; }
        public int MoveParameterHash { get; private set; }
        public int StopParameterHash { get; private set; }
        public int LandParameterHash { get; private set; }
        public int AirborneParameterHash { get; private set; }

        public int IdleParameterHash { get; private set; }
        public int WalkParameterHash { get; private set; }
        public int RunParameterHash { get; private set; }
        public int SprintParameterHash { get; private set; }
        public int MediumStopParameterHash { get; private set; }
        public int HardStopParameterHash { get; private set; }
        public int RollParameterHash { get; private set; }
        public int HardLandParameterHash { get; private set; }
        public int FallParameterHash { get; private set; }

        public void Initialize()
        {
            GroundedParameterHash = Animator.StringToHash(groundedParameterName);
            MoveParameterHash = Animator.StringToHash(movingParameterName);
            StopParameterHash = Animator.StringToHash(stoppingParameterName);
            LandParameterHash = Animator.StringToHash(landingParameterName);
            AirborneParameterHash = Animator.StringToHash(airborneParameterName);

            IdleParameterHash = Animator.StringToHash(idleParameterName);
            WalkParameterHash = Animator.StringToHash(walkParameterName);
            RunParameterHash = Animator.StringToHash(runParameterName);
            SprintParameterHash = Animator.StringToHash(sprintParameterName);
            MediumStopParameterHash = Animator.StringToHash(mediumStopParameterName);
            HardStopParameterHash = Animator.StringToHash(hardStopParameterName);
            RollParameterHash = Animator.StringToHash(rollParameterName);
            HardLandParameterHash = Animator.StringToHash(hardLandParameterName);

            FallParameterHash = Animator.StringToHash(fallParameterName);
        }
    }
}

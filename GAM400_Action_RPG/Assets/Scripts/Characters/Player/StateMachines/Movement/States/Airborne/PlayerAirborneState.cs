using UnityEngine;

namespace BattleZZang
{
    public class PlayerAirborneState : PlayerMoveState
    {
        public PlayerAirborneState(PlayerMoveStateMachine stateMachine) : base(stateMachine)
        { }

        public override void Enter()
        {
            base.Enter();
            StartAnimation(animationData.AirborneParameterHash);

            ResetSprintState();
        }

        public override void Exit()
        {
            base.Exit();
            StopAnimation(animationData.AirborneParameterHash);
        }

        protected override void OnContactWithGround(Collider collider)
        {
            stateMachine.Change(stateMachine.LightLand);
        }

        protected virtual void ResetSprintState()
        {
            movementShareData.IsSprint = false;
        }
    }
}

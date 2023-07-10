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

            ResetSprintState();
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

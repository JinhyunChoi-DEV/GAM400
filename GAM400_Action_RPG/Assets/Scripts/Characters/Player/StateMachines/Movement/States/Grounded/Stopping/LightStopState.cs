using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BattleZZang
{
    public class LightStopState : PlayerStopState
    {
        public LightStopState(PlayerMoveStateMachine stateMachine) : base(stateMachine)
        {
        }

        public override void Enter()
        {
            base.Enter();

            movementShareData.MoveDecelerationForce = moveData.StopData.LightDecelerationForce;
            movementShareData.CurrentJumpForce = airborneData.JumpData.WeakForce;
        }
    }
}

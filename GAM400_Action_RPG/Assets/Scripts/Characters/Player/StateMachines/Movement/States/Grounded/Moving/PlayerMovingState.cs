using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BattleZZang
{
    public class PlayerMovingState : PlayerGroundedState
    {
        public PlayerMovingState(PlayerMoveStateMachine stateMachine) : base(stateMachine)
        {
        }

        public override void Enter()
        {
            base.Enter();
            StartAnimation(animationData.MoveParameterHash);
        }

        public override void Exit()
        {
            base.Exit();
            StopAnimation(animationData.MoveParameterHash);
        }
    }
}

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
    }
}

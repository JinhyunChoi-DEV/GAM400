using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BattleZZang
{
    public class PlayerMoveState : IState
    {
        protected readonly PlayerMoveStateMachine stateMachine;
        protected readonly CharacterMoveData moveData;
        protected Vector2 moveInput;

        private PlayerInput input => stateMachine.Player.Input;
        private PlayerPhysics physics => stateMachine.Player.Physics;

        public PlayerMoveState(PlayerMoveStateMachine stateMachine, CharacterMoveData moveData)
        {
            this.stateMachine = stateMachine;
            this.moveData = moveData;
        }

        public virtual void Enter()
        {
            Debug.Log("State: " + GetType().Name);
        }

        public virtual void Exit()
        {
        }

        public virtual void HandleInput()
        {
            ReadMoveInput();
        }

        public virtual void Update()
        {
        }

        public virtual void FixedUpdate()
        {
            Move();
        }

        private void ReadMoveInput()
        {
            moveInput = input.PlayerActions.Movement.ReadValue<Vector2>();
        }

        private void Move()
        {
            if (moveInput == Vector2.zero || moveData.SpeedModifier == 0.0f)
                return;

            var dir = GetMoveDirection();
            var speed = GetSpeed();
            physics.ApplyForce(dir, speed);
        }

        protected Vector3 GetMoveDirection()
        {
            return new Vector3(moveInput.x, 0.0f, moveInput.y);
        }

        protected float GetSpeed()
        {
            return moveData.BaseSpeed * moveData.SpeedModifier;
        }
    }
}

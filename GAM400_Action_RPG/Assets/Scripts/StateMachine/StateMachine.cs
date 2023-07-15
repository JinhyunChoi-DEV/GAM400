using UnityEngine;

namespace BattleZZang
{
    public abstract class StateMachine
    {
        protected IState current;

        public void Change(IState next)
        {
            current?.Exit();

            current = next;

            current.Enter();
        }

        public void HandleInput()
        {
            current?.HandleInput();
        }

        public void Update()
        {
            current?.Update();
        }

        public void FixedUpdate()
        {
            current?.FixedUpdate();
        }

        public void OnAnimationEnter()
        {
            current?.OnAnimationEnter();
        }

        public void OnAnimationExit()
        {
            current?.OnAnimationExit();
        }

        public void OnAnimationTransition()
        {
            current?.OnAnimationTransition();
        }

        public void OnTriggerEnter(Collider collider)
        {
            current?.OnTriggerEnter(collider);
        }

        public void OnTriggerExit(Collider collider)
        {
            current?.OnTriggerExit(collider);
        }

        public void OnAnimatorIK(int layerIndex)
        {
            current?.OnAnimatorIK(layerIndex);
        }
    }
}

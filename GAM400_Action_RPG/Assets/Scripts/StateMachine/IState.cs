using UnityEngine;

namespace BattleZZang
{
    public interface IState
    {
        public void Enter();
        public void Exit();
        public void HandleInput();
        public void Update();
        public void FixedUpdate();
        public void OnAnimationEnter();
        public void OnAnimationExit();
        public void OnAnimationTransition();
        public void OnTriggerEnter(Collider collider);
        public void OnTriggerExit(Collider collider);

        public void OnAnimatorIK(int layerIndex);
    }
}

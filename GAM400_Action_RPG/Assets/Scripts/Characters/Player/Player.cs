using UnityEngine;

namespace BattleZZang
{
    [RequireComponent(typeof(PlayerInput), typeof(PlayerPhysics), typeof(PlayerCamera))]
    public class Player : MonoBehaviour
    {
        [field: SerializeField] public PlayerSO Data { get; private set; }
        [field: SerializeField] public PlayerCameraUtility CameraUtility { get; private set; }
        [field: SerializeField] public PlayerAnimationData AnimationData { get; private set; }
        [field: SerializeField] public PlayerLookAt LookAt { get; private set; }
        [field: SerializeField] public Animator Animator { get; private set; }

        public PlayerInput Input { get; private set; }
        public PlayerPhysics Physics { get; private set; }
        public PlayerCamera Camera { get; private set; }

        public PlayerMoveStateMachine MoveStateMachine { get; private set; }
        
        void Awake()
        {
            Input = GetComponent<PlayerInput>();
            Physics = GetComponent<PlayerPhysics>();
            Camera = GetComponent<PlayerCamera>();

            CameraUtility.Initialize();
            AnimationData.Initialize();
            LookAt.Initialize();

            MoveStateMachine = new PlayerMoveStateMachine(this);
        }

        void Start()
        {
            MoveStateMachine.Change(MoveStateMachine.Idle);
        }

        void Update()
        {
            MoveStateMachine.HandleInput();

            MoveStateMachine.Update();
        }

        void FixedUpdate()
        {
            MoveStateMachine.FixedUpdate();
        }

        public void OnMoveStateAnimationEventEnter()
        {
            MoveStateMachine.OnAnimationEnter();
        }

        public void OnMoveStateAnimationEventExit()
        {
            MoveStateMachine.OnAnimationExit();
        }

        public void OnMoveStateAnimationTransitionEvent()
        {
            MoveStateMachine.OnAnimationTransition();
        }

        private void OnAnimatorIK(int layerIndex)
        {
            MoveStateMachine.OnAnimatorIK(layerIndex);
        }
    }
}

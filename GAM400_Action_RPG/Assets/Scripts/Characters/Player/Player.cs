using UnityEngine;

namespace BattleZZang
{
    [RequireComponent(typeof(PlayerInput), typeof(PlayerPhysics), typeof(PlayerCamera))]
    public class Player : MonoBehaviour
    {
        [field: SerializeField] public PlayerSO Data { get; private set; }
        public PlayerInput Input { get; private set; }
        public PlayerPhysics Physics { get; private set; }
        public PlayerCamera Camera { get; private set; }

        private PlayerMoveStateMachine moveStateMachine;

        void Awake()
        {
            Input = GetComponent<PlayerInput>();
            Physics = GetComponent<PlayerPhysics>();
            Camera = GetComponent<PlayerCamera>();

            moveStateMachine = new PlayerMoveStateMachine(this);
        }

        void Start()
        {
            moveStateMachine.Change(moveStateMachine.Idle);
        }

        void Update()
        {
            moveStateMachine.HandleInput();

            moveStateMachine.Update();
        }

        void FixedUpdate()
        {
            moveStateMachine.FixedUpdate();
        }
    }
}

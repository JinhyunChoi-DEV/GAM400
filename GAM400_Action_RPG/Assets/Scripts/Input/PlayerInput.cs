using UnityEngine;

namespace BattleZZang
{
    public class PlayerInput : MonoBehaviour
    {
        public PlayerInputActions InptActions { get; private set; }
        public PlayerInputActions.PlayerActions PlayerActions { get; private set; }

        private void Awake()
        {
            InptActions = new PlayerInputActions();

            PlayerActions = InptActions.Player;
        }

        private void OnEnable()
        {
            InptActions.Enable();
        }

        private void OnDisable()
        {
            InptActions.Disable();
        }
    }
}

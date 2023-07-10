using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

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

        public void DisableActionFor(InputAction action, float seconds)
        {
            StartCoroutine(DisableAction(action, seconds));
        }

        private IEnumerator DisableAction(InputAction action, float seconds)
        {
            action.Disable();
            yield return new WaitForSeconds(seconds);
            action.Enable();
        }
    }
}

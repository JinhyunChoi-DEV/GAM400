using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace BattleZZang
{
    public class DebugUI : MonoBehaviour
    {
        [SerializeField] private Player player;
        [SerializeField] private TMP_Text stateText;
        [SerializeField] private Toggle ik;
        [SerializeField] private Toggle headDebug;

        void Start()
        {
            ik.isOn = player.ActiveIK;
            headDebug.isOn = player.LookAt.ShowDebug;

            ik.onValueChanged.AddListener(IKToggle);
            headDebug.onValueChanged.AddListener(HeadDebugOnOff);
        }

        void Update()
        {
            if(player.MoveStateMachine != null)
                stateText.text = player.MoveStateMachine.GetState();
        }

        private void IKToggle(bool isActive)
        {
            player.ActiveIK = isActive;
        }

        private void HeadDebugOnOff(bool isActive)
        {
            player.LookAt.ShowDebug = isActive;
        }
    }
}

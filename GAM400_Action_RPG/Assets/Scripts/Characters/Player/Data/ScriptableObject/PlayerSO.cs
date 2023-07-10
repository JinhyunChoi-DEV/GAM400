using UnityEngine;

namespace BattleZZang
{
    [CreateAssetMenu(fileName = "Player", menuName = "Custom/Character/Player")]
    public class PlayerSO : ScriptableObject
    {
        [field: SerializeField] public PlayerGroundedData GroundedData { get; private set; }
    }
}

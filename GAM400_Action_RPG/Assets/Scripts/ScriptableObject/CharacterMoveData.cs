using UnityEngine;

namespace BattleZZang
{
    [CreateAssetMenu(fileName = "CharacterMoveData", menuName = "Scriptable Object/Character Move Data")]
    public class CharacterMoveData : ScriptableObject
    {
        [Header("Speed")]
        [SerializeField] private float baseSpeed;
        public float BaseSpeed { get { return baseSpeed; } }

        [SerializeField] private float speedModifier;
        public float SpeedModifier { get { return speedModifier; } }

    }

}

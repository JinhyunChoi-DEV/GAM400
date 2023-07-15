using System;
using UnityEngine;

namespace BattleZZang
{
    [Serializable]
    public class PlayerRunData 
    {
        [field: SerializeField][field: Range(0.2f, 2.0f)] public float SpeedModifier { get; private set; } = 1f;
    }
}

using System;
using UnityEngine;

namespace BattleZZang
{
    [Serializable]
    public class PlayerLayerData 
    {
        [field: SerializeField] public LayerMask GroundLayer { get; private set; }

        // 6: Environment, 7: GroundCheck 
        public bool ContainsLayer(LayerMask layerMask, int layer)
        {
            return (1 << layer & layerMask) != 0;
        }

        public bool IsGroundLayer(int layer)
        {
            return ContainsLayer(GroundLayer, layer);
        }
    }
}

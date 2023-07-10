using System;
using UnityEngine;

namespace BattleZZang
{
    public class CapsuleColliderData 
    {
        public CapsuleCollider Collider { get; private set; }
        public Vector3 ColliderCenterInLocalSpace { get; private set; }
        public Vector3 ColliderVerticalExtents { get; private set; }

        public void Initialize(GameObject gameObject)
        {
            if (Collider != null)
                return;

            Collider = gameObject.GetComponent<CapsuleCollider>();
            UpdateColliderCenter();
        }

        public void UpdateColliderCenter()
        {
            ColliderCenterInLocalSpace = Collider.center;

            ColliderVerticalExtents = new Vector3(0f, Collider.bounds.extents.y, 0f);
        }
    }
}

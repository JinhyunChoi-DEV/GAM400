using System;
using UnityEngine;

namespace BattleZZang
{
    [Serializable]
    public class CapsuleColliderUtility
    {
        public CapsuleColliderData CapsuleColliderData { get; private set; }
        [field: SerializeField] public DefaultColliderData DefaultColliderData { get; private set; }
        [field: SerializeField] public SlopeData SlopeData { get; private set; }

        public void Initialize(GameObject gameObject)
        {
            if (CapsuleColliderData != null)
                return;

            CapsuleColliderData = new CapsuleColliderData();

            CapsuleColliderData.Initialize(gameObject);
        }

        public void CalculateCapsuleColliderDimension()
        {
            var height = DefaultColliderData.Height * (1.0f - SlopeData.StepHeightPercentage);
            
            SetCapsuleColliderRadius(DefaultColliderData.Radius);
            SetCapsuleColliderHeight(height);
            RecalculateCapsuleColliderCenter();

            var heightHalf = CapsuleColliderData.Collider.height/2.0f;
            var radius = CapsuleColliderData.Collider.radius;
            if (heightHalf < radius)
                SetCapsuleColliderRadius(heightHalf);

            CapsuleColliderData.UpdateColliderCenter();
        }

        public void SetCapsuleColliderRadius(float radius)
        {
            CapsuleColliderData.Collider.radius = radius;
        }

        public void SetCapsuleColliderHeight(float height)
        {
            CapsuleColliderData.Collider.height = height;
        }

        public void RecalculateCapsuleColliderCenter()
        {
            float difference = DefaultColliderData.Height - CapsuleColliderData.Collider.height;
            float newYPos = DefaultColliderData.CenterY + (difference / 2.0f);

            Vector3 newCenter = new Vector3(0, newYPos, 0);
            CapsuleColliderData.Collider.center = newCenter;
        }
    }
}

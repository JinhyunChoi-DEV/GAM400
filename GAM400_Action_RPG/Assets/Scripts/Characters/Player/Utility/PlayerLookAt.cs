using System;
using UnityEngine;

namespace BattleZZang
{
    [Serializable]
    public class PlayerLookAt 
    {
        [field: SerializeField] public Transform LookAtTransform { private set; get; }

        [field: SerializeField] [field: Range(0.1f, 5.0f)] public float Radius { get; private set; } = 0.5f;
        [field: SerializeField] public bool ShowDebug { get; private set; }

        private PlayerMovementShareData data;
        private Transform debugObject;

        public void Initialize(Player player)
        {
            data = player.MoveStateMachine.MovementShareData;
            debugObject = LookAtTransform.GetChild(0);
        }

        public void Update()
        {
            debugObject.gameObject.SetActive(ShowDebug);

            float targetAngle = data.RotationAngle;
            if (Mathf.Abs(data.RotationAngle) > 90)
                targetAngle = (Math.Sign(targetAngle) == 1) ? 90.0f : -90.0f;

            float radian = Mathf.Deg2Rad * targetAngle;
            float x = Radius * Mathf.Sin(radian);
            float y = LookAtTransform.localPosition.y;
            float z = Radius * Mathf.Cos(radian);

            LookAtTransform.localPosition = new Vector3(x, y, z);
        }
    }
}

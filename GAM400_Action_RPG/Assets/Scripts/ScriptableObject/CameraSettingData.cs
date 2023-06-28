using UnityEngine;

namespace BattleZZang
{
    [CreateAssetMenu(fileName = "CameraSettingData", menuName = "Scriptable Object/Camera Setting Data")]
    public class CameraSettingData : ScriptableObject
    {
        [Header("FOV")]
        [SerializeField] [Range(20, 90)] private float fov;
        public float FOV { get { return fov; } }


        [Header("Sensitivity - Vertical")]
        [SerializeField] private float verticalMinRange;
        public float VerticalMinRange { get { return verticalMinRange; } }

        [SerializeField] private float verticalMaxRange;
        public float VerticalMaxRange { get { return verticalMaxRange; } }

        [SerializeField] private float verticalSpeed;
        public float VerticalSpeed { get { return verticalSpeed; } }

        [SerializeField] private float verticalAccelTime;
        public float VerticalAccelTime { get { return verticalAccelTime; } }

        [SerializeField] private float verticalDecelTime;
        public float VerticalDecelTime { get { return verticalDecelTime; } }


        [Header("Sensitivity - Horizontal")]
        [SerializeField] private float horizontalMinRange;
        public float HorizontalMinRange { get { return horizontalMinRange; } }

        [SerializeField] private float horizontalMaxRange;
        public float HorizontalMaxRange { get { return horizontalMaxRange; } }

        [SerializeField] private float horizontalSpeed;
        public float HorizontalSpeed { get { return horizontalSpeed; } }

        [SerializeField] private float horizontalAccelTime;
        public float HorizontalAccelTime { get { return horizontalAccelTime; } }

        [SerializeField] private float horizontalDecelTime;
        public float HorizontalDecelTime { get { return horizontalDecelTime; } }
    }
}

using UnityEngine;

namespace BattleZZang
{
    [CreateAssetMenu(fileName = "CameraZoomData", menuName = "Scriptable Object/Camera Zoom Data")]
    public class CameraZoomData: ScriptableObject
    {
        [Header("Distance")]
        [SerializeField] [Range(1, 10)] private float baseDistance;
        public float BaseDistance { get { return baseDistance; } }

        [SerializeField] [Range(1, 10)] private float minDistance;
        public float MinDistance { get { return minDistance; } }

        [SerializeField] [Range(1, 10)] private float maxDistance;
        public float MaxDistance { get { return maxDistance; } }


        [Header("Sensitivity")] 
        [SerializeField] [Range(1, 10)] private float smoothing;
        public float Smoothing { get { return smoothing; } }

        [SerializeField] [Range(1, 10)] private float zoomSensitivity;
        public float ZoomSensitivity { get { return zoomSensitivity; } }
    }
}

using System;
using Cinemachine;
using UnityEngine;

namespace BattleZZang
{
    public class PlayerCameraZoom
    {
        private readonly CinemachineFramingTransposer framingTransposer;
        private readonly CinemachineInputProvider inputProvider;
        private readonly CameraZoomData data;
        private readonly float epsilon = 0.0001f;

        private float newDistance;

        public PlayerCameraZoom(CinemachineVirtualCamera camera, CameraZoomData data)
        {
            this.data = data;

            framingTransposer = camera.GetCinemachineComponent<CinemachineFramingTransposer>();
            inputProvider = camera.gameObject.GetComponent<CinemachineInputProvider>();
            newDistance = data.BaseDistance;
        }

        public void Update()
        {
            float zoomValue = inputProvider.GetAxisValue(2) * data.ZoomSensitivity;
            newDistance = Mathf.Clamp(newDistance + zoomValue, data.MinDistance, data.MaxDistance);

            float currentDistance = framingTransposer.m_CameraDistance;
            if (Math.Abs(newDistance - currentDistance) < epsilon)
                return;

            float result = Mathf.Lerp(currentDistance, newDistance, data.Smoothing * Time.deltaTime);
            framingTransposer.m_CameraDistance = result;
        }
    }
}
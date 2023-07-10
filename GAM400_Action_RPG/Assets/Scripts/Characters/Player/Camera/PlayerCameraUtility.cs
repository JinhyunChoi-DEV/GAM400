using System;
using Cinemachine;
using UnityEngine;

namespace BattleZZang
{
    [Serializable]
    public class PlayerCameraUtility
    {
        [field:SerializeField] public CinemachineVirtualCamera Camera { get; private set; }
        [field: SerializeField] public float DefaultHorizontalWaitTime { get; private set; } = 0.0f;
        [field: SerializeField] public float DefaultHorizontalRecenteringTime { get; private set; } = 4.0f;

        private CinemachinePOV POV;

        public void Initialize()
        {
            POV = Camera.GetCinemachineComponent<CinemachinePOV>();
        }

        public void EnableRecentering(float waitTime = -1.0f, float recenteringTime = -1.0f, float baseMoveSpeed = 1.0f, float moveSpeed = 1.0f)
        {
            POV.m_HorizontalRecentering.m_enabled = true;
            POV.m_HorizontalRecentering.CancelRecentering();

            if (Math.Abs(waitTime - (-1.0f)) < MathVariables.epsilon)
                waitTime = DefaultHorizontalWaitTime;
            
            if (Math.Abs(recenteringTime - (-1.0f)) < MathVariables.epsilon)
                recenteringTime = DefaultHorizontalRecenteringTime;

            recenteringTime = recenteringTime * baseMoveSpeed / moveSpeed;

            POV.m_HorizontalRecentering.m_WaitTime = waitTime;
            POV.m_HorizontalRecentering.m_RecenteringTime= recenteringTime;
        }

        public void DisableRecentering()
        {
            POV.m_HorizontalRecentering.m_enabled = false;
        }
    }
}

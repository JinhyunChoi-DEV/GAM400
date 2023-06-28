using Cinemachine;

namespace BattleZZang
{
    public class PlayerCameraSetting
    {
        private readonly CinemachinePOV pov;
        private readonly CameraSettingData data;

        public PlayerCameraSetting(CinemachineVirtualCamera camera, CameraSettingData data)
        {
            this.data = data;

            camera.m_Lens.FieldOfView = data.FOV;

            pov = camera.GetCinemachineComponent<CinemachinePOV>();
            pov.m_VerticalAxis.m_MinValue = data.VerticalMinRange;
            pov.m_VerticalAxis.m_MaxValue= data.VerticalMaxRange;
            pov.m_VerticalAxis.m_SpeedMode = AxisState.SpeedMode.InputValueGain;
            pov.m_VerticalAxis.m_MaxSpeed = data.VerticalSpeed;
            pov.m_VerticalAxis.m_AccelTime = data.VerticalAccelTime;
            pov.m_VerticalAxis.m_DecelTime= data.VerticalDecelTime;

            pov.m_HorizontalAxis.m_MinValue = data.HorizontalMinRange;
            pov.m_HorizontalAxis.m_MaxValue = data.HorizontalMaxRange;
            pov.m_HorizontalAxis.m_SpeedMode = AxisState.SpeedMode.InputValueGain;
            pov.m_HorizontalAxis.m_MaxSpeed = data.HorizontalSpeed;
            pov.m_HorizontalAxis.m_AccelTime = data.HorizontalAccelTime;
            pov.m_HorizontalAxis.m_DecelTime = data.HorizontalDecelTime;
        }
    }
}
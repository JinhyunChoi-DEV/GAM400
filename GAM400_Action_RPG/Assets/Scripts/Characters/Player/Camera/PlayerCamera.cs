
using Cinemachine;
using UnityEngine;

namespace BattleZZang
{
    // Required List
    // Body: Framing Transposer
    // Aim : POV
    public class PlayerCamera : MonoBehaviour
    {
        [SerializeField] private Transform cameraTarget;
        [SerializeField] private CinemachineVirtualCamera camera;
        [SerializeField] private CameraSettingData settingData;
        [SerializeField] private CameraZoomData zoomData;

        private PlayerCameraSetting setting;
        private PlayerCameraZoom zoom;

        void Awake()
        {
            setting = new PlayerCameraSetting(camera, settingData);
            zoom = new PlayerCameraZoom(camera, zoomData);

            camera.LookAt = cameraTarget;
            camera.Follow = cameraTarget;
        }
        
        void Start()
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }

        // Update is called once per frame
        void Update()
        {
            zoom.Update();
        }
    }
}

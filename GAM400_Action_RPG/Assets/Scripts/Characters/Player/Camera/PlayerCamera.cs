
using Cinemachine;
using UnityEngine;

namespace BattleZZang
{
    // Required List
    // Body: Framing Transposer
    // Aim : POV
    public class PlayerCamera : MonoBehaviour
    {
        public Transform CameraTransform { get; private set; }

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
            CameraTransform = Camera.main.transform;
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

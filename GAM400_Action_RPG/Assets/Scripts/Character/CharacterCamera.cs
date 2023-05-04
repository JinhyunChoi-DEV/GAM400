using Cinemachine;
using UnityEngine;
using Cursor = UnityEngine.Cursor;

[RequireComponent(typeof(CinemachineVirtualCamera))]
public class CharacterCamera : MonoBehaviour
{
    [SerializeField] private Transform cameraTarget;
    [SerializeField] private Vector2 mouseXLimitAngle = new Vector2(-90, 90);
    [SerializeField] private Vector2 mouseRotationSpeed = new Vector2(5, 2);

    CinemachineVirtualCamera vCamera;

    private float rotateX = 0.0f;
    private float rotateY = 0.0f;
    private float lerpSpeed = 5.0f;

    void Awake()
    {
        vCamera = GetComponent<CinemachineVirtualCamera>();

        vCamera.Follow = cameraTarget;
        vCamera.m_StandbyUpdate = CinemachineVirtualCameraBase.StandbyUpdateMode.RoundRobin;
        vCamera.m_Lens.FieldOfView = 90.0f;

        var bodySetting = vCamera.GetCinemachineComponent<Cinemachine3rdPersonFollow>();
        bodySetting.ShoulderOffset = Vector3.zero;
        bodySetting.CameraDistance = 2.5f;
        bodySetting.IgnoreTag = "Player";
    }

    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }


    void FixedUpdate()
    {
        float axisX = Input.GetAxis("Mouse X");
        float axisY = Input.GetAxis("Mouse Y");

        // prevent tiny y axis movement
        if (Mathf.Abs(axisY) >= 0.2f)
        {
            rotateX -= axisY * mouseRotationSpeed.y;
        }
        rotateX = Mathf.Clamp(rotateX, mouseXLimitAngle.x, mouseXLimitAngle.y);
        rotateY += axisX * mouseRotationSpeed.x;

        Quaternion xRotation = Quaternion.Euler(rotateX, 0, 0);
        Quaternion yRotation = Quaternion.Euler(0, rotateY, 0);
        Quaternion newRotation = yRotation * xRotation;

        cameraTarget.rotation = Quaternion.Lerp(cameraTarget.rotation, newRotation, Time.deltaTime * lerpSpeed);
    }
}

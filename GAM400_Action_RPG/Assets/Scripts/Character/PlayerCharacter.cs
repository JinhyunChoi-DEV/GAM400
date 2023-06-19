using Unity.VisualScripting;
using UnityEngine;

public class PlayerCharacter : MonoBehaviour
{
    [SerializeField] private CharacterData data;
    [SerializeField] private PlayerCharacterPhysics physics;
    [SerializeField] private CharacterCamera camera;

    public CameraLookInfo Look => camera.GetLook();

    private CharacterMoveStateMachine moveStateMachine;

    void Start()
    {
        moveStateMachine = new CharacterMoveStateMachine(data, physics, this);
    }

    void Update()
    {
        moveStateMachine.Update();
    }

    void FixedUpdate()
    {
        moveStateMachine.FixedUpdate();
    }
}

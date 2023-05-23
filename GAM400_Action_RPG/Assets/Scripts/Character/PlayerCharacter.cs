using Unity.VisualScripting;
using UnityEngine;

public class PlayerCharacter : MonoBehaviour
{
    [SerializeField] private CharacterData data;
    [SerializeField] private PlayerCharacterPhysics physics;

    private CharacterMoveStateMachine moveStateMachine;

    void Start()
    {
        moveStateMachine = new CharacterMoveStateMachine(data, physics);
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

using UnityEngine;

[CreateAssetMenu(fileName = "Character Data", menuName = "Scriptable Object/Character Data")]
public class CharacterData : ScriptableObject
{
    [Header("Walk")]
    [SerializeField] private float walkSpeed;
    public float WalkSpeed { get { return walkSpeed; } }

    [Header("Sprint")]
    [SerializeField] private float sprintSpeed;
    public float SprintSpeed { get { return sprintSpeed; } }

    [Header("Gravity")]
    [SerializeField] private float gravity;
    [SerializeField] private float gravityMultiplier;
    [SerializeField] private float gravityTimer;
    public float Gravity { get { return gravity; } }
    public float GravityMultiplier { get { return gravityMultiplier; } }
    public float GravityTimer { get { return gravityTimer; } }

    [Header("Jump")]
    [SerializeField] private float jumpPower;
    public float JumpPower { get { return jumpPower; } }


    [Header("Slope")]
    [SerializeField] private float maxSlope;
    public float MaxSlope { get { return maxSlope; } }
}

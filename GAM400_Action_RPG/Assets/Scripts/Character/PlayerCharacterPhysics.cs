using UnityEngine;

public struct MoveDirection
{
    public Vector3 Forward;
    public Vector3 Right;
}

public class PlayerCharacterPhysics : MonoBehaviour
{
    [SerializeField] private Rigidbody rigidbody;
    [SerializeField] private CapsuleCollider collider;
    [SerializeField] private CharacterData data;
    [SerializeField] private float groundCheckOffset = 0.01f;
    public bool IsGround { get; private set; }
    public Vector3 Velocity { get; private set; }

    private Vector3 origin;
    private float radius;

    public MoveDirection GetDirection()
    {
        //TODO: FIX 일단 테스트용
        MoveDirection result;
        result.Forward = collider.gameObject.transform.forward;
        result.Right = collider.gameObject.transform.right;

        return result;
    }

    public void ApplyVelocity(Vector3 vel)
    {
        Velocity = vel;
    }

    void Start()
    {
        radius = collider.radius;
    }

    void Update()
    {
        origin = collider.gameObject.transform.position + collider.center;
    }

    void FixedUpdate()
    {
        IsGround = CheckGround();

        rigidbody.velocity = Velocity;
    }

    bool CheckGround()
    {
        float maxDistance = collider.bounds.extents.y - radius + groundCheckOffset;
        return Physics.SphereCast(origin, radius, Vector3.down, out var hit, maxDistance);
    }
}
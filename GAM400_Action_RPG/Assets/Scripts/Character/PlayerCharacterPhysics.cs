using System.Collections.Specialized;
using Cinemachine.Utility;
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
    [SerializeField] private PlayerCharacter character;
    [SerializeField] private float groundCheckOffset = 0.01f;
    public bool IsGround { get; private set; }
    public Vector3 Velocity { get; private set; }

    public RaycastHit Hit { get; private set; }

    private Vector3 origin;
    private float radius;

    public void ApplyVelocity(Vector3 vel)
    {
        //TODO:
        if (vel.y > 0.0f)
        {
            Velocity = vel;
            Debug.Log("Real KK");
        }
        else
        {
            var speed = vel.magnitude;
            var dir = Vector3.ProjectOnPlane(vel.normalized, Hit.normal);
            Velocity = dir.normalized * speed;
        }
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
        var result = Physics.SphereCast(origin, radius, Vector3.down, out var hit, maxDistance);
        Hit = hit;

        return result;
    }
}
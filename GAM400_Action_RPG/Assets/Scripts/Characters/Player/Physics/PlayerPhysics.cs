using UnityEngine;

namespace BattleZZang
{
    [RequireComponent(typeof(CapsuleCollider), typeof(Rigidbody))]
    public class PlayerPhysics : MonoBehaviour
    {
        public PlayerPhysicsShareData PhysicsShareData { get; private set; }

        [SerializeField] private Rigidbody rigidBody;

        [field: SerializeField] public PlayerSO Data { get; private set; }

        [field:Header("Collisions")]
        [field: SerializeField] public PlayerCollider Collider { get; private set; }

        private PlayerGroundedData moveData => Data.GroundedData;

        public Quaternion GetRotation()
        {
            return rigidBody.rotation;
        }

        public void ApplyForce(Vector3 dir, float speed)
        {
            var newForce = dir * speed;
            var horizontalSpeed = rigidBody.velocity;
            horizontalSpeed.y = 0.0f;

            rigidBody.AddForce(newForce - horizontalSpeed, ForceMode.VelocityChange);
        }

        public void ApplyRotation(Quaternion rotation)
        {
            rigidBody.MoveRotation(rotation);
        }

        public void ResetVelocity()
        {
            rigidBody.velocity = Vector3.zero;
        }

        private void Awake()
        {
            PhysicsShareData = new PlayerPhysicsShareData();

            Collider.Initialize(gameObject);
            Collider.CalculateCapsuleColliderDimension();
        }

        private void OnValidate()
        {
            Collider.Initialize(gameObject);
            Collider.CalculateCapsuleColliderDimension();
        }

        private void FixedUpdate()
        {
            Vector3 colliderCenter = Collider.CapsuleColliderData.Collider.bounds.center;
            var radius = Collider.CapsuleColliderData.Collider.radius / 2.0f - Collider.DefaultColliderData.RayOffset;
            
            if(Physics.SphereCast(colliderCenter, radius, Vector3.down, out var hit, Collider.SlopeData.FloatDistance))
            {
                float angle = Vector3.Angle(hit.normal, Vector3.up);
                SpeedModiferByAngle(angle);

                if (PhysicsShareData.SlopeSpeedModifiers == 0)
                    return;

                AdjustSlopeSpeedByDirection(angle, hit);

                // if you want to adjust the player object scale, then multiply local scale.y
                float distanceFloatingPoint = Collider.CapsuleColliderData.ColliderCenterInLocalSpace.y - hit.distance;

                if (distanceFloatingPoint == 0.0f)
                    return;

                float amountToLift = distanceFloatingPoint * Collider.SlopeData.StepReachForce - rigidBody.velocity.y;
                Vector3 liftForce = new Vector3(0, amountToLift, 0);
                rigidBody.AddForce(liftForce, ForceMode.VelocityChange);
            }
        }

        private void SpeedModiferByAngle(float angle)
        {
            float slopeSpeedModifier = moveData.SlopeDecreaseSpeedByAngles.Evaluate(angle);
            PhysicsShareData.SlopeSpeedModifiers = slopeSpeedModifier;
            Debug.Log(slopeSpeedModifier);
        }

        private void AdjustSlopeSpeedByDirection(float angle, RaycastHit hit)
        {
            var moveDir = Vector3.ProjectOnPlane(rigidBody.velocity, hit.normal);

            // Since we are going uphill, use it as it is
            if (moveDir.y > 1.0f)
                return;

            PhysicsShareData.SlopeSpeedModifiers = 1.0f + moveData.SlopeIncreaseSpeedByAngles.Evaluate(angle);
        }
    }
}

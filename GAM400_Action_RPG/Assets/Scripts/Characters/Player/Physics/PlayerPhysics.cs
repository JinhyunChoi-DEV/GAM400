using System;
using UnityEngine;

namespace BattleZZang
{
    public struct GroundRayResult
    {
        public bool IsCasted;
        public Vector3 Direction;
        public RaycastHit Hit;
        public float GroundAngle;
    }

    [RequireComponent(typeof(CapsuleCollider), typeof(Rigidbody))]
    public class PlayerPhysics : MonoBehaviour
    {
        public PlayerPhysicsShareData PhysicsShareData { get; private set; }

        public Rigidbody RigidBody { get; private set; }

        [field: SerializeField] public PlayerSO Data { get; private set; }

        [field:Header("Collisions")]
        [field: SerializeField] public PlayerCapsuleColliderUtility Collider { get; private set; }
        [field: SerializeField] public PlayerLayerData LayerData { get; private set; }
        [field: SerializeField] public PlayerFeetIKData FeetIKData { get; private set;}
        [SerializeField] private Player player;

        private PlayerGroundedData moveData => Data.GroundedData;

        public void UpdateFloating(ref bool needRecentering)
        {
            var center = Collider.CapsuleColliderUtility.CapsuleColliderData.Collider.bounds.center;
            var result = GetRayResult(center ,Vector3.down, Collider.CapsuleColliderUtility.SlopeData.FloatDistance);

            if (result.IsCasted)
            {
                float angle = result.GroundAngle;
                SpeedModiferByAngle(angle, ref needRecentering);

                if (PhysicsShareData.SlopeSpeedModifiers == 0)
                    return;
                
                AdjustSlopeSpeedByDirection(angle);

                // if you want to adjust the player object scale, then multiply local scale.y
                float distanceFloatingPoint = Collider.CapsuleColliderUtility.CapsuleColliderData.ColliderCenterInLocalSpace.y - result.Hit.distance;

                if (distanceFloatingPoint == 0.0f)
                    return;

                float amountToLift = distanceFloatingPoint * Collider.CapsuleColliderUtility.SlopeData.StepReachForce - RigidBody.velocity.y;
                Vector3 liftForce = new Vector3(0, amountToLift, 0);
                RigidBody.AddForce(liftForce, ForceMode.VelocityChange);
            }
        }

        public bool IsGroundLayer(LayerMask layer)
        {
            return LayerData.IsGroundLayer(layer);
        }

        public bool IsGroundUnderneath()
        {
            BoxCollider groundChecker = Collider.TriggerColliderData.GroundCheckCollider;
            Vector3 groundColliderCenter = groundChecker.bounds.center;

            Collider[] overlappedColliders = Physics.OverlapBox(groundColliderCenter, Collider.TriggerColliderData.GroundCheckColliderExtents, groundChecker.transform.rotation, LayerData.GroundLayer, QueryTriggerInteraction.Ignore);

            return overlappedColliders.Length > 0;
        }

        public GroundRayResult GetRayResult(Vector3 center, Vector3 dir, float maxDistance)
        {
            GroundRayResult result;
            result.IsCasted = false;
            result.Direction = dir;
            result.GroundAngle = 0.0f;

            var ray = new Ray(center, dir);
            result.IsCasted = Physics.Raycast(ray, out result.Hit, maxDistance, LayerData.GroundLayer, QueryTriggerInteraction.Ignore);

            if (!result.IsCasted)
                return result;

            result.GroundAngle = Vector3.Angle(result.Hit.normal, -dir);
            return result;
        }

        public bool IsMovingUp(float minVelocity = 0.1f)
        {
            return RigidBody.velocity.y > minVelocity;
        }

        public bool IsMovingDown(float minVelocity = 0.1f)
        {
            return RigidBody.velocity.y < -minVelocity;
        }

        private void Awake()
        {
            RigidBody = GetComponent<Rigidbody>();
            PhysicsShareData = new PlayerPhysicsShareData();

            Collider.CapsuleColliderUtility.Initialize(gameObject);
            Collider.CapsuleColliderUtility.CalculateCapsuleColliderDimension();
        }

        private void OnTriggerEnter(Collider collider)
        {
            player.MoveStateMachine.OnTriggerEnter(collider);
        }

        private void OnTriggerExit(Collider collider)
        {
            player.MoveStateMachine.OnTriggerExit(collider);
        }

        private void OnValidate()
        {
            Collider.CapsuleColliderUtility.Initialize(gameObject);
            Collider.CapsuleColliderUtility.CalculateCapsuleColliderDimension();
        }

        private void SpeedModiferByAngle(float angle, ref bool needRecentering)
        {
            float slopeSpeedModifier = moveData.SlopeDecreaseSpeedByAngles.Evaluate(angle);
            if (Math.Abs(PhysicsShareData.SlopeSpeedModifiers - slopeSpeedModifier) > MathVariables.epsilon)
            {
                PhysicsShareData.SlopeSpeedModifiers = slopeSpeedModifier;
                needRecentering = true;
            }
        }

        private void AdjustSlopeSpeedByDirection(float angle)
        {
            // Since we are going uphill, use it as it is
            if (IsMovingUp())
                return;

            PhysicsShareData.SlopeSpeedModifiers = 1.0f + moveData.SlopeIncreaseSpeedByAngles.Evaluate(angle);
        }
    }
}

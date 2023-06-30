using UnityEngine;

namespace BattleZZang
{
    public class PlayerPhysics : MonoBehaviour
    {
        [SerializeField] private Rigidbody rigidBody;

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
    }
}

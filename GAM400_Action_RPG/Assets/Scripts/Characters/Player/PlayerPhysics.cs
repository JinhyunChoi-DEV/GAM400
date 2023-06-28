using UnityEngine;

namespace BattleZZang
{
    public class PlayerPhysics : MonoBehaviour
    {
        [SerializeField] private Rigidbody rigidBody;

        public void ApplyForce(Vector3 dir, float speed)
        {
            var newForce = dir * speed;
            var horizontalSpeed = rigidBody.velocity;
            horizontalSpeed.y = 0.0f;

            rigidBody.AddForce(newForce - horizontalSpeed, ForceMode.VelocityChange);
        }
    }
}

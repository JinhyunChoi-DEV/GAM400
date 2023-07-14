using UnityEngine;

namespace BattleZZang
{
    public class PlayerFeetGrounder : MonoBehaviour
    {
        [Header("Feet Grounder")]
        public bool EnableFeetIK = true;
        [SerializeField] Animator animator;
        [SerializeField] [Range(0f, 2f)] private float heightFormGroundRaycast = 1.14f;
        [SerializeField] [Range(0f, 2f)] private float raycastDistance = 1.5f;
        [SerializeField] private LayerMask environmentLayer;    // TODO: 플레이어 데이터껄로 가져오기
        [SerializeField] private float pelvisOffset = 0f;
        [SerializeField] [Range(0f, 1f)] private float pelvisUpAndDownSpeed = 0.28f;
        [SerializeField] [Range(0f, 1f)] private float feetToIKPositionSpeed = 0.5f;

        public string LeftFootAnimVariableName = "LeftFootCurve";
        public string RightFootAnimVariableName = "RightFootCurve";

        public bool UseProIKFeature = false;
        public bool ShowDebug = true;

        private bool isActive => EnableFeetIK && (animator != null);
        private Vector3 rightFootPosition, leftFootPosition, rightFootIKPosition, leftFootIKPosition;
        private Quaternion leftFootIKRotation, rightFootIKRotation;
        private float lastPelvisPositionY, lastRightFootPositionY, lastLeftFootPositionY;

        private void FixedUpdate()
        {
            if (isActive == false)
                return;

            AdjustFeetTarget(ref rightFootPosition, HumanBodyBones.RightFoot);
            AdjustFeetTarget(ref leftFootPosition, HumanBodyBones.LeftFoot);

            //find and raycast to the ground to find position
            FeetPositionHandle(rightFootPosition, ref rightFootIKPosition, ref rightFootIKRotation);
            FeetPositionHandle(leftFootPosition, ref leftFootIKPosition, ref leftFootIKRotation);
        }

        private void OnAnimatorIK(int layerIndex)
        {
            if (isActive == false)
                return;

            MovePelvisHeight();
            animator.SetIKPositionWeight(AvatarIKGoal.RightFoot, 1);
            animator.SetIKPositionWeight(AvatarIKGoal.LeftFoot, 1);

            if (UseProIKFeature)
            {
                animator.SetIKRotationWeight(AvatarIKGoal.RightFoot, animator.GetFloat(RightFootAnimVariableName));
                animator.SetIKRotationWeight(AvatarIKGoal.LeftFoot, animator.GetFloat(LeftFootAnimVariableName));
            }

            MoveFeetToIKPoint(AvatarIKGoal.RightFoot, rightFootIKPosition, rightFootIKRotation, ref lastRightFootPositionY);
            MoveFeetToIKPoint(AvatarIKGoal.LeftFoot, leftFootIKPosition, leftFootIKRotation, ref lastLeftFootPositionY);
        }

        private void MoveFeetToIKPoint(AvatarIKGoal foot, Vector3 positionIKHolder, Quaternion rotationIKHolder, ref float lastFootPositionY)
        {
            Vector3 targetIKPosition = animator.GetIKPosition(foot);

            if (positionIKHolder != Vector3.zero)
            {
                targetIKPosition = transform.InverseTransformPoint(targetIKPosition);
                positionIKHolder = transform.InverseTransformPoint(positionIKHolder);

                float newYPosition = Mathf.Lerp(lastFootPositionY, positionIKHolder.y, feetToIKPositionSpeed);
                targetIKPosition.y += newYPosition;
                lastFootPositionY = newYPosition;
                targetIKPosition = transform.TransformPoint(targetIKPosition);
                animator.SetIKRotation(foot, rotationIKHolder);
            }

            animator.SetIKPosition(foot, targetIKPosition);
        }

        private void MovePelvisHeight()
        {
            if (rightFootIKPosition == Vector3.zero || leftFootIKPosition == Vector3.zero ||
                lastPelvisPositionY == 0.0f)
            {
                lastPelvisPositionY = animator.bodyPosition.y;
                return;
            }

            float leftOffsetPosition = leftFootIKPosition.y - transform.position.y;
            float rightOffsetPosition = rightFootIKPosition.y - transform.position.y;
            float totalOffset = (leftOffsetPosition < rightOffsetPosition) ? leftOffsetPosition : rightOffsetPosition;

            Vector3 newPelvisPosition = animator.bodyPosition + Vector3.up * totalOffset;

            newPelvisPosition.y = Mathf.Lerp(lastPelvisPositionY, newPelvisPosition.y, pelvisUpAndDownSpeed);
            animator.bodyPosition = newPelvisPosition;
            lastPelvisPositionY = animator.bodyPosition.y;
        }

        private void FeetPositionHandle(Vector3 fromSkyPosition, ref Vector3 feetIKPosition, ref Quaternion feetIKRotation)
        {
            RaycastHit feetHit;

            if (ShowDebug)
                Debug.DrawLine(fromSkyPosition, fromSkyPosition + Vector3.down * (raycastDistance + heightFormGroundRaycast), Color.red);

            if (Physics.Raycast(fromSkyPosition, Vector3.down, out feetHit, raycastDistance + heightFormGroundRaycast, environmentLayer))
            {
                feetIKPosition = fromSkyPosition;
                feetIKPosition.y = feetHit.point.y + pelvisOffset;
                feetIKRotation = Quaternion.FromToRotation(Vector3.up, feetHit.normal) * transform.rotation;

                return;
            }

            feetIKPosition = Vector3.zero;
        }

        private void AdjustFeetTarget(ref Vector3 feetPosition, HumanBodyBones foot)
        {
            feetPosition = animator.GetBoneTransform(foot).position;
            feetPosition.y = transform.position.y + heightFormGroundRaycast;
        }
    }
}

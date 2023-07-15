using UnityEngine;

namespace BattleZZang
{
    public class PlayerFeetGrounder
    {
        private PlayerLayerData layerData;
        private Animator animator;
        private Transform transform;
        private PlayerFeetIKData ikData;

        private bool isActive => ikData.EnableFeetIK && (animator != null);
        private Vector3 rightFootPosition, leftFootPosition, rightFootIKPosition, leftFootIKPosition;
        private Quaternion leftFootIKRotation, rightFootIKRotation;
        private float lastPelvisPositionY, lastRightFootPositionY, lastLeftFootPositionY;

        public PlayerFeetGrounder(Player player)
        {
            animator = player.Animator;
            layerData = player.Physics.LayerData;
            transform = player.transform;
            ikData = player.Physics.FeetIKData;
        }

        public void UpdateFeetPosition()
        {
            if (isActive == false)
                return;

            AdjustFeetTarget(ref rightFootPosition, HumanBodyBones.RightFoot);
            AdjustFeetTarget(ref leftFootPosition, HumanBodyBones.LeftFoot);

            var rightFootTransform = animator.GetBoneTransform(HumanBodyBones.RightFoot).transform;
            var leftFootTransform = animator.GetBoneTransform(HumanBodyBones.LeftFoot).transform;
            FeetPositionHandle(rightFootPosition, ref rightFootIKPosition, ref rightFootIKRotation, rightFootTransform);
            FeetPositionHandle(leftFootPosition, ref leftFootIKPosition, ref leftFootIKRotation, leftFootTransform);
        }

        public void OnAnimatorIK(int layerIndex)
        {
            if (isActive == false)
                return;

            MovePelvisHeight();
            animator.SetIKPositionWeight(AvatarIKGoal.RightFoot, 1);
            animator.SetIKPositionWeight(AvatarIKGoal.LeftFoot, 1);

            if (ikData.UseProIKFeature)
            {
                animator.SetIKRotationWeight(AvatarIKGoal.RightFoot, animator.GetFloat(ikData .RightFootAnimVariableName));
                animator.SetIKRotationWeight(AvatarIKGoal.LeftFoot, animator.GetFloat(ikData.LeftFootAnimVariableName));
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

                float newYPosition = Mathf.Lerp(lastFootPositionY, positionIKHolder.y, ikData.FeetToIKPositionSpeed);
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

            newPelvisPosition.y = Mathf.Lerp(lastPelvisPositionY, newPelvisPosition.y, ikData.PelvisUpAndDownSpeed);
            animator.bodyPosition = newPelvisPosition;
            lastPelvisPositionY = animator.bodyPosition.y;
        }

        private void FeetPositionHandle(Vector3 fromSkyPosition, ref Vector3 feetIKPosition, ref Quaternion feetIKRotation, Transform footTransform)
        {
            RaycastHit feetHit;

            if (ikData.ShowDebug)
                Debug.DrawLine(fromSkyPosition, fromSkyPosition + Vector3.down * (ikData.RaycastDistance + ikData.HeightFormGroundRaycast), Color.red);

            if (Physics.Raycast(fromSkyPosition, Vector3.down, out feetHit, ikData.RaycastDistance + ikData.HeightFormGroundRaycast, layerData.GroundLayer))
            {
                feetIKPosition = fromSkyPosition;
                feetIKPosition.y = feetHit.point.y + ikData.PelvisOffset;
                
                Quaternion rp = Quaternion.LookRotation(footTransform.parent.forward, footTransform.parent.up);
                Vector3 footRot = new Vector3(0.0f, Quaternion.Inverse(rp).eulerAngles.y, 0.0f);
                feetIKRotation = Quaternion.FromToRotation(Vector3.up, feetHit.normal) * Quaternion.Euler(footRot);

                return;
            }
            
            feetIKPosition = Vector3.zero;
        }

        private void AdjustFeetTarget(ref Vector3 feetPosition, HumanBodyBones foot)
        {
            feetPosition = animator.GetBoneTransform(foot).position;
            feetPosition.y = transform.position.y + ikData.HeightFormGroundRaycast;
        }
    }
}

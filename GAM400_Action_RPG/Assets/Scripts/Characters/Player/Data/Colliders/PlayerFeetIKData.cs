using System;
using UnityEngine;

namespace BattleZZang
{
    [Serializable]
    public class PlayerFeetIKData
    {
        [field: Header("Debug Field")]
        [field:SerializeField] public bool EnableFeetIK { get; private set; } = true;
        [field: SerializeField] public bool UseProIKFeature { get; private set; }= false;
        [field: SerializeField] public bool ShowDebug { get; private set; } = true;

        [field: Header("Setting Value Field")]
        [field: SerializeField] [field: Range(0f, 2f)] public float HeightFormGroundRaycast { get; private set; } = 1.14f;
        [field: SerializeField][field: Range(0f, 2f)] public float RaycastDistance { get; private set; }= 1.5f;
        [field: SerializeField][field: Range(0f, 1f)] public float PelvisUpAndDownSpeed { get; private set; }= 0.28f;
        [field: SerializeField][field: Range(0f, 1f)] public float FeetToIKPositionSpeed { get; private set; }= 0.5f;
        [field: SerializeField] public float PelvisOffset { get; private set; }= 0f;

        [field: Header("ETC")]
        [field: SerializeField] public string LeftFootAnimVariableName { get; private set; } = "LeftFootCurve";
        [field: SerializeField] public string RightFootAnimVariableName { get; private set; } = "RightFootCurve";

    }
}

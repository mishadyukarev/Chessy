using Assets.Scripts.Abstractions.Enums;
using UnityEngine;

public struct RaycastHit2DComponent
{
    private RaycastHit2D _raycastHit2D;
    private RaycastGettedTypes _raycastGettedType;


    internal RaycastHit2D RaycastHit2D => _raycastHit2D;
    internal bool IsGettedAnything => _raycastGettedType != default;
    internal RaycastGettedTypes RaycastGettedType => _raycastGettedType;

    internal void StartFill()
    {
        _raycastHit2D = default;
        _raycastGettedType = default;
    }
    internal void SetRaycastHit2D(RaycastHit2D raycastHit2D) => _raycastHit2D = raycastHit2D;
    internal void SetGettedType(RaycastGettedTypes raycastGettedType) => _raycastGettedType = raycastGettedType;
    internal void ResetGettedType() => _raycastGettedType = default;
    internal bool IsGettedType(RaycastGettedTypes raycastGettedTypes) => _raycastGettedType == raycastGettedTypes;
}
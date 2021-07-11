using UnityEngine;

public struct RaycastHit2DComponent
{
    private RaycastHit2D _raycastHit2D;
    private bool _isUI;

    internal RaycastHit2D RaycastHit2D => _raycastHit2D;
    internal bool IsUI => _isUI;

    internal void StartFill()
    {
        _raycastHit2D = default;
        _isUI = default;
    }
    internal void SetRaycastHit2D(RaycastHit2D raycastHit2D) => _raycastHit2D = raycastHit2D;
    internal void SetIsUI(bool isUI) => _isUI = isUI;
}
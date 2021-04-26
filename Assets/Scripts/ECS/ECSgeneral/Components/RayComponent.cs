using UnityEngine;

public struct RayComponent
{
    private RaycastHit2D _raycastHit2D;

    internal RaycastHit2D RaycastHit2D
    {
        get { return _raycastHit2D; }
        set { _raycastHit2D = value; }
    }
}
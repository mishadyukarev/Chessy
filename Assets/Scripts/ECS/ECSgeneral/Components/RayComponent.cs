using UnityEngine;

public struct RayComponent
{
    private SystemsGeneralManager _systemsGeneralManager;
    private RaycastHit2D _raycastHit2D;



    public RayComponent(SystemsGeneralManager systemsGeneralManager)
    {
        _systemsGeneralManager = systemsGeneralManager;
        _raycastHit2D = default;
    }



    public bool TryGetRaycastHit2D(out RaycastHit2D raycastHit2D)
    {
        _systemsGeneralManager.InvokeRunSystem(SystemGeneralTypes.Else, nameof(RaySystem));

        raycastHit2D = _raycastHit2D;

        if (raycastHit2D) return true;
        else return false;
    }

    internal void Pack(RaycastHit2D raycastHit2D)
    {
        _raycastHit2D = raycastHit2D;
    }
}

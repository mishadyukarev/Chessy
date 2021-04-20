
using UnityEngine;

public struct GetterCellComponent
{
    private RaycastHit2D _raycastHit2dIN;

    private int[] _xyCurrentCellOUT;
    private bool _isReceivedOUT;

    private SystemsGeneralManager _systemsGeneralManager;


    public GetterCellComponent(StartValuesConfig nameValueManager, SystemsGeneralManager systemsGeneralManager)
    {
        _raycastHit2dIN = default;
        _xyCurrentCellOUT = new int[nameValueManager.XY_FOR_ARRAY];
        _isReceivedOUT = default;


        _systemsGeneralManager = systemsGeneralManager;

    }


    public RaycastHit2D RaycastHit2dIN => _raycastHit2dIN;

    public int[] XYcurrentCellOUT => _xyCurrentCellOUT;
    public bool IsReceivedOUT => _isReceivedOUT;

    public bool SetIsReceiveOUT(bool isActive) => _isReceivedOUT = isActive;


    public bool TryGetXYCurrentCell(RaycastHit2D raycastHit2D, out int[] xyCurrentCell)
    {
        _raycastHit2dIN = raycastHit2D;

        _systemsGeneralManager.InvokeRunSystem(SystemGeneralTypes.Else, nameof(GetterCellSystem));

        xyCurrentCell = _xyCurrentCellOUT;
        return _isReceivedOUT;
    }
}

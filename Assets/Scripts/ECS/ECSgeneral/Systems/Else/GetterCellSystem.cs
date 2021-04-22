using Leopotam.Ecs;
using UnityEngine;


public struct GetterCellComponent
{
    private RaycastHit2D _raycastHit2dIN;

    private int[] _xyCurrentCellOUT;
    private bool _isReceivedOUT;

    private SystemsGeneralManager _systemsGeneralManager;

    public GetterCellComponent(StartValuesGameConfig nameValueManager, SystemsGeneralManager systemsGeneralManager)
    {
        _raycastHit2dIN = default;
        _xyCurrentCellOUT = new int[nameValueManager.XY_FOR_ARRAY];
        _isReceivedOUT = default;

        _systemsGeneralManager = systemsGeneralManager;
    }


    public bool TryGetXYCurrentCell(RaycastHit2D raycastHit2D, out int[] xyCurrentCell)
    {
        _raycastHit2dIN = raycastHit2D;

        _systemsGeneralManager.InvokeRunSystem(SystemGeneralTypes.Else, nameof(GetterCellSystem));

        xyCurrentCell = _xyCurrentCellOUT;
        return _isReceivedOUT;
    }

    internal void Unpack(out RaycastHit2D raycastHit2dIN)
    {
        raycastHit2dIN = _raycastHit2dIN;
    }

    internal void Pack(int[] xyCurrentCellOUT, bool isReceivedOUT)
    {
        _xyCurrentCellOUT = xyCurrentCellOUT;
        _isReceivedOUT = isReceivedOUT;
    }
}



public sealed class GetterCellSystem : CellReduction, IEcsRunSystem
{
    private EcsComponentRef<GetterCellComponent> _getterCellComponentRef = default;

    internal GetterCellSystem(ECSmanager eCSmanager, SupportGameManager supportManager) : base(eCSmanager, supportManager)
    {
        _getterCellComponentRef = eCSmanager.EntitiesGeneralManager.GetterCellComponentRef;
    }

    public void Run()
    {
        _getterCellComponentRef.Unref().Unpack(out RaycastHit2D raycastHit2dIN);

        if (raycastHit2dIN != default)
        {
            for (int x = 0; x < Xcount; x++)
            {
                for (int y = 0; y < Ycount; y++)
                {
                    int one = CellComponent(x, y).InstanceIDcell;
                    int two = raycastHit2dIN.transform.gameObject.GetInstanceID();

                    if (one == two)
                    {
                        _getterCellComponentRef.Unref().Pack(new int[] { x, y }, true);
                        return;
                    }
                }
            }
            _getterCellComponentRef.Unref().Pack(new int[] { -1, -1 }, false);
        }
    }
}

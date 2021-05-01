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

        _systemsGeneralManager.InvokeRunSystem(SystemGeneralTypes.Multiple, nameof(GetterCellSystem));

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
    private EcsComponentRef<RayComponent> _rayComponentRef = default;
    private EcsComponentRef<SelectorComponent> _selectorComponentRef = default;

    internal GetterCellSystem(ECSmanager eCSmanager) : base(eCSmanager)
    {
        _getterCellComponentRef = eCSmanager.EntitiesGeneralManager.GetterCellComponentRef;
        _rayComponentRef = eCSmanager.EntitiesGeneralManager.RayComponentRef;
        _selectorComponentRef = eCSmanager.EntitiesGeneralManager.SelectorComponentRef;
    }

    public void Run()
    {
        if (_rayComponentRef.Unref().RaycastHit2D != default)
        {
            for (int x = 0; x < Xcount; x++)
            {
                for (int y = 0; y < Ycount; y++)
                {
                    int one = CellComponent(x, y).InstanceIDcell;
                    int two = _rayComponentRef.Unref().RaycastHit2D.transform.gameObject.GetInstanceID();

                    if (one == two)
                    {
                        _selectorComponentRef.Unref().XYcurrentCell = new int[] { x, y };
                        _selectorComponentRef.Unref().IsGettedCell = true;
                        //_getterCellComponentRef.Unref().Pack(new int[] { x, y }, true);
                        return;
                    }
                }
            }
            _selectorComponentRef.Unref().XYcurrentCell = new int[] { -1, -1 };
            _selectorComponentRef.Unref().IsGettedCell = false;
            //_getterCellComponentRef.Unref().Pack(new int[] { -1, -1 }, false);
        }
    }
}

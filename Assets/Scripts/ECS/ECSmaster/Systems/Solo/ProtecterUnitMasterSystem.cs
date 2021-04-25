using Leopotam.Ecs;
using Photon.Realtime;

internal struct ProtecterUnitMasterComponent
{
    private CellManager _cellManager;
    private SystemsMasterManager _systemsMasterManager;

    private int[] _xyCellIN;
    private Player _playerIN;

    private bool _isProtectedOUT;

    internal ProtecterUnitMasterComponent(ECSmanager eCSmanager, SupportGameManager supportGameManager)
    {
        _cellManager = supportGameManager.CellManager;
        _systemsMasterManager = eCSmanager.SystemsMasterManager;

        _xyCellIN = new int[supportGameManager.StartValuesGameConfig.XY_FOR_ARRAY];
        _playerIN = default;

        _isProtectedOUT = default;
    }


    internal bool ProtectUnit(int[] xyCellIN, Player playerIN)
    {
        _cellManager.CopyXYinTo(xyCellIN, _xyCellIN);
        _playerIN = playerIN;

        _systemsMasterManager.InvokeRunSystem(SystemMasterTypes.Else, nameof(ProtecterUnitMasterSystem));

        return _isProtectedOUT;
    }

    internal void Unpack(out int[] xyCellIN, out Player playerIN)
    {
        xyCellIN = _xyCellIN;
        playerIN = _playerIN;
    }

    internal void Pack(bool isProtectedOUT)
    {
        _isProtectedOUT = isProtectedOUT;
    }
}



internal class ProtecterUnitMasterSystem : CellReduction, IEcsRunSystem
{
    private EcsComponentRef<ProtecterUnitMasterComponent> _protecterUnitMasterComponentRef = default;

    internal ProtecterUnitMasterSystem(ECSmanager eCSmanager, SupportGameManager supportGameManager) : base(eCSmanager, supportGameManager)
    {
        _protecterUnitMasterComponentRef = eCSmanager.EntitiesMasterManager.ProtecterUnitMasterComponentRef;
    }


    public void Run()
    {
        _protecterUnitMasterComponentRef.Unref().Unpack(out int[] xyCellIN, out Player playerIN);

        if (CellUnitComponent(xyCellIN).HaveMaxSteps)
        {
            CellUnitComponent(xyCellIN).IsProtected = true;
            CellUnitComponent(xyCellIN).IsRelaxed = false;
            CellUnitComponent(xyCellIN).AmountSteps = 0;

            _protecterUnitMasterComponentRef.Unref().Pack(true);
        }

        _protecterUnitMasterComponentRef.Unref().Pack(false);
    }
}
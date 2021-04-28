using Leopotam.Ecs;
using Photon.Realtime;
using static MainGame;

internal struct ProtecterUnitMasterComponent
{
    private CellManager _cellManager;
    private SystemsMasterManager _systemsMasterManager;

    private int[] _xyCellIN;
    private Player _playerIN;

    private bool _isProtectedOUT;

    internal ProtecterUnitMasterComponent(ECSmanager eCSmanager)
    {
        _cellManager = InstanceGame.CellManager;
        _systemsMasterManager = eCSmanager.SystemsMasterManager;

        _xyCellIN = new int[InstanceGame.StartValuesGameConfig.XY_FOR_ARRAY];
        _playerIN = default;

        _isProtectedOUT = default;
    }


    internal bool ProtectUnit(int[] xyCellIN, Player playerIN)
    {
        _cellManager.CopyXYinTo(xyCellIN, _xyCellIN);
        _playerIN = playerIN;

        _systemsMasterManager.InvokeRunSystem(SystemMasterTypes.Multiple, nameof(ProtecterUnitMasterSystem));

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

    internal ProtecterUnitMasterSystem(ECSmanager eCSmanager) : base(eCSmanager)
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
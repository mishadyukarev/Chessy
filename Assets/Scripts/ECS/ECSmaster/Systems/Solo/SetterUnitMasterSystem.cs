using Leopotam.Ecs;
using Photon.Realtime;


public struct SetterUnitMasterComponent
{
    private int[] _xyCellIN;
    private UnitTypes _unitTypeIN;
    private Player _playerIN;

    private bool _isSettedOUT;

    private StartValuesGameConfig _nameValueManager;
    private CellManager _cellManager;
    private SystemsMasterManager _systemsMasterManager;


    public SetterUnitMasterComponent(StartValuesGameConfig nameValueManager, CellManager cellManager, SystemsMasterManager systemsMasterManager)
    {
        _xyCellIN = new int[nameValueManager.XY_FOR_ARRAY];
        _unitTypeIN = default;
        _playerIN = default;

        _isSettedOUT = default;

        _nameValueManager = nameValueManager;
        _cellManager = cellManager;
        _systemsMasterManager = systemsMasterManager;
    }


    public bool TrySetUnit(int[] xyCell, UnitTypes unitType, Player player)
    {
        _cellManager.CopyXYinTo(xyCell, _xyCellIN);
        _unitTypeIN = unitType;
        _playerIN = player;

        _systemsMasterManager.InvokeRunSystem(SystemMasterTypes.Else, nameof(SetterUnitMasterSystem));

        return _isSettedOUT;
    }

    public void GetValues(out int[] xyCell, out UnitTypes unitType, out Player player)
    {
        xyCell = _xyCellIN;
        unitType = _unitTypeIN;
        player = _playerIN;
    }

    public void SetValues(bool isSetted)
    {
        _isSettedOUT = isSetted;
    }
}


public class SetterUnitMasterSystem : CellReduction, IEcsRunSystem
{
    private EcsComponentRef<SetterUnitMasterComponent> _setterUnitMasterComponentRef = default;
    private EcsComponentRef<EconomyMasterComponent.UnitsMasterComponent> _economyUnitMasterComponent = default;

    internal SetterUnitMasterSystem(ECSmanager eCSmanager, SupportGameManager supportManager) : base(eCSmanager, supportManager)
    {
        _setterUnitMasterComponentRef = eCSmanager.EntitiesMasterManager.SetterUnitMasterComponentRef;
        _economyUnitMasterComponent = eCSmanager.EntitiesMasterManager.EconomyUnitsMasterComponentRef;

        _startValues = supportManager.StartValuesGameConfig;
    }


    public void Run()
    {
        _setterUnitMasterComponentRef.Unref().GetValues(out int[] xyCell, out UnitTypes unitType, out Player player);

        switch (unitType)
        {
            case UnitTypes.None:
                break;

            case UnitTypes.King:
                ExecuteSetUnitKing(xyCell, player);
                break;

            case UnitTypes.Pawn:
                ExecuteSetUnitPawn(xyCell, player);
                break;

            default:
                break;
        }
    }

    private void ExecuteSetUnitPawn(int[] xyCell, Player player)
    {
        if (!CellEnvironmentComponent(xyCell).HaveMountain && !CellUnitComponent(xyCell).HaveUnit)
        {
            if (player.IsMasterClient)
            {
                if (CellComponent(xyCell).IsStartMaster)
                {
                    CellUnitComponent(xyCell).SetUnit(UnitTypes.Pawn, _startValues.AMOUNT_HEALTH_PAWN, _startValues.POWER_DAMAGE_PAWN, _startValues.AMOUNT_STEPS_PAWN, false, false, player);
                    _economyUnitMasterComponent.Unref().AmountUnitPawnMaster -= _startValues.AMOUNT_FOR_TAKE_UNIT;
                    _setterUnitMasterComponentRef.Unref().SetValues(true);
                }
            }

            else
            {
                if (CellComponent(xyCell).IsStartOther)
                {
                    CellUnitComponent(xyCell).SetUnit(UnitTypes.Pawn, _startValues.AMOUNT_HEALTH_PAWN, _startValues.POWER_DAMAGE_PAWN, _startValues.AMOUNT_STEPS_PAWN, false, false, player);
                    _economyUnitMasterComponent.Unref().AmountUnitPawnOther -= _startValues.AMOUNT_FOR_TAKE_UNIT;
                    _setterUnitMasterComponentRef.Unref().SetValues(true);
                }
                else _setterUnitMasterComponentRef.Unref().SetValues(false);
            }        
        }
    }

    private void ExecuteSetUnitKing(int[] xyCell, Player player)
    {
        if (!CellEnvironmentComponent(xyCell).HaveMountain && !CellUnitComponent(xyCell).HaveUnit)
        {
            if (player.IsMasterClient)
            {
                if (CellComponent(xyCell).IsStartMaster)
                {
                    CellUnitComponent(xyCell).SetUnit(UnitTypes.King, _startValues.AMOUNT_HEALTH_KING, _startValues.POWER_DAMAGE_KING, _startValues.AMOUNT_STEPS_KING, false, false, player);
                    _economyUnitMasterComponent.Unref().AmountKingMaster -= _startValues.AMOUNT_FOR_TAKE_UNIT;
                    _setterUnitMasterComponentRef.Unref().SetValues(true);
                }
            }
            else
            {
                if (CellComponent(xyCell).IsStartOther)
                {
                    CellUnitComponent(xyCell).SetUnit(UnitTypes.King, _startValues.AMOUNT_HEALTH_KING, _startValues.POWER_DAMAGE_KING, _startValues.AMOUNT_STEPS_KING, false, false, player);
                    _economyUnitMasterComponent.Unref().AmountKingOther -= _startValues.AMOUNT_FOR_TAKE_UNIT;
                    _setterUnitMasterComponentRef.Unref().SetValues(true);
                }
                else _setterUnitMasterComponentRef.Unref().SetValues(false);
            }    
        }
    }
}

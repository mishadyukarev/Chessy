using Leopotam.Ecs;
using Photon.Realtime;
using static MainGame;


public struct SetterUnitMasterComponent
{
    private int[] _xyCellIN;
    private UnitTypes _unitTypeIN;
    private Player _playerIN;

    private bool _isSettedOUT;

    private StartValuesGameConfig _nameValueManager;
    private CellBaseOperations _cellManager;
    private SystemsMasterManager _systemsMasterManager;


    public SetterUnitMasterComponent(StartValuesGameConfig nameValueManager, CellBaseOperations cellManager, SystemsMasterManager systemsMasterManager)
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

        _systemsMasterManager.InvokeRunSystem(SystemMasterTypes.Multiple, nameof(SetterUnitMasterSystem));

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


internal class SetterUnitMasterSystem : CellReduction, IEcsRunSystem
{
    private EcsComponentRef<SetterUnitMasterComponent> _setterUnitMasterComponentRef = default;
    private EcsComponentRef<EconomyMasterComponent.UnitsMasterComponent> _economyUnitMasterComponent = default;

    internal SetterUnitMasterSystem(ECSmanager eCSmanager) : base(eCSmanager)
    {
        _setterUnitMasterComponentRef = eCSmanager.EntitiesMasterManager.SetterUnitMasterComponentRef;
        _economyUnitMasterComponent = eCSmanager.EntitiesMasterManager.EconomyUnitsMasterComponentRef;

        _startValuesGameConfig = InstanceGame.StartValuesGameConfig;
    }


    public void Run()
    {
        _setterUnitMasterComponentRef.Unref().GetValues(out int[] xyCell, out UnitTypes unitType, out Player player);


        if (!CellEnvironmentComponent(xyCell).HaveMountain && !CellUnitComponent(xyCell).HaveUnit)
        {
            switch (unitType)
            {
                case UnitTypes.None:
                    break;


                case UnitTypes.King:

                    if (player.IsMasterClient)
                    {
                        if (CellComponent(xyCell).IsStartMaster)
                        {
                            CellUnitComponent(xyCell).SetUnit(UnitTypes.King, _startValuesGameConfig.AMOUNT_HEALTH_KING, _startValuesGameConfig.STANDART_AMOUNT_STEPS_KING, false, false, player);
                            _economyUnitMasterComponent.Unref().AmountKingMaster -= _startValuesGameConfig.AMOUNT_FOR_TAKE_UNIT;
                            _setterUnitMasterComponentRef.Unref().SetValues(true);
                        }
                    }
                    else
                    {
                        if (CellComponent(xyCell).IsStartOther)
                        {
                            CellUnitComponent(xyCell).SetUnit(UnitTypes.King, _startValuesGameConfig.AMOUNT_HEALTH_KING, _startValuesGameConfig.STANDART_AMOUNT_STEPS_KING, false, false, player);
                            _economyUnitMasterComponent.Unref().AmountKingOther -= _startValuesGameConfig.AMOUNT_FOR_TAKE_UNIT;
                            _setterUnitMasterComponentRef.Unref().SetValues(true);
                        }
                        else _setterUnitMasterComponentRef.Unref().SetValues(false);
                    }

                    break;


                case UnitTypes.Pawn:

                    if (player.IsMasterClient)
                    {
                        if (CellComponent(xyCell).IsStartMaster)
                        {
                            CellUnitComponent(xyCell).SetUnit(UnitTypes.Pawn, _startValuesGameConfig.AMOUNT_HEALTH_PAWN, _startValuesGameConfig.STANDART_AMOUNT_STEPS_PAWN, false, false, player);
                            _economyUnitMasterComponent.Unref().AmountUnitPawnMaster -= _startValuesGameConfig.AMOUNT_FOR_TAKE_UNIT;
                            _setterUnitMasterComponentRef.Unref().SetValues(true);
                        }
                    }

                    else
                    {
                        if (CellComponent(xyCell).IsStartOther)
                        {
                            CellUnitComponent(xyCell).SetUnit(UnitTypes.Pawn, _startValuesGameConfig.AMOUNT_HEALTH_PAWN, _startValuesGameConfig.STANDART_AMOUNT_STEPS_PAWN, false, false, player);
                            _economyUnitMasterComponent.Unref().AmountUnitPawnOther -= _startValuesGameConfig.AMOUNT_FOR_TAKE_UNIT;
                            _setterUnitMasterComponentRef.Unref().SetValues(true);
                        }
                        else _setterUnitMasterComponentRef.Unref().SetValues(false);
                    }

                    break;


                case UnitTypes.Rook:

                    if (player.IsMasterClient)
                    {
                        if (CellComponent(xyCell).IsStartMaster)
                        {
                            CellUnitComponent(xyCell).SetUnit(UnitTypes.Rook, _startValuesGameConfig.AMOUNT_HEALTH_ROOK, _startValuesGameConfig.STANDART_AMOUNT_STEPS_ROOK, false, false, player);
                            _economyUnitMasterComponent.Unref().AmountRookMaster -= _startValuesGameConfig.AMOUNT_FOR_TAKE_UNIT;
                            _setterUnitMasterComponentRef.Unref().SetValues(true);
                        }
                    }
                    else
                    {
                        if (CellComponent(xyCell).IsStartOther)
                        {
                            CellUnitComponent(xyCell).SetUnit(UnitTypes.Rook, _startValuesGameConfig.AMOUNT_HEALTH_ROOK, _startValuesGameConfig.STANDART_AMOUNT_STEPS_ROOK, false, false, player);
                            _economyUnitMasterComponent.Unref().AmountRookOther -= _startValuesGameConfig.AMOUNT_FOR_TAKE_UNIT;
                            _setterUnitMasterComponentRef.Unref().SetValues(true);
                        }
                        else _setterUnitMasterComponentRef.Unref().SetValues(false);
                    }

                    break;


                case UnitTypes.Bishop:

                    if (player.IsMasterClient)
                    {
                        if (CellComponent(xyCell).IsStartMaster)
                        {
                            CellUnitComponent(xyCell).SetUnit(UnitTypes.Bishop, _startValuesGameConfig.AMOUNT_HEALTH_BISHOP, _startValuesGameConfig.STANDART_AMOUNT_STEPS_BISHOP, false, false, player);
                            _economyUnitMasterComponent.Unref().AmountBishopMaster -= _startValuesGameConfig.AMOUNT_FOR_TAKE_UNIT;
                            _setterUnitMasterComponentRef.Unref().SetValues(true);
                        }
                    }
                    else
                    {
                        if (CellComponent(xyCell).IsStartOther)
                        {
                            CellUnitComponent(xyCell).SetUnit(UnitTypes.Bishop, _startValuesGameConfig.AMOUNT_HEALTH_BISHOP, _startValuesGameConfig.STANDART_AMOUNT_STEPS_BISHOP, false, false, player);
                            _economyUnitMasterComponent.Unref().AmountBishopOther -= _startValuesGameConfig.AMOUNT_FOR_TAKE_UNIT;
                            _setterUnitMasterComponentRef.Unref().SetValues(true);
                        }
                        else _setterUnitMasterComponentRef.Unref().SetValues(false);
                    }

                    break;


                default:
                    break;
            }
        }
    }
}
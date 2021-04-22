using Leopotam.Ecs;
using Photon.Realtime;


internal struct GetterUnitMasterComponent
{
    private SystemsMasterManager _systemsMasterManager;

    private UnitTypes _unitTypeIN;
    private Player _playerIN;

    private bool _isGettedOUT;



    internal GetterUnitMasterComponent(SystemsMasterManager systemsMasterManager)
    {
        _systemsMasterManager = systemsMasterManager;

        _unitTypeIN = default;
        _playerIN = default;

        _isGettedOUT = default;
    }


    internal bool TryGetUnit(UnitTypes unitTypeIN, Player playerIN)
    {
        _unitTypeIN = unitTypeIN;
        _playerIN = playerIN;

        _systemsMasterManager.InvokeRunSystem(SystemMasterTypes.Else, nameof(GetterUnitMasterSystem));

        return _isGettedOUT;
    }

    internal void Unpack(out UnitTypes unitTypeIN, out Player playerIN)
    {
        unitTypeIN = _unitTypeIN;
        playerIN = _playerIN;
    }

    internal void Pack(bool isGettedOUT) => _isGettedOUT = isGettedOUT;
}


internal class GetterUnitMasterSystem : CellReduction, IEcsRunSystem
{
    private EcsComponentRef<GetterUnitMasterComponent> _getterUnitMasterComponentRef = default;
    private EcsComponentRef<EconomyMasterComponent.UnitMasterComponent> _economyCountUnitMasterComponent = default;

    internal GetterUnitMasterSystem(ECSmanager eCSmanager, SupportGameManager supportManager) : base(eCSmanager, supportManager)
    {
        _getterUnitMasterComponentRef = eCSmanager.EntitiesMasterManager.GetterUnitMasterComponentRef;
        _economyCountUnitMasterComponent = eCSmanager.EntitiesMasterManager.EconomyUnitsMasterComponentRef;

        _startValues = supportManager.StartValuesConfig;
    }

    public void Run()
    {
        _getterUnitMasterComponentRef.Unref().Unpack(out UnitTypes unitTypeIN, out Player playerIN);

        switch (unitTypeIN)
        {
            case UnitTypes.None:
                break;

            case UnitTypes.King:
                TakeKing(playerIN);
                break;

            case UnitTypes.Pawn:
                TakePawn(playerIN);
                break;

            default:
                break;

        }
    }

    private void TakeKing(Player playerIN)
    {
        if (playerIN.IsMasterClient)
        {
            if (_economyCountUnitMasterComponent.Unref().AmountKingMaster >= _startValues.AMOUNT_FOR_TAKE_UNIT)
            {
                _getterUnitMasterComponentRef.Unref().Pack(true);
                //_economyCountUnitMasterComponent.Unref().AmountKingMaster -= _startValues.TakeUnit;
            }
            else _getterUnitMasterComponentRef.Unref().Pack(false);
        }
        else
        {
            if (_economyCountUnitMasterComponent.Unref().AmountKingOther >= _startValues.AMOUNT_FOR_TAKE_UNIT)
            {
                _getterUnitMasterComponentRef.Unref().Pack(true);
                //_economyCountUnitMasterComponent.Unref().AmountKingOther -= _startValues.TakeUnit;
            }
            else _getterUnitMasterComponentRef.Unref().Pack(false);
        }
    }

    private void TakePawn(Player playerIN)
    {
        if (playerIN.IsMasterClient)
        {
            if (_economyCountUnitMasterComponent.Unref().AmountUnitPawnMaster >= _startValues.AMOUNT_FOR_TAKE_UNIT)
            {
                _getterUnitMasterComponentRef.Unref().Pack(true);
                // _economyCountUnitMasterComponent.Unref().AmountUnitPawnMaster -= _startValues.TakeUnit;
            }
            else _getterUnitMasterComponentRef.Unref().Pack(false);
        }
        else
        {
            if (_economyCountUnitMasterComponent.Unref().AmountUnitPawnOther >= _startValues.AMOUNT_FOR_TAKE_UNIT)
            {
                _getterUnitMasterComponentRef.Unref().Pack(true);
                //_economyCountUnitMasterComponent.Unref().AmountUnitPawnOther -= _startValues.TakeUnit;
            }
            else _getterUnitMasterComponentRef.Unref().Pack(false);
        }
    }
}
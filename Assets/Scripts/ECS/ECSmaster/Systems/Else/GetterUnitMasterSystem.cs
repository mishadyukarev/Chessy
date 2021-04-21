using Leopotam.Ecs;
using Photon.Realtime;

internal class GetterUnitMasterSystem : CellReduction, IEcsRunSystem
{
    private EcsComponentRef<GetterUnitMasterComponent> _getterUnitMasterComponentRef = default;
    private EcsComponentRef<EconomyMasterComponent.UnitsMasterComponent> _economyCountUnitMasterComponent = default;

    internal GetterUnitMasterSystem(ECSmanager eCSmanager, SupportManager supportManager) : base(eCSmanager, supportManager)
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
            if (_economyCountUnitMasterComponent.Unref().AmountKingMaster >= _startValues.TAKE_UNIT)
            {
                _getterUnitMasterComponentRef.Unref().Pack(true);
                //_economyCountUnitMasterComponent.Unref().AmountKingMaster -= _startValues.TakeUnit;
            }
            else _getterUnitMasterComponentRef.Unref().Pack(false);
        }
        else
        {
            if (_economyCountUnitMasterComponent.Unref().AmountKingOther >= _startValues.TAKE_UNIT)
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
            if (_economyCountUnitMasterComponent.Unref().AmountUnitPawnMaster >= _startValues.TAKE_UNIT)
            {
                _getterUnitMasterComponentRef.Unref().Pack(true);
                // _economyCountUnitMasterComponent.Unref().AmountUnitPawnMaster -= _startValues.TakeUnit;
            }
            else _getterUnitMasterComponentRef.Unref().Pack(false);
        }
        else
        {
            if (_economyCountUnitMasterComponent.Unref().AmountUnitPawnOther >= _startValues.TAKE_UNIT)
            {
                _getterUnitMasterComponentRef.Unref().Pack(true);
                //_economyCountUnitMasterComponent.Unref().AmountUnitPawnOther -= _startValues.TakeUnit;
            }
            else _getterUnitMasterComponentRef.Unref().Pack(false);
        }
    }
}

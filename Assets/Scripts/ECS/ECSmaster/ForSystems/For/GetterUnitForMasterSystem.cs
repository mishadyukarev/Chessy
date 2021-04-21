using Leopotam.Ecs;
using Photon.Realtime;

internal class GetterUnitForMasterSystem : StartValuesReduction
{
    private EcsComponentRef<EconomyMasterComponent.UnitMasterComponent> _economyUnitMasterComponent = default;

    internal GetterUnitForMasterSystem(ECSmanager eCSmanager, SupportManager supportManager) : base(supportManager)
    {
        _economyUnitMasterComponent = eCSmanager.EntitiesMasterManager.EconomyUnitsMasterComponentRef;
    }

    public bool TryGetUnit(UnitTypes unitType, Player player)
    {
        switch (unitType)
        {
            case UnitTypes.King:

                if (player.IsMasterClient)
                {
                    if (_economyUnitMasterComponent.Unref().AmountKingMaster >= _startValues.TAKE_UNIT) return true;
                    else return false;
                }
                else
                {
                    if (_economyUnitMasterComponent.Unref().AmountKingOther >= _startValues.TAKE_UNIT) return true;
                    else return false;
                }

            case UnitTypes.Pawn:

                if (player.IsMasterClient)
                {
                    if (_economyUnitMasterComponent.Unref().AmountUnitPawnMaster >= _startValues.TAKE_UNIT) return true;
                    else return false;
                }
                else
                {
                    if (_economyUnitMasterComponent.Unref().AmountUnitPawnOther >= _startValues.TAKE_UNIT) return true;
                    else return false;
                }

            default:
                return false;
        }
    }
}

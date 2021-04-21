using Leopotam.Ecs;
using Photon.Realtime;

internal class InventorSupportSystem : CellReduction
{
    private EcsComponentRef<EconomyMasterComponent.UnitsMasterComponent> _economyCountUnitMasterComponent = default;

    internal InventorSupportSystem(ECSmanager eCSmanager, SupportManager supportManager) : base(eCSmanager, supportManager)
    {
        _economyCountUnitMasterComponent = eCSmanager.EntitiesMasterManager.EconomyUnitsMasterComponentRef;
    }

    public bool TryGetUnit(UnitTypes unitType, Player player)
    {
        switch (unitType)
        {
            case UnitTypes.King:

                if (player.IsMasterClient)
                {
                    if (_economyCountUnitMasterComponent.Unref().AmountKingMaster >= _startValues.TAKE_UNIT) return true;
                    else return false;
                }
                else
                {
                    if (_economyCountUnitMasterComponent.Unref().AmountKingOther >= _startValues.TAKE_UNIT) return true;
                    else return false;
                }

            case UnitTypes.Pawn:

                if (player.IsMasterClient)
                {
                    if (_economyCountUnitMasterComponent.Unref().AmountUnitPawnMaster >= _startValues.TAKE_UNIT) return true;
                    else return false;
                }
                else
                {
                    if (_economyCountUnitMasterComponent.Unref().AmountUnitPawnOther >= _startValues.TAKE_UNIT) return true;
                    else return false;
                }

            default:
                return false;
        }
    }
}

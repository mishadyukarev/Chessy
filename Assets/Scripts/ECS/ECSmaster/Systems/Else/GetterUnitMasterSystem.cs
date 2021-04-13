using Leopotam.Ecs;
using Photon.Realtime;
using UnityEngine;
using static Main;

internal class GetterUnitMasterSystem : CellReductionSystem, IEcsRunSystem
{
    private StartValuesConfig _startValues;

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
                break;

            case UnitTypes.Pawn:

                if (playerIN.IsMasterClient)
                {
                    if (_economyCountUnitMasterComponent.Unref().AmountUnitPawnMaster >= _startValues.TakeUnit)
                    {
                        _getterUnitMasterComponentRef.Unref().Pack(true);
                        _economyCountUnitMasterComponent.Unref().TakeAmountUnitPawnMaster(_startValues.TakeUnit);
                    }
                    else _getterUnitMasterComponentRef.Unref().Pack(false);
                }
                else
                {
                    if (_economyCountUnitMasterComponent.Unref().AmountUnitPawnOther >= _startValues.TakeUnit)
                    {
                        _getterUnitMasterComponentRef.Unref().Pack(true);
                        _economyCountUnitMasterComponent.Unref().TakeAmountUnitPawnOther(_startValues.TakeUnit);
                    }
                    else _getterUnitMasterComponentRef.Unref().Pack(false);
                }

                break;

            default:
                break;

        }
    }
}

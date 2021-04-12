using Leopotam.Ecs;
using Photon.Realtime;
using System;
using UnityEngine;
using static Main;

public class SetterUnitMasterSystem : CellReductionSystem, IEcsRunSystem
{
    private StartValuesConfig _startValues;

    private EcsComponentRef<SetterUnitMasterComponent> _setterUnitMasterComponentRef = default;


    internal SetterUnitMasterSystem(ECSmanager eCSmanager, SupportManager supportManager) : base(eCSmanager, supportManager)
    {
        _setterUnitMasterComponentRef = eCSmanager.EntitiesMasterManager.SetterUnitMasterComponentRef;

        _startValues = supportManager.StartValues;
    }


    public void Run()
    {
        _setterUnitMasterComponentRef.Unref().GetValues(out int[] xyCell, out UnitTypes unitType, out Player player);

        switch (unitType)
        {
            case UnitTypes.None:
                break;

            case UnitTypes.King:
                break;

            case UnitTypes.Pawn:
                ExecuteSetUnit(xyCell, unitType, player);
                break;

            default:
                break;
        }
    }

    private void ExecuteSetUnit(int[] xyCell, UnitTypes unitType, Player player)
    {
        if (!CellEnvironmentComponent(xyCell).HaveMountain && !CellUnitComponent(xyCell).HaveUnit)
        {
            if (player.IsMasterClient && CellComponent(xyCell).IsStartMaster)
            {
                CellUnitComponent(xyCell).SetResetUnit(unitType, _startValues.AmountHealthPawn, _startValues.PawerDamagePawn, _startValues.AmountStepsPawn,false, false, player);
                _setterUnitMasterComponentRef.Unref().SetValues(true);
            }

            else if (CellComponent(xyCell).IsStartOther)
            {
                CellUnitComponent(xyCell).SetResetUnit(unitType, _startValues.AmountHealthPawn, _startValues.PawerDamagePawn, _startValues.AmountStepsPawn, false, false, player);
                _setterUnitMasterComponentRef.Unref().SetValues(true);
            }

            else _setterUnitMasterComponentRef.Unref().SetValues(false);
        }
    }
}

﻿using Assets.Scripts;
using Assets.Scripts.ECS.Component;
using Assets.Scripts.ECS.Component.Game.Master;
using Leopotam.Ecs;
using System;

internal sealed class GetterUnitMasterSystem : IEcsRunSystem
{
    private EcsFilter<InfoMasCom> _infoFilter = default;
    private EcsFilter<ForGettingUnitMasCom> _getterUnitFilter = default;
    private EcsFilter<InventorUnitsComponent> _unitsInventorFilter = default;

    internal UnitTypes UnitType => _getterUnitFilter.Get1(0).UnitTypeForGetting;

    public void Run()
    {
        ref var infoCom = ref _infoFilter.Get1(0);
        ref var unitInventorCom = ref _unitsInventorFilter.Get1(0);

        bool isGetted = false; //= _eGM.UnitInventorEnt_UnitInventorCom.AmountUnits(UnitType, Info.Sender.IsMasterClient) >= _amountForTakingUnit;
        UnitTypes unitType = default;
        switch (UnitType)
        {
            case UnitTypes.None:
                throw new Exception();

            case UnitTypes.King:
                isGetted = unitInventorCom.HaveUnitInInventor(UnitType, infoCom.FromInfo.Sender.IsMasterClient);
                if (isGetted)
                {
                    unitType = UnitType;
                }
                break;

            case UnitTypes.Pawn:
                if (unitInventorCom.HaveUnitInInventor(UnitTypes.Pawn, infoCom.FromInfo.Sender.IsMasterClient))
                {
                    isGetted = true;
                    unitType = UnitType;
                }
                break;

            case UnitTypes.Rook:
                if (unitInventorCom.HaveUnitInInventor(UnitTypes.Rook, infoCom.FromInfo.Sender.IsMasterClient))
                {
                    isGetted = true;
                    unitType = UnitType;
                }
                break;

            case UnitTypes.Bishop:
                if (unitInventorCom.HaveUnitInInventor(UnitTypes.Bishop, infoCom.FromInfo.Sender.IsMasterClient))
                {
                    isGetted = true;
                    unitType = UnitType;
                }
                break;

            default:
                throw new Exception();
        }
        RpcGeneralSystem.GetUnitToGeneral(infoCom.FromInfo.Sender, isGetted, unitType);
    }
}

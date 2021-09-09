﻿using Assets.Scripts;
using Assets.Scripts.Abstractions.Enums;
using Assets.Scripts.ECS.Component;
using Assets.Scripts.ECS.Component.Game.Master;
using Assets.Scripts.ECS.Components.Data.Else.Game.General.AvailCells;
using Assets.Scripts.ECS.Game.General.Components;
using Leopotam.Ecs;
using Photon.Pun;

internal sealed class AttackUnitMasterSystem : IEcsRunSystem
{
    private EcsFilter<InfoMasCom> _infoMasterFilter = default;
    private EcsFilter<ForAttackMasCom> _forAttackFilter = default;

    private EcsFilter<CellUnitDataComponent, OwnerComponent, OwnerBotComponent> _cellUnitFilter = default;
    private EcsFilter<AvailCellsForAttackComp> _availCellsForAttack;

    public void Run()
    {
        ref var infoCom = ref _infoMasterFilter.Get1(0);
        ref var forAttackMasCom = ref _forAttackFilter.Get1(0);

        var sender = infoCom.FromInfo.Sender;
        var fromIdx = forAttackMasCom.IdxFromCell;
        var toIdxForAttack = forAttackMasCom.IdxToCell;

        ref var fromCellUnitDataCom = ref _cellUnitFilter.Get1(fromIdx);
        ref var fromOwnerCellUnitCom = ref _cellUnitFilter.Get2(fromIdx);

        ref var toCellUnitDataCom = ref _cellUnitFilter.Get1(toIdxForAttack);
        ref var toOwnerCellUnitCom = ref _cellUnitFilter.Get2(toIdxForAttack);
        ref var toBotOwnerCellUnitCom = ref _cellUnitFilter.Get3(toIdxForAttack);



        AttackTypes simpUniqueType = default;


        var list1 = _availCellsForAttack.Get1(0).GetSimpleListCopy(fromOwnerCellUnitCom.IsMasterClient, fromIdx);

        foreach (var idx in list1)
        {
            if (idx == toIdxForAttack) simpUniqueType = AttackTypes.Simple;
        }


        var list2 = _availCellsForAttack.Get1(0).GetUniqueListCopy(fromOwnerCellUnitCom.IsMasterClient, fromIdx);

        foreach (var idx in list2)
        {
            if (idx == toIdxForAttack) simpUniqueType = AttackTypes.Unique;
        }


        if (simpUniqueType != default)
        {
            if (fromOwnerCellUnitCom.ActorNumber != toOwnerCellUnitCom.ActorNumber)
            {
                fromCellUnitDataCom.ResetAmountSteps();
                fromCellUnitDataCom.ResetConditionType();


                int damageFrom = 0;
                int damageTo = 0;

                damageTo += fromCellUnitDataCom.SimplePowerDamage;
                damageTo -= toCellUnitDataCom.PowerProtection;


                if (fromCellUnitDataCom.IsMelee)
                {
                    RpcGeneralSystem.SoundToGeneral(RpcTarget.All, SoundEffectTypes.AttackMelee);

                    damageFrom += toCellUnitDataCom.SimplePowerDamage;

                    if (simpUniqueType == AttackTypes.Unique)
                    {
                        damageTo += fromCellUnitDataCom.UniquePowerDamage;// CellUnitsDataSystem.UniquePowerDamage(fromUnitType);
                    }
                }

                else
                {
                    RpcGeneralSystem.SoundToGeneral(RpcTarget.All, SoundEffectTypes.AttackArcher);

                    if (simpUniqueType == AttackTypes.Unique)
                    {
                        damageTo += fromCellUnitDataCom.UniquePowerDamage;
                    }
                }

                if (damageTo < 0) damageTo = 0;

                fromCellUnitDataCom.TakeAmountHealth(damageFrom);
                toCellUnitDataCom.TakeAmountHealth(damageTo);

                if (!toCellUnitDataCom.HaveAmountHealth)
                {
                    if (toCellUnitDataCom.IsUnitType(UnitTypes.King))
                        RpcGeneralSystem.EndGameToMaster(fromOwnerCellUnitCom.ActorNumber);

                    toCellUnitDataCom.ResetUnitType();

                    if (fromCellUnitDataCom.IsMelee)
                    {
                        toCellUnitDataCom.ReplaceUnit(fromCellUnitDataCom);
                    }
                }

                if (!fromCellUnitDataCom.HaveAmountHealth)
                {
                    if (fromCellUnitDataCom.IsUnitType(UnitTypes.King))
                    {
                        if (toOwnerCellUnitCom.HaveOwner)
                        {
                            RpcGeneralSystem.EndGameToMaster(toOwnerCellUnitCom.ActorNumber);
                        }

                        else if (toBotOwnerCellUnitCom.IsBot)
                        {

                        }
                    }

                    fromCellUnitDataCom.ResetUnitType();
                }
            }

            //RPCGameSystem.AttackUnitToGeneral(Sender, _isAttacked);
            //RPCGameSystem.AttackUnitToGeneral(RpcTarget.All, false, _isAttacked, FromXy, ToXy);
        }
    }
}

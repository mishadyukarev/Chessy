using Assets.Scripts;
using Assets.Scripts.Abstractions.Enums;
using Assets.Scripts.ECS.Component;
using Assets.Scripts.ECS.Component.Game.Master;
using Assets.Scripts.ECS.Game.General.Components;
using Leopotam.Ecs;
using Photon.Pun;

internal sealed class AttackUnitMasterSystem : IEcsRunSystem
{
    private EcsFilter<InfoMasCom> _infoMasterFilter = default;
    private EcsFilter<ForAttackMasCom> _forAttackFilter = default;
    private EcsFilter<UnitsInGameInfoComponent> _idxUnitsFilter = default;
    private EcsFilter<UnitsInConditionInGameCom> _unitsInCondFilter = default;

    private EcsFilter<CellUnitDataComponent, OwnerComponent, OwnerBotComponent> _cellUnitFilter = default;

    public void Run()
    {
        ref var infoCom = ref _infoMasterFilter.Get1(0);
        ref var forAttackMasCom = ref _forAttackFilter.Get1(0);
        ref var unitsInGameCom = ref _idxUnitsFilter.Get1(0);
        ref var unitsInCondCom = ref _unitsInCondFilter.Get1(0);

        var sender = infoCom.FromInfo.Sender;
        var fromIdx = forAttackMasCom.IdxFromCell;
        var toIdxForAttack = forAttackMasCom.IdxToCell;

        ref var fromCellUnitDataCom = ref _cellUnitFilter.Get1(fromIdx);
        ref var fromOwnerCellUnitCom = ref _cellUnitFilter.Get2(fromIdx);

        ref var toCellUnitDataCom = ref _cellUnitFilter.Get1(toIdxForAttack);
        ref var toOwnerCellUnitCom = ref _cellUnitFilter.Get2(toIdxForAttack);
        ref var toBotOwnerCellUnitCom = ref _cellUnitFilter.Get3(toIdxForAttack);


        if (fromOwnerCellUnitCom.ActorNumber != toOwnerCellUnitCom.ActorNumber)
        {
            unitsInCondCom.ReplaceCondition(fromCellUnitDataCom.ConditionUnitType, ConditionUnitTypes.None, fromCellUnitDataCom.UnitType, fromOwnerCellUnitCom.IsMasterClient, fromIdx);

            fromCellUnitDataCom.ResetAmountSteps();
            fromCellUnitDataCom.ResetConditionType();


            int damageFrom = 0;
            int damageTo = 0;

            damageTo += fromCellUnitDataCom.SimplePowerDamage;
            damageTo -= toCellUnitDataCom.PowerProtection;


            if (fromCellUnitDataCom.IsMelee)
            {
                RpcGameSystem.SoundToGeneral(RpcTarget.All, SoundEffectTypes.AttackMelee);

                damageFrom += toCellUnitDataCom.SimplePowerDamage;

                //if (isFindedUnique)
                //{
                //    damageToSelelected += CellUnitsDataSystem.UniquePowerDamage(fromUnitType);
                //}
            }

            else
            {
                RpcGameSystem.SoundToGeneral(RpcTarget.All, SoundEffectTypes.AttackArcher);

                //if (isFindedUnique)
                //{
                //    damageToSelelected += CellUnitsDataSystem.UniquePowerDamage(fromUnitType);
                //}
            }

            if (damageTo < 0) damageTo = 0;

            fromCellUnitDataCom.TakeAmountHealth(damageFrom);
            toCellUnitDataCom.TakeAmountHealth(damageTo);


            if (!fromCellUnitDataCom.HaveAmountHealth)
            {
                if (fromCellUnitDataCom.IsUnitType(UnitTypes.King))
                {
                    if (toOwnerCellUnitCom.HaveOwner)
                    {
                        RpcGameSystem.EndGameToMaster(toOwnerCellUnitCom.ActorNumber);
                    }

                    else if (toBotOwnerCellUnitCom.IsBot)
                    {

                    }
                }

                unitsInGameCom.RemoveAmountUnitsInGame(fromCellUnitDataCom.UnitType, fromOwnerCellUnitCom.IsMasterClient, fromIdx);
                unitsInCondCom.RemoveUnitInCondition(fromCellUnitDataCom.ConditionUnitType, fromCellUnitDataCom.UnitType, fromOwnerCellUnitCom.IsMasterClient, fromIdx);
                fromCellUnitDataCom.ResetUnitType();

                if (fromOwnerCellUnitCom.HaveOwner)
                {
                    unitsInGameCom.RemoveAmountUnitsInGame(fromCellUnitDataCom.UnitType, fromOwnerCellUnitCom.IsMasterClient, fromIdx);
                }
            }

            if (!toCellUnitDataCom.HaveAmountHealth)
            {
                if (toCellUnitDataCom.IsUnitType(UnitTypes.King))
                    RpcGameSystem.EndGameToMaster(fromOwnerCellUnitCom.ActorNumber);

                unitsInGameCom.RemoveAmountUnitsInGame(toCellUnitDataCom.UnitType, toOwnerCellUnitCom.IsMasterClient, toIdxForAttack);
                unitsInCondCom.RemoveUnitInCondition(toCellUnitDataCom.ConditionUnitType, toCellUnitDataCom.UnitType, toOwnerCellUnitCom.IsMasterClient, toIdxForAttack);
                toCellUnitDataCom.ResetUnitType();

                if (fromCellUnitDataCom.IsMelee)
                {
                    unitsInGameCom.RemoveAmountUnitsInGame(fromCellUnitDataCom.UnitType, fromOwnerCellUnitCom.IsMasterClient, fromIdx);
                    unitsInGameCom.AddAmountUnitInGame(fromCellUnitDataCom.UnitType, fromOwnerCellUnitCom.IsMasterClient, toIdxForAttack);

                    toCellUnitDataCom.ReplaceUnit(fromCellUnitDataCom);
                }
            }
        }

        //RPCGameSystem.AttackUnitToGeneral(Sender, _isAttacked);
        //RPCGameSystem.AttackUnitToGeneral(RpcTarget.All, false, _isAttacked, FromXy, ToXy);
    }
}

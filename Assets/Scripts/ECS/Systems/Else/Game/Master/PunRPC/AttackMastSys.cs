using Assets.Scripts;
using Assets.Scripts.Abstractions.Enums;
using Assets.Scripts.ECS.Component.Data.Else.Game.General.Cell;
using Assets.Scripts.ECS.Component.Game.Master;
using Assets.Scripts.ECS.Components.Data.Else.Game.General;
using Assets.Scripts.ECS.Components.Data.Else.Game.General.AvailCells;
using Assets.Scripts.ECS.Game.General.Components;
using Leopotam.Ecs;
using Photon.Pun;

internal sealed class AttackMastSys : IEcsRunSystem
{
    private EcsFilter<InfoMasCom> _infoMasterFilter = default;
    private EcsFilter<ForAttackMasCom> _forAttackFilter = default;

    private EcsFilter<CellUnitDataCom, OwnerOnlineComp, OwnerOfflineCom, OwnerBotComponent> _cellUnitFilter = default;
    private EcsFilter<CellBuildDataComponent> _cellBuildFilter = default;
    private EcsFilter<CellEnvironDataCom> _cellEnvFilter = default;

    private EcsFilter<CellsForAttackCom> _cellsAttackFilt = default;

    private EcsFilter<EndGameDataUIComponent> _endGameDataUIFilter = default;

    public void Run()
    {
        ref var infoCom = ref _infoMasterFilter.Get1(0);
        ref var forAttackMasCom = ref _forAttackFilter.Get1(0);

        var sender = infoCom.FromInfo.Sender;
        var fromIdx = forAttackMasCom.IdxFromCell;
        var toIdxAttack = forAttackMasCom.IdxToCell;

        ref var fromUnitDatCom = ref _cellUnitFilter.Get1(fromIdx);
        ref var fromOnUnitCom = ref _cellUnitFilter.Get2(fromIdx);
        ref var fromOffUnitCom = ref _cellUnitFilter.Get3(fromIdx);
        ref var fromBotUnitCom = ref _cellUnitFilter.Get4(fromIdx);

        ref var toUnitDatCom = ref _cellUnitFilter.Get1(toIdxAttack);
        ref var toOnUnitCom = ref _cellUnitFilter.Get2(toIdxAttack);
        ref var toOffUnitCom = ref _cellUnitFilter.Get3(toIdxAttack);
        ref var toBotUnitCom = ref _cellUnitFilter.Get4(toIdxAttack);
        ref var toBuildDatCom = ref _cellBuildFilter.Get1(toIdxAttack);
        ref var toEnvDatCom = ref _cellEnvFilter.Get1(toIdxAttack);

        ref var cellsAttackCom = ref _cellsAttackFilt.Get1(0);

        AttackTypes simpUniqueType = default;

        if (PhotonNetwork.OfflineMode)
        {
            if (cellsAttackCom.FindByIdx(AttackTypes.Simple, fromOffUnitCom.IsMainMaster, fromIdx, toIdxAttack))
                simpUniqueType = AttackTypes.Simple;

            if (cellsAttackCom.FindByIdx(AttackTypes.Unique, fromOffUnitCom.IsMainMaster, fromIdx, toIdxAttack))
                simpUniqueType = AttackTypes.Unique;
        }

        else
        {
            if (cellsAttackCom.FindByIdx(AttackTypes.Simple, fromOnUnitCom.IsMasterClient, fromIdx, toIdxAttack))
                simpUniqueType = AttackTypes.Simple;

            if (cellsAttackCom.FindByIdx(AttackTypes.Unique, fromOnUnitCom.IsMasterClient, fromIdx, toIdxAttack))
                simpUniqueType = AttackTypes.Unique;
        }




        if (simpUniqueType != default)
        {
            fromUnitDatCom.ResetAmountSteps();
            fromUnitDatCom.ResetConditionType();


            int damageFrom = 0;
            int damageTo = 0;

            damageTo += fromUnitDatCom.SimplePowerDamage;
            damageTo -= toUnitDatCom.PowerProtection;
            damageTo -= toBuildDatCom.PowerProtectionUnit(toUnitDatCom.UnitType);
            damageTo -= toEnvDatCom.PowerProtectionUnit(toUnitDatCom.UnitType);


            if (fromUnitDatCom.IsMelee)
            {
                RpcSys.SoundToGeneral(RpcTarget.All, SoundEffectTypes.AttackMelee);

                if (toUnitDatCom.IsMelee)
                {
                    damageFrom += toUnitDatCom.SimplePowerDamage;

                    if (simpUniqueType == AttackTypes.Unique)
                    {
                        damageTo += fromUnitDatCom.UniquePowerDamage;
                    }
                }
            }

            else
            {
                RpcSys.SoundToGeneral(RpcTarget.All, SoundEffectTypes.AttackArcher);

                if (simpUniqueType == AttackTypes.Unique)
                {
                    damageTo += fromUnitDatCom.UniquePowerDamage;
                }
            }

            fromUnitDatCom.TakeAmountHealth(damageFrom);
            toUnitDatCom.TakeAmountHealth(damageTo);


            if (!toUnitDatCom.HaveAmountHealth)
            {
                if (toUnitDatCom.IsUnit(UnitTypes.King))
                {
                    _endGameDataUIFilter.Get1(0).IsEndGame = true;
                    _endGameDataUIFilter.Get1(0).IsOwnerWinner = toOnUnitCom.HaveOwner;

                    if (toOnUnitCom.HaveOwner)
                    {
                        _endGameDataUIFilter.Get1(0).PlayerWinner = fromOnUnitCom.Owner;
                    }
                    else
                    {
                        _endGameDataUIFilter.Get1(0).IsBotWinner = false;
                    }
                }

                toUnitDatCom.ResetUnit();


                if (fromUnitDatCom.IsMelee)
                {
                    toUnitDatCom.ReplaceUnit(fromUnitDatCom);
                    if (PhotonNetwork.OfflineMode) toOffUnitCom.IsMainMaster = fromOffUnitCom.IsMainMaster;
                    else toOnUnitCom.SetOwner(fromOnUnitCom.Owner);

                    fromUnitDatCom.ResetUnit();

                    if (!toUnitDatCom.HaveAmountHealth)
                    {
                        toUnitDatCom.ResetUnit();
                    }
                }

                else
                {

                }
            }

            else if (!fromUnitDatCom.HaveAmountHealth)
            {
                if (fromUnitDatCom.IsUnit(UnitTypes.King))
                {
                    _endGameDataUIFilter.Get1(0).IsEndGame = true;
                    _endGameDataUIFilter.Get1(0).IsOwnerWinner = false;
                    _endGameDataUIFilter.Get1(0).IsBotWinner = true;
                }

                fromUnitDatCom.ResetUnitType();
            }

            //RPCGameSystem.AttackUnitToGeneral(Sender, _isAttacked);
            //RPCGameSystem.AttackUnitToGeneral(RpcTarget.All, false, _isAttacked, FromXy, ToXy);
        }
    }
}

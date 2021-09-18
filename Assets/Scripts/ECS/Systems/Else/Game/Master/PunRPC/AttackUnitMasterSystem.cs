using Assets.Scripts;
using Assets.Scripts.Abstractions.Enums;
using Assets.Scripts.ECS.Component.Data.Else.Game.General.Cell;
using Assets.Scripts.ECS.Component.Game.Master;
using Assets.Scripts.ECS.Components.Data.Else.Game.General.AvailCells;
using Assets.Scripts.ECS.Game.General.Components;
using Leopotam.Ecs;
using Photon.Pun;

internal sealed class AttackUnitMasterSystem : IEcsRunSystem
{
    private EcsFilter<InfoMasCom> _infoMasterFilter = default;
    private EcsFilter<ForAttackMasCom> _forAttackFilter = default;

    private EcsFilter<CellUnitDataCom, OwnerOnlineComp, OwnerBotComponent> _cellUnitFilter = default;
    private EcsFilter<CellBuildDataComponent> _cellBuildFilter = default;
    private EcsFilter<CellEnvironDataCom> _cellEnvFilter = default;

    private EcsFilter<AvailCellsForAttackComp> _availCellsForAttack = default;

    private EcsFilter<EndGameDataUIComponent> _endGameDataUIFilter = default;

    public void Run()
    {
        ref var infoCom = ref _infoMasterFilter.Get1(0);
        ref var forAttackMasCom = ref _forAttackFilter.Get1(0);

        var sender = infoCom.FromInfo.Sender;
        var fromIdx = forAttackMasCom.IdxFromCell;
        var toIdxForAttack = forAttackMasCom.IdxToCell;

        ref var fromCellUnitDataCom = ref _cellUnitFilter.Get1(fromIdx);
        ref var fromOwnerCellUnitCom = ref _cellUnitFilter.Get2(fromIdx);
        ref var fromBotOwnerCellUnitComp = ref _cellUnitFilter.Get3(fromIdx);

        ref var toUnitDatCom = ref _cellUnitFilter.Get1(toIdxForAttack);
        ref var toOwnerCellUnitCom = ref _cellUnitFilter.Get2(toIdxForAttack);
        ref var toBotOwnerCellUnitCom = ref _cellUnitFilter.Get3(toIdxForAttack);
        ref var toBuildDatCom = ref _cellBuildFilter.Get1(toIdxForAttack);
        ref var toEnvDatCom = ref _cellEnvFilter.Get1(toIdxForAttack);

        ref var availCellsForAttackComp = ref _availCellsForAttack.Get1(0);

        AttackTypes simpUniqueType = default;



        if (availCellsForAttackComp.FindByIdx(AttackTypes.Simple, fromOwnerCellUnitCom.IsMasterClient, fromIdx, toIdxForAttack))
            simpUniqueType = AttackTypes.Simple;

        if (availCellsForAttackComp.FindByIdx(AttackTypes.Unique, fromOwnerCellUnitCom.IsMasterClient, fromIdx, toIdxForAttack))
            simpUniqueType = AttackTypes.Unique;


        if (simpUniqueType != default)
        {
            fromCellUnitDataCom.ResetAmountSteps();
            fromCellUnitDataCom.ResetConditionType();


            int damageFrom = 0;
            int damageTo = 0;

            damageTo += fromCellUnitDataCom.SimplePowerDamage;
            damageTo -= toUnitDatCom.PowerProtection;
            damageTo -= toBuildDatCom.PowerProtectionUnit(toUnitDatCom.UnitType);
            damageTo -= toEnvDatCom.PowerProtectionUnit(toUnitDatCom.UnitType);


            if (fromCellUnitDataCom.IsMelee)
            {
                RpcSys.SoundToGeneral(RpcTarget.All, SoundEffectTypes.AttackMelee);

                damageFrom += toUnitDatCom.SimplePowerDamage;

                if (simpUniqueType == AttackTypes.Unique)
                {
                    damageTo += fromCellUnitDataCom.UniquePowerDamage;
                }
            }

            else
            {
                RpcSys.SoundToGeneral(RpcTarget.All, SoundEffectTypes.AttackArcher);

                if (simpUniqueType == AttackTypes.Unique)
                {
                    damageTo += fromCellUnitDataCom.UniquePowerDamage;
                }
            }

            //if (damageTo < 0) damageTo = 0;

            fromCellUnitDataCom.TakeAmountHealth(damageFrom);
            toUnitDatCom.TakeAmountHealth(damageTo);


            if (!toUnitDatCom.HaveAmountHealth)
            {
                if (toUnitDatCom.Is(UnitTypes.King))
                {
                    _endGameDataUIFilter.Get1(0).IsEndGame = true;
                    _endGameDataUIFilter.Get1(0).IsOwnerWinner = toOwnerCellUnitCom.HaveOwner;

                    if (toOwnerCellUnitCom.HaveOwner)
                    {
                        _endGameDataUIFilter.Get1(0).PlayerWinner = fromOwnerCellUnitCom.Owner;
                    }
                    else
                    {
                        _endGameDataUIFilter.Get1(0).IsBotWinner = false;
                    }
                }

                toUnitDatCom.ResetUnit();


                if (fromCellUnitDataCom.IsMelee)
                {
                    toUnitDatCom.ReplaceUnit(fromCellUnitDataCom);
                    toOwnerCellUnitCom.SetOwner(fromOwnerCellUnitCom.Owner);

                    fromCellUnitDataCom.ResetUnit();

                    if (!toUnitDatCom.HaveAmountHealth)
                    {
                        toUnitDatCom.ResetUnit();
                    }
                }

                else
                {

                }
            }

            else if (!fromCellUnitDataCom.HaveAmountHealth)
            {
                if (fromCellUnitDataCom.Is(UnitTypes.King))
                {
                    _endGameDataUIFilter.Get1(0).IsEndGame = true;
                    _endGameDataUIFilter.Get1(0).IsOwnerWinner = false;
                    _endGameDataUIFilter.Get1(0).IsBotWinner = true;
                }

                fromCellUnitDataCom.ResetUnitType();
            }

            //RPCGameSystem.AttackUnitToGeneral(Sender, _isAttacked);
            //RPCGameSystem.AttackUnitToGeneral(RpcTarget.All, false, _isAttacked, FromXy, ToXy);
        }
    }
}

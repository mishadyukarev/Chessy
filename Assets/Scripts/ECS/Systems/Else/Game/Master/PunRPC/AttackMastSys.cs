using Assets.Scripts;
using Assets.Scripts.Abstractions.Enums;
using Assets.Scripts.ECS.Component.Data.Else.Game.General.Cell;
using Assets.Scripts.ECS.Component.Game.Master;
using Assets.Scripts.ECS.Components.Data.Else.Game.General.AvailCells;
using Assets.Scripts.Supports;
using Leopotam.Ecs;
using Photon.Pun;

internal sealed class AttackMastSys : IEcsRunSystem
{
    private EcsFilter<InfoMasCom> _infoMasterFilter = default;
    private EcsFilter<ForAttackMasCom> _forAttackFilter = default;

    private EcsFilter<CellUnitDataCom, OwnerCom> _cellUnitFilter = default;
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
        ref var fromOwnUnitCom = ref _cellUnitFilter.Get2(fromIdx);

        ref var toUnitDatCom = ref _cellUnitFilter.Get1(toIdxAttack);
        ref var toOwnUnitCom = ref _cellUnitFilter.Get2(toIdxAttack);
        ref var toBuildDatCom = ref _cellBuildFilter.Get1(toIdxAttack);
        ref var toEnvDatCom = ref _cellEnvFilter.Get1(toIdxAttack);

        ref var cellsAttackCom = ref _cellsAttackFilt.Get1(0);

        AttackTypes simpUniqueType = default;


        if (cellsAttackCom.FindByIdx(fromOwnUnitCom.PlayerType, AttackTypes.Simple,  fromIdx, toIdxAttack))
            simpUniqueType = AttackTypes.Simple;

        if (cellsAttackCom.FindByIdx(fromOwnUnitCom.PlayerType, AttackTypes.Unique,  fromIdx, toIdxAttack))
            simpUniqueType = AttackTypes.Unique;





        if (simpUniqueType != default)
        {
            fromUnitDatCom.ResetAmountSteps();
            fromUnitDatCom.ResetConditionType();


            int damageFrom = 0;
            int damageTo = 0;

            damageTo += fromUnitDatCom.SimplePowerDamage;
            damageTo -= toUnitDatCom.PowerProtection;
            damageTo -= toBuildDatCom.PowerProtectionUnit(toUnitDatCom.UnitType, fromUnitDatCom.SimplePowerDamage);
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
                    _endGameDataUIFilter.Get1(0).PlayerWinner = fromOwnUnitCom.PlayerType;
                }

                toUnitDatCom.ResetUnit();


                if (fromUnitDatCom.IsMelee)
                {
                    toUnitDatCom.ReplaceUnit(fromUnitDatCom);
                    toOwnUnitCom.PlayerType = fromOwnUnitCom.PlayerType;

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
                    fromOwnUnitCom.PlayerType = toOwnUnitCom.PlayerType;
                    _endGameDataUIFilter.Get1(0).PlayerWinner = toOwnUnitCom.PlayerType;
                }

                fromUnitDatCom.DefUnitType();
            }
        }
    }
}

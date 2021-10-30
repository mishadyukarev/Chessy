﻿using Leopotam.Ecs;
using Photon.Pun;

namespace Scripts.Game
{
    internal sealed class AttackMastSys : IEcsRunSystem
    {
        private EcsFilter<ForAttackMasCom> _forAttackFilter = default;

        private EcsFilter<CellUnitDataCom, HpUnitC, DamageComponent, StepComponent> _cellUnitFilter = default;
        private EcsFilter<CellUnitDataCom, ConditionUnitC, ToolWeaponC, UnitEffectsC, ThirstyC, OwnerCom> _cellUnitEffFilt = default;

        private EcsFilter<CellBuildDataCom> _cellBuildFilter = default;
        private EcsFilter<CellEnvironmentDataC> _cellEnvFilter = default;
        private EcsFilter<CellRiverDataC> _cellRiverFilt = default;

        public void Run()
        {
            ref var forAttackMasCom = ref _forAttackFilter.Get1(0);



            var fromIdx = forAttackMasCom.IdxFromCell;
            var toIdxAttack = forAttackMasCom.IdxToCell;

            ref var unitC_from = ref _cellUnitFilter.Get1(fromIdx);
            ref var hpUnitC_from = ref _cellUnitFilter.Get2(fromIdx);
            ref var fromDamUnitC = ref _cellUnitFilter.Get3(fromIdx);
            ref var stepUnitC_from = ref _cellUnitFilter.Get4(fromIdx);

            ref var condUnitC_from = ref _cellUnitEffFilt.Get2(fromIdx);
            ref var twUnitC_from = ref _cellUnitEffFilt.Get3(fromIdx);
            ref var effUnitC_from = ref _cellUnitEffFilt.Get4(fromIdx);
            ref var thirUnitC_from = ref _cellUnitEffFilt.Get5(fromIdx);
            ref var fromOwnUnitCom = ref _cellUnitEffFilt.Get6(fromIdx);

            ref var riverC_from = ref _cellRiverFilt.Get1(fromIdx);


            ref var unitC_to = ref _cellUnitFilter.Get1(toIdxAttack);
            ref var hpUnitC_to = ref _cellUnitFilter.Get2(toIdxAttack);
            ref var toDamUnitC =ref _cellUnitFilter.Get3(toIdxAttack);
            ref var stepUnitC_to = ref _cellUnitFilter.Get4(toIdxAttack);

            ref var condUnitC_to = ref _cellUnitEffFilt.Get2(toIdxAttack);
            ref var twUnitC_to = ref _cellUnitEffFilt.Get3(toIdxAttack);
            ref var effUnitC_to = ref _cellUnitEffFilt.Get4(toIdxAttack);
            ref var thirUnitC_to = ref _cellUnitEffFilt.Get5(toIdxAttack);
            ref var ownUnitC_to = ref _cellUnitEffFilt.Get6(toIdxAttack);

            ref var riverC_to = ref _cellRiverFilt.Get1(toIdxAttack);


            ref var toBuildDatCom = ref _cellBuildFilter.Get1(toIdxAttack);
            ref var toEnvDatCom = ref _cellEnvFilter.Get1(toIdxAttack);


            var simpUniqueType = CellsAttackC.FindByIdx(fromOwnUnitCom.PlayerType, fromIdx, toIdxAttack);

            if (simpUniqueType != default)
            {
                stepUnitC_from.ZeroSteps();
                condUnitC_from.DefCondition();
                //stepUnitC_to.ZeroSteps();
                //condUnitC_to.DefCondition();


                float powerDamFrom = 0;
                float powerDamTo = 0;


                powerDamFrom += fromDamUnitC.DamageAttack(unitC_from, twUnitC_from, effUnitC_from, simpUniqueType);

                if (unitC_from.IsMelee)
                    RpcSys.SoundToGeneral(RpcTarget.All, SoundEffectTypes.AttackMelee);
                else RpcSys.SoundToGeneral(RpcTarget.All, SoundEffectTypes.AttackArcher);


                if (unitC_to.IsMelee)
                {
                    powerDamTo += toDamUnitC.DamageOnCell(unitC_to, condUnitC_to, twUnitC_to, effUnitC_to, toBuildDatCom.BuildType, toEnvDatCom.Envronments);
                }


                float min = 0;
                float max = 0;
                float minusTo = 0;
                float minusFrom = 0;

                if (powerDamTo > powerDamFrom)
                {
                    max = powerDamTo * 2f;
                    min = powerDamTo / 2f;
                    if (min < powerDamFrom) minusTo = 100 * powerDamFrom / max;

                    max = powerDamFrom * 2;
                    minusFrom = 100 * powerDamTo / max;
                }
                else
                {
                    max = powerDamTo * 2f;
                    minusTo = 100 * powerDamFrom / max;

                    max = powerDamFrom * 2;
                    min = powerDamFrom / 2;
                    if (min < powerDamTo) minusFrom = 100 * powerDamTo / max;
                }



                if (!unitC_from.IsMelee) minusFrom = 0;
                if (twUnitC_from.Is(ToolWeaponTypes.Shield))
                {
                    minusFrom = 0;
                    twUnitC_from.TakeShieldProtect();
                }

                if (minusFrom > 0)
                {
                    hpUnitC_from.TakeHp((int)minusFrom);
                    if (hpUnitC_from.IsHpDeathAfterAttack) hpUnitC_from.ZeroHp();
                }




                if (!unitC_to.IsMelee) minusTo = hpUnitC_to.MaxHpUnit(effUnitC_to, unitC_to.UnitType);
                if (twUnitC_to.Is(ToolWeaponTypes.Shield))
                {
                    minusTo = 0;
                    twUnitC_to.TakeShieldProtect();
                }

                if (minusTo > 0)
                {
                    hpUnitC_to.TakeHp((int)minusTo);
                    if (hpUnitC_to.IsHpDeathAfterAttack) hpUnitC_to.ZeroHp();
                }



                if (!hpUnitC_to.HaveHp)
                {
                    if (unitC_to.Is(UnitTypes.King))
                    {
                        EndGameDataUIC.PlayerWinner = fromOwnUnitCom.PlayerType;
                    }
                    else if (unitC_to.Is(UnitTypes.Scout))
                    {
                        InventorUnitsC.AddUnitsInInventor(ownUnitC_to.PlayerType, unitC_to.UnitType, LevelUnitTypes.Wood);
                    }

                    unitC_to.DefUnitType();


                    if (unitC_from.IsMelee)
                    {
                        unitC_to.Set(unitC_from);
                        hpUnitC_to.AmountHp = hpUnitC_from.AmountHp;
                        stepUnitC_to.AmountSteps = stepUnitC_from.AmountSteps;
                        condUnitC_to.CondUnitType = condUnitC_from.CondUnitType;
                        twUnitC_to.Set(twUnitC_from);
                        ownUnitC_to.PlayerType = fromOwnUnitCom.PlayerType;

                        unitC_from.DefUnitType();

                        if (!hpUnitC_to.HaveHp) unitC_to.DefUnitType();

                        if (riverC_to.HaveNearRiver) thirUnitC_to.SetMaxWater(unitC_to.UnitType);
                    }
                }

                else if (!hpUnitC_from.HaveHp)
                {
                    if (unitC_from.Is(UnitTypes.King))
                    {
                        fromOwnUnitCom.PlayerType = ownUnitC_to.PlayerType;
                        EndGameDataUIC.PlayerWinner = ownUnitC_to.PlayerType;
                    }

                    unitC_from.DefUnitType();
                }

                hpUnitC_to.TryTakeBonusHp(effUnitC_to, unitC_to.UnitType);
                hpUnitC_from.TryTakeBonusHp(effUnitC_from, unitC_from.UnitType);

                effUnitC_from.DefAllEffects();
                effUnitC_to.DefAllEffects();
            }
        }
    }
}
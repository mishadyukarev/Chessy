using Leopotam.Ecs;
using Photon.Pun;

namespace Scripts.Game
{
    internal sealed class AttackMastSys : IEcsRunSystem
    {
        private EcsFilter<ForAttackMasCom> _forAttackFilter = default;

        private EcsFilter<CellUnitDataCom, HpUnitC, DamageComponent, StepComponent> _cellUnitFilter = default;
        private EcsFilter<CellUnitDataCom, ConditionUnitC, ToolWeaponC, UnitEffectsC, OwnerCom> _cellUnitEffFilt = default;

        private EcsFilter<CellBuildDataCom> _cellBuildFilter = default;
        private EcsFilter<CellEnvironmentDataC> _cellEnvFilter = default;

        public void Run()
        {
            ref var forAttackMasCom = ref _forAttackFilter.Get1(0);

            var fromIdx = forAttackMasCom.IdxFromCell;
            var toIdxAttack = forAttackMasCom.IdxToCell;

            ref var fromUnitC = ref _cellUnitFilter.Get1(fromIdx);
            ref var fromHpUnitC = ref _cellUnitFilter.Get2(fromIdx);
            ref var fromDamUnitC = ref _cellUnitFilter.Get3(fromIdx);
            ref var stepUnitC_from = ref _cellUnitFilter.Get4(fromIdx);

            ref var condUnitC_from = ref _cellUnitEffFilt.Get2(fromIdx);
            ref var twUnitC_from = ref _cellUnitEffFilt.Get3(fromIdx);
            ref var effUnitC_from = ref _cellUnitEffFilt.Get4(fromIdx);
            ref var fromOwnUnitCom = ref _cellUnitEffFilt.Get5(fromIdx);


            ref var toUnitC = ref _cellUnitFilter.Get1(toIdxAttack);
            ref var toHpUnitC = ref _cellUnitFilter.Get2(toIdxAttack);
            ref var toDamUnitC =ref _cellUnitFilter.Get3(toIdxAttack);
            ref var toStepUnitC = ref _cellUnitFilter.Get4(toIdxAttack);

            ref var condUnitC_to = ref _cellUnitEffFilt.Get2(toIdxAttack);
            ref var twUnitC_to = ref _cellUnitEffFilt.Get3(toIdxAttack);
            ref var effUnitC_to = ref _cellUnitEffFilt.Get4(toIdxAttack);
            ref var ownUnitC_to = ref _cellUnitEffFilt.Get5(toIdxAttack);


            ref var toBuildDatCom = ref _cellBuildFilter.Get1(toIdxAttack);
            ref var toEnvDatCom = ref _cellEnvFilter.Get1(toIdxAttack);


            var simpUniqueType = CellsAttackC.FindByIdx(fromOwnUnitCom.PlayerType, fromIdx, toIdxAttack);

            if (simpUniqueType != default)
            {
                effUnitC_from.Def(StatTypes.Health);
                effUnitC_from.Def(StatTypes.Damage);
                effUnitC_from.Def(StatTypes.Steps);
                stepUnitC_from.DefSteps();
                condUnitC_from.DefCondition();

                effUnitC_to.Def(StatTypes.Health);
                effUnitC_to.Def(StatTypes.Damage);
                effUnitC_to.Def(StatTypes.Steps);


                float powerDamFrom = 0;
                float powerDamTo = 0;

      
                powerDamFrom += fromDamUnitC.DamageAttack(fromUnitC, twUnitC_from, effUnitC_from, simpUniqueType);

                if (fromUnitC.IsMelee)
                    RpcSys.SoundToGeneral(RpcTarget.All, SoundEffectTypes.AttackMelee);
                else RpcSys.SoundToGeneral(RpcTarget.All, SoundEffectTypes.AttackArcher);
                

                if (toUnitC.IsMelee)
                {
                    powerDamTo += toDamUnitC.DamageOnCell(toUnitC, condUnitC_to, twUnitC_to, effUnitC_to, toBuildDatCom.BuildType, toEnvDatCom.Envronments);
                }


                float min = 0;
                float max = 0;
                float minusTo = 0;
                float minusFrom = 0;

                if (powerDamTo > powerDamFrom)
                {
                    max = powerDamTo * 2f;
                    min = powerDamTo / 2f;
                    if(min < powerDamFrom) minusTo = 100 * powerDamFrom / max;

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



                if (!fromUnitC.IsMelee) minusFrom = 0;
                if (twUnitC_from.Is(ToolWeaponTypes.Shield))
                {
                    minusFrom = 0;
                    twUnitC_from.TakeShieldProtect();
                }

                if (minusFrom > 0)
                {
                    fromHpUnitC.TakeHp((int)minusFrom);
                    if (fromHpUnitC.AmountHp <= UnitValues.HP_FOR_DEATH_AFTER_ATTACK) fromHpUnitC.DefHp();
                }
                



                if (!toUnitC.IsMelee) minusTo = UnitValues.StandMaxHpUnit(toUnitC.UnitType);
                if (twUnitC_to.Is(ToolWeaponTypes.Shield))
                {
                    minusTo = 0;
                    twUnitC_to.TakeShieldProtect();
                }

                if (minusTo > 0)
                {
                    toHpUnitC.TakeHp((int)minusTo);
                    if (toHpUnitC.AmountHp <= UnitValues.HP_FOR_DEATH_AFTER_ATTACK) toHpUnitC.DefHp();
                }
                


                if (!toHpUnitC.HaveHp)
                {
                    if (toUnitC.Is(UnitTypes.King))
                    {
                        EndGameDataUIC.PlayerWinner = fromOwnUnitCom.PlayerType;
                    }
                    else if(toUnitC.Is(UnitTypes.Scout))
                    {
                        InventorUnitsC.AddUnitsInInventor(ownUnitC_to.PlayerType, toUnitC.UnitType, LevelUnitTypes.Wood);
                    }

                    toUnitC.DefUnitType();


                    if (fromUnitC.IsMelee)
                    {
                        toUnitC.Set(fromUnitC);
                        toHpUnitC.AmountHp = fromHpUnitC.AmountHp;
                        toStepUnitC.AmountSteps = stepUnitC_from.AmountSteps;
                        condUnitC_to.CondUnitType = condUnitC_from.CondUnitType;
                        twUnitC_to.Set(twUnitC_from);
                        ownUnitC_to.PlayerType = fromOwnUnitCom.PlayerType;

                        fromUnitC.DefUnitType();

                        if (!toHpUnitC.HaveHp)
                        {
                            toUnitC.DefUnitType();
                        }
                    }
                }

                else if (!fromHpUnitC.HaveHp)
                {
                    if (fromUnitC.Is(UnitTypes.King))
                    {
                        fromOwnUnitCom.PlayerType = ownUnitC_to.PlayerType;
                        EndGameDataUIC.PlayerWinner = ownUnitC_to.PlayerType;
                    }

                    fromUnitC.DefUnitType();
                }
            }
        }
    }
}
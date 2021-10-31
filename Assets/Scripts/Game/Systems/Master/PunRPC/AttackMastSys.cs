using Leopotam.Ecs;
using Photon.Pun;

namespace Scripts.Game
{
    internal sealed class AttackMastSys : IEcsRunSystem
    {
        private EcsFilter<ForAttackMasCom> _forAttackFilter = default;

        private EcsFilter<CellUnitDataCom, LevelUnitC, OwnerCom> _cellUnitMainFilt = default;
        private EcsFilter<CellUnitDataCom, HpUnitC, DamageComponent, StepComponent> _cellUnitFilter = default;
        private EcsFilter<CellUnitDataCom, ConditionUnitC, ToolWeaponC, UnitEffectsC, ThirstyUnitC> _cellUnitEffFilt = default;

        private EcsFilter<CellBuildDataC> _cellBuildFilter = default;
        private EcsFilter<CellEnvDataC> _cellEnvFilter = default;
        private EcsFilter<CellRiverDataC> _cellRiverFilt = default;

        public void Run()
        {
            ref var forAttackMasCom = ref _forAttackFilter.Get1(0);



            var idx_from = forAttackMasCom.IdxFromCell;
            var idx_to = forAttackMasCom.IdxToCell;

            ref var unitC_from = ref _cellUnitFilter.Get1(idx_from);

            ref var levUnitC_from = ref _cellUnitMainFilt.Get2(idx_from);
            ref var ownUnitC_from = ref _cellUnitMainFilt.Get3(idx_from);

            ref var hpUnitC_from = ref _cellUnitFilter.Get2(idx_from);
            ref var fromDamUnitC = ref _cellUnitFilter.Get3(idx_from);
            ref var stepUnitC_from = ref _cellUnitFilter.Get4(idx_from);

            ref var condUnitC_from = ref _cellUnitEffFilt.Get2(idx_from);
            ref var twUnitC_from = ref _cellUnitEffFilt.Get3(idx_from);
            ref var effUnitC_from = ref _cellUnitEffFilt.Get4(idx_from);
            ref var thirUnitC_from = ref _cellUnitEffFilt.Get5(idx_from);

            ref var riverC_from = ref _cellRiverFilt.Get1(idx_from);


            ref var unitC_to = ref _cellUnitFilter.Get1(idx_to);

            ref var levUnitC_to = ref _cellUnitMainFilt.Get2(idx_to);
            ref var ownUnitC_to = ref _cellUnitMainFilt.Get3(idx_to);

            ref var hpUnitC_to = ref _cellUnitFilter.Get2(idx_to);
            ref var toDamUnitC =ref _cellUnitFilter.Get3(idx_to);
            ref var stepUnitC_to = ref _cellUnitFilter.Get4(idx_to);

            ref var condUnitC_to = ref _cellUnitEffFilt.Get2(idx_to);
            ref var twUnitC_to = ref _cellUnitEffFilt.Get3(idx_to);
            ref var effUnitC_to = ref _cellUnitEffFilt.Get4(idx_to);
            ref var thirUnitC_to = ref _cellUnitEffFilt.Get5(idx_to);

            ref var riverC_to = ref _cellRiverFilt.Get1(idx_to);


            ref var toBuildDatCom = ref _cellBuildFilter.Get1(idx_to);
            ref var toEnvDatCom = ref _cellEnvFilter.Get1(idx_to);


            var simpUniqueType = CellsAttackC.FindByIdx(ownUnitC_from.Owner, idx_from, idx_to);

            if (simpUniqueType != default)
            {
                stepUnitC_from.ZeroSteps();
                condUnitC_from.DefCondition();
                //stepUnitC_to.ZeroSteps();
                //condUnitC_to.DefCondition();


                float powerDamFrom = 0;
                float powerDamTo = 0;


                powerDamFrom += fromDamUnitC.DamageAttack(unitC_from.Unit, levUnitC_from.Level, twUnitC_from, effUnitC_from, simpUniqueType);

                if (unitC_from.IsMelee)
                    RpcSys.SoundToGeneral(RpcTarget.All, SoundEffectTypes.AttackMelee);
                else RpcSys.SoundToGeneral(RpcTarget.All, SoundEffectTypes.AttackArcher);


                if (unitC_to.IsMelee)
                {
                    powerDamTo += toDamUnitC.DamageOnCell(unitC_to.Unit, levUnitC_to.Level, condUnitC_to, twUnitC_to, effUnitC_to, toBuildDatCom.BuildType, toEnvDatCom.Envronments);
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




                if (!unitC_to.IsMelee) minusTo = hpUnitC_to.MaxHpUnit(effUnitC_to, unitC_to.Unit);
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
                        EndGameDataUIC.PlayerWinner = ownUnitC_from.Owner;
                    }
                    else if (unitC_to.Is(UnitTypes.Scout))
                    {
                        InventorUnitsC.AddUnit(ownUnitC_to.Owner, unitC_to.Unit, LevelUnitTypes.Wood);
                    }

                    unitC_to.NoneUnit();


                    if (unitC_from.IsMelee)
                    {
                        unitC_to.SetUnit(unitC_from.Unit);
                        levUnitC_to.SetLevel(levUnitC_from.Level);
                        hpUnitC_to.AmountHp = hpUnitC_from.AmountHp;
                        stepUnitC_to.StepsAmount = stepUnitC_from.StepsAmount;
                        condUnitC_to.CondUnitType = condUnitC_from.CondUnitType;
                        twUnitC_to.Set(twUnitC_from);
                        ownUnitC_to.SetOwner(ownUnitC_from.Owner);
                        if (riverC_to.HaveNearRiver) thirUnitC_to.SetMaxWater(unitC_to.Unit);
                        WhereUnitsC.Add(ownUnitC_to.Owner, unitC_to.Unit, levUnitC_to.Level, idx_to);


                        WhereUnitsC.Remove(ownUnitC_from.Owner, unitC_from.Unit, levUnitC_from.Level, idx_from);
                        unitC_from.NoneUnit();
                        

                        if (!hpUnitC_to.HaveHp)
                        {
                            WhereUnitsC.Remove(ownUnitC_to.Owner, unitC_to.Unit, levUnitC_to.Level, idx_to);
                            unitC_to.NoneUnit();
                        }
                    }
                }

                else if (!hpUnitC_from.HaveHp)
                {
                    if (unitC_from.Is(UnitTypes.King))
                    {
                        ownUnitC_from.SetOwner(ownUnitC_to.Owner);
                        EndGameDataUIC.PlayerWinner = ownUnitC_to.Owner;
                    }

                    WhereUnitsC.Remove(ownUnitC_from.Owner, unitC_from.Unit, levUnitC_from.Level, idx_from);
                    unitC_from.NoneUnit();
                }

                hpUnitC_to.TryTakeBonusHp(effUnitC_to, unitC_to.Unit);
                hpUnitC_from.TryTakeBonusHp(effUnitC_from, unitC_from.Unit);

                effUnitC_from.DefAllEffects();
                effUnitC_to.DefAllEffects();
            }
        }
    }
}
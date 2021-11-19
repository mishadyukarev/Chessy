using Leopotam.Ecs;

namespace Game.Game
{
    public sealed class StatsUISystem : IEcsRunSystem
    {
        private EcsFilter<UnitC, LevelC, OwnerC> _unitF = default;
        private EcsFilter<HpC, DamageC, StepC, WaterUnitC> _statUnitF = default;
        private EcsFilter<ConditionUnitC, UnitEffectsC> _effUnitF = default;
        private EcsFilter<ToolWeaponC> _twUnitF = default;

        private EcsFilter<BuildC> _buildF = default;
        private EcsFilter<EnvC> _envF = default;

        public void Run()
        {
            ref var unit_sel = ref _unitF.Get1(SelIdx.Idx);
            ref var levUnit_sel = ref _unitF.Get2(SelIdx.Idx);
            ref var own_sel = ref _unitF.Get3(SelIdx.Idx);

            ref var selHpUnitC = ref _statUnitF.Get1(SelIdx.Idx);
            ref var selDamUnitC = ref _statUnitF.Get2(SelIdx.Idx);
            ref var selStepUnitC = ref _statUnitF.Get3(SelIdx.Idx);
            ref var thirUnitC_sel = ref _statUnitF.Get4(SelIdx.Idx);

            ref var selConUnitC = ref _effUnitF.Get1(SelIdx.Idx);
            ref var effUnit_sel = ref _effUnitF.Get2(SelIdx.Idx);

            ref var twUnit_sel = ref _twUnitF.Get1(SelIdx.Idx);
            
            



            ref var selBuildC = ref _buildF.Get1(SelIdx.Idx);
            ref var selEnvC = ref _envF.Get1(SelIdx.Idx);


            if (unit_sel.HaveUnit)
            {
                StatUIC.SetActiveStatZone(true);

                StatUIC.SetTextToStat(UnitStatTypes.Hp, selHpUnitC.Hp.ToString());
                StatUIC.SetTextToStat(UnitStatTypes.Damage, selDamUnitC.DamageOnCell(unit_sel.Unit, levUnit_sel.Level, selConUnitC, twUnit_sel, effUnit_sel, UnitUpgC.UpgPercent(UnitStatTypes.Damage, unit_sel.Unit, levUnit_sel.Level, own_sel.Owner), selBuildC.Build, selEnvC.Envronments).ToString());
                StatUIC.SetTextToStat(UnitStatTypes.Steps, selStepUnitC.Steps.ToString());
                StatUIC.SetTextToStat(UnitStatTypes.Water, thirUnitC_sel.Water.ToString());

                StatUIC.FillAmount(UnitStatTypes.Hp, selHpUnitC.Hp, HpC.MAX_HP);



                StatUIC.FillAmount(UnitStatTypes.Damage, 
                    selDamUnitC.DamageOnCell(unit_sel.Unit, levUnit_sel.Level, selConUnitC, twUnit_sel, effUnit_sel, 
                    UnitUpgC.UpgPercent(UnitStatTypes.Damage, unit_sel.Unit, levUnit_sel.Level, own_sel.Owner), selBuildC.Build, selEnvC.Envronments), 
                    selDamUnitC.DamageAttack(unit_sel.Unit, levUnit_sel.Level, twUnit_sel, effUnit_sel, AttackTypes.Simple, 
                    UnitUpgC.UpgPercent(UnitStatTypes.Damage, unit_sel.Unit, levUnit_sel.Level, own_sel.Owner)));



                StatUIC.FillAmount(UnitStatTypes.Steps, selStepUnitC.Steps, selStepUnitC.MaxSteps(unit_sel.Unit, effUnit_sel.Have(UnitStatTypes.Steps), UnitUpgC.Steps(unit_sel.Unit, levUnit_sel.Level, own_sel.Owner)));
                StatUIC.FillAmount(UnitStatTypes.Water, thirUnitC_sel.Water, thirUnitC_sel.MaxWater(UnitWaterUpgC.UpgPercent(own_sel.Owner, unit_sel.Unit)));
            }

            else
            {
                StatUIC.SetActiveStatZone(false);
            }
        }
    }
}
using Leopotam.Ecs;
using static Game.Game.EntityPool;

namespace Game.Game
{
    public sealed class StatsUISystem : IEcsRunSystem
    {
        private EcsFilter<UnitC, LevelC, OwnerC> _unitF = default;
        private EcsFilter<HpC, StepC, WaterC> _statUnitF = default;
        private EcsFilter<ConditionC, EffectsC> _effUnitF = default;
        private EcsFilter<ToolWeaponC> _twUnitF = default;

        public void Run()
        {
            ref var unit_sel = ref _unitF.Get1(SelIdx.Idx);
            ref var levUnit_sel = ref _unitF.Get2(SelIdx.Idx);
            ref var own_sel = ref _unitF.Get3(SelIdx.Idx);

            ref var selHpUnitC = ref _statUnitF.Get1(SelIdx.Idx);
            ref var selStepUnitC = ref _statUnitF.Get2(SelIdx.Idx);
            ref var thirUnitC_sel = ref _statUnitF.Get3(SelIdx.Idx);

            ref var selConUnitC = ref _effUnitF.Get1(SelIdx.Idx);
            ref var effUnit_sel = ref _effUnitF.Get2(SelIdx.Idx);

            ref var twUnit_sel = ref _twUnitF.Get1(SelIdx.Idx);
            
            



            ref var selBuildC = ref Build<BuildC>(SelIdx.Idx);
            ref var selEnvC = ref Environment<EnvC>(SelIdx.Idx);


            if (unit_sel.Have)
            {
                StatUIC.SetActiveStatZone(true);

                StatUIC.SetTextToStat(UnitStatTypes.Hp, selHpUnitC.HP.ToString());
                StatUIC.SetTextToStat(UnitStatTypes.Damage, UnitStat<UnitStatCellC>(SelIdx.Idx).DamageOnCell.ToString());
                StatUIC.SetTextToStat(UnitStatTypes.Steps, selStepUnitC.Steps.ToString());
                StatUIC.SetTextToStat(UnitStatTypes.Water, thirUnitC_sel.Water.ToString());

                StatUIC.FillAmount(UnitStatTypes.Hp, selHpUnitC.HP, HpC.MAX_HP);



                StatUIC.FillAmount(UnitStatTypes.Damage, UnitStat<UnitStatCellC>(SelIdx.Idx).DamageOnCell,
                    UnitStat<UnitStatCellC>(SelIdx.Idx).DamageAttack(AttackTypes.Simple));



                StatUIC.FillAmount(UnitStatTypes.Steps, selStepUnitC.Steps, UnitStat<UnitStatCellC>(SelIdx.Idx).MaxAmountSteps);
                StatUIC.FillAmount(UnitStatTypes.Water, thirUnitC_sel.Water, UnitStat<UnitStatCellC>(SelIdx.Idx).MaxWater);
            }

            else
            {
                StatUIC.SetActiveStatZone(false);
            }
        }
    }
}
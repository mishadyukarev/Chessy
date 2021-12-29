using Leopotam.Ecs;
using static Game.Game.EntityCellPool;
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
            var selIdx = SelIdx<IdxC>().Idx;

            ref var unit_sel = ref _unitF.Get1(selIdx);
            ref var levUnit_sel = ref _unitF.Get2(selIdx);
            ref var own_sel = ref _unitF.Get3(selIdx);

            ref var selHpUnitC = ref _statUnitF.Get1(selIdx);
            ref var selStepUnitC = ref _statUnitF.Get2(selIdx);
            ref var thirUnitC_sel = ref _statUnitF.Get3(selIdx);

            ref var selConUnitC = ref _effUnitF.Get1(selIdx);
            ref var effUnit_sel = ref _effUnitF.Get2(selIdx);

            ref var twUnit_sel = ref _twUnitF.Get1(selIdx);
            
            



            ref var selBuildC = ref Build<BuildC>(selIdx);
            ref var selEnvC = ref Environment<EnvC>(selIdx);


            if (unit_sel.Have)
            {
                StatUIC.SetActiveStatZone(true);

                StatUIC.SetTextToStat(UnitStatTypes.Hp, selHpUnitC.Hp.ToString());
                StatUIC.SetTextToStat(UnitStatTypes.Damage, Unit<DamageUnitC>(selIdx).DamageOnCell.ToString());
                StatUIC.SetTextToStat(UnitStatTypes.Steps, selStepUnitC.Steps.ToString());
                StatUIC.SetTextToStat(UnitStatTypes.Water, thirUnitC_sel.Water.ToString());

                StatUIC.FillAmount(UnitStatTypes.Hp, selHpUnitC.Hp, HpUnitWC.MAX);



                StatUIC.FillAmount(UnitStatTypes.Damage, Unit<DamageUnitC>(selIdx).DamageOnCell,
                    Unit<DamageUnitC>(selIdx).DamageAttack(AttackTypes.Simple));



                StatUIC.FillAmount(UnitStatTypes.Steps, selStepUnitC.Steps, Unit<StepUnitWC>(selIdx).MaxAmountSteps);
                StatUIC.FillAmount(UnitStatTypes.Water, thirUnitC_sel.Water, Unit<WaterUnitC>(selIdx).MaxWater);
            }

            else
            {
                StatUIC.SetActiveStatZone(false);
            }
        }
    }
}
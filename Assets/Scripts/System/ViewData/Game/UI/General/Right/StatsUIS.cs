using static Game.Game.EntityCellUnitPool;
using static Game.Game.EntityPool;

namespace Game.Game
{
    struct StatsUIS : IEcsRunSystem
    {
        public void Run()
        {
            var selIdx = SelIdx<IdxC>().Idx;

            ref var unit_sel = ref Unit<UnitC>(selIdx);

            ref var hpUnit_sel = ref Unit<HpC>(selIdx);
            ref var stepUnit_sel = ref Unit<StepC>(selIdx);
            ref var waterUnit_sel = ref Unit<WaterC>(selIdx);


            if (unit_sel.Have)
            {
                StatUIC.SetActiveStatZone(true);

                StatUIC.SetTextToStat(UnitStatTypes.Hp, hpUnit_sel.Hp.ToString());
                StatUIC.SetTextToStat(UnitStatTypes.Damage, Unit<UnitCellEC>(selIdx).DamageOnCell.ToString());
                StatUIC.SetTextToStat(UnitStatTypes.Steps, stepUnit_sel.Steps.ToString());
                StatUIC.SetTextToStat(UnitStatTypes.Water, waterUnit_sel.Water.ToString());

                StatUIC.FillAmount(UnitStatTypes.Hp, hpUnit_sel.Hp, UnitCellEC.MAX_HP);



                StatUIC.FillAmount(UnitStatTypes.Damage, Unit<UnitCellEC>(selIdx).DamageOnCell,
                    Unit<UnitCellEC>(selIdx).DamageAttack(AttackTypes.Simple));



                StatUIC.FillAmount(UnitStatTypes.Steps, stepUnit_sel.Steps, Unit<UnitCellEC>(selIdx).MaxAmountSteps);
                StatUIC.FillAmount(UnitStatTypes.Water, waterUnit_sel.Water, Unit<UnitCellEC>(selIdx).MaxWater);
            }

            else
            {
                StatUIC.SetActiveStatZone(false);
            }
        }
    }
}
using static Game.Game.CellUnitE;
using static Game.Game.EntityPool;
using static Game.Game.UIEntRightStats;

namespace Game.Game
{
    struct StatsUIS : IEcsRunSystem
    {
        public void Run()
        {
            var selIdx = SelIdx<IdxC>().Idx;

            ref var unit_sel = ref Unit<UnitTC>(selIdx);

            ref var hpUnit_sel = ref Unit<HpC>(selIdx);
            ref var stepUnit_sel = ref Unit<StepC>(selIdx);
            ref var waterUnit_sel = ref Unit<WaterC>(selIdx);


            if (unit_sel.Have)
            {
                Stat<ImageUIC>(UnitStatTypes.Hp).SetActiveParent(true);
                Stat<ImageUIC>(UnitStatTypes.Damage).SetActiveParent(true);
                Stat<ImageUIC>(UnitStatTypes.Steps).SetActiveParent(true);
                Stat<ImageUIC>(UnitStatTypes.Water).SetActiveParent(true);


                Stat<TextMPUGUIC>(UnitStatTypes.Hp).Text = hpUnit_sel.Hp.ToString();
                Stat<TextMPUGUIC>(UnitStatTypes.Damage).Text = Unit<UnitCellEC>(selIdx).DamageOnCell.ToString();
                Stat<TextMPUGUIC>(UnitStatTypes.Steps).Text = stepUnit_sel.Steps.ToString();
                Stat<TextMPUGUIC>(UnitStatTypes.Water).Text = waterUnit_sel.Water.ToString();

                //UIEntRightStats.FillAmount(UnitStatTypes.Hp, hpUnit_sel.Hp, UnitCellEC.MAX_HP);



                //UIEntRightStats.FillAmount(UnitStatTypes.Damage, Unit<UnitCellEC>(selIdx).DamageOnCell,
                //    Unit<UnitCellEC>(selIdx).DamageAttack(AttackTypes.Simple));



                //UIEntRightStats.FillAmount(UnitStatTypes.Steps, stepUnit_sel.Steps, Unit<UnitCellEC>(selIdx).MaxAmountSteps);
                //UIEntRightStats.FillAmount(UnitStatTypes.Water, waterUnit_sel.Water, Unit<UnitCellEC>(selIdx).MaxWater);
            }

            else
            {
                Stat<ImageUIC>(UnitStatTypes.Hp).SetActiveParent(false);
                Stat<ImageUIC>(UnitStatTypes.Damage).SetActiveParent(false);
                Stat<ImageUIC>(UnitStatTypes.Steps).SetActiveParent(false);
                Stat<ImageUIC>(UnitStatTypes.Water).SetActiveParent(false);
            }
        }
    }
}
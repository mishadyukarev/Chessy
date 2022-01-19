using static Game.Game.CellUnitEs;
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

            ref var hpUnit_sel = ref CellUnitHpEs.Hp<AmountC>(selIdx);
            ref var stepUnit_sel = ref CellUnitStepEs.Steps<AmountC>(selIdx);
            ref var waterUnit_sel = ref CellUnitWaterEs.Water<AmountC>(selIdx);


            if (unit_sel.Have)
            {
                Stat<ImageUIC>(UnitStatTypes.Hp).SetActiveParent(true);
                Stat<ImageUIC>(UnitStatTypes.Damage).SetActiveParent(true);
                Stat<ImageUIC>(UnitStatTypes.Steps).SetActiveParent(true);
                Stat<ImageUIC>(UnitStatTypes.Water).SetActiveParent(true);


                Stat<TextMPUGUIC>(UnitStatTypes.Hp).Text = hpUnit_sel.Amount.ToString();
                Stat<TextMPUGUIC>(UnitStatTypes.Damage).Text = DamageOnCell(selIdx).ToString();
                Stat<TextMPUGUIC>(UnitStatTypes.Steps).Text = stepUnit_sel.Amount.ToString();
                Stat<TextMPUGUIC>(UnitStatTypes.Water).Text = waterUnit_sel.Amount.ToString();




                UIEntRightStats.Stat<ImageUIC>(UnitStatTypes.Hp).FillAmount = hpUnit_sel.Amount / 100f;



                UIEntRightStats.Stat<ImageUIC>(UnitStatTypes.Damage).FillAmount 
                    = (float)(DamageOnCell(selIdx) / (float)DamageAttack(selIdx, AttackTypes.Simple));

                Stat<ImageUIC>(UnitStatTypes.Steps).FillAmount = (float)stepUnit_sel.Amount / (float)CellUnitStepEs.MaxAmountSteps(selIdx);
                UIEntRightStats.Stat<ImageUIC>(UnitStatTypes.Water).FillAmount = (float)waterUnit_sel.Amount / (float)CellUnitWaterEs.MaxWater(selIdx);
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
using static Game.Game.CellUnitEntities;
using static Game.Game.EntityPool;
using static Game.Game.UIEntRightStats;

namespace Game.Game
{
    struct StatsUIS : IEcsRunSystem
    {
        public void Run()
        {
            var selIdx = EntitiesPool.SelectedIdxE.IdxC.Idx;

            ref var unit_sel = ref CellUnitEntities.Else(selIdx).UnitC;

            ref var hpUnit_sel = ref CellUnitEntities.Hp(selIdx).AmountC;
            ref var stepUnit_sel = ref CellUnitEntities.Step(selIdx).AmountC;
            ref var waterUnit_sel = ref CellUnitEntities.Water(selIdx).AmountC;


            if (unit_sel.Have)
            {
                Stat<ImageUIC>(UnitStatTypes.Hp).SetActiveParent(true);
                Stat<ImageUIC>(UnitStatTypes.Damage).SetActiveParent(true);
                Stat<ImageUIC>(UnitStatTypes.Steps).SetActiveParent(true);
                Stat<ImageUIC>(UnitStatTypes.Water).SetActiveParent(true);


                Stat<TextUIC>(UnitStatTypes.Hp).Text = hpUnit_sel.Amount.ToString();
                Stat<TextUIC>(UnitStatTypes.Damage).Text = DamageOnCell(selIdx).ToString();
                Stat<TextUIC>(UnitStatTypes.Steps).Text = stepUnit_sel.Amount.ToString();
                Stat<TextUIC>(UnitStatTypes.Water).Text = waterUnit_sel.Amount.ToString();




                UIEntRightStats.Stat<ImageUIC>(UnitStatTypes.Hp).FillAmount = hpUnit_sel.Amount / 100f;



                UIEntRightStats.Stat<ImageUIC>(UnitStatTypes.Damage).FillAmount 
                    = (float)(DamageOnCell(selIdx) / (float)DamageAttack(selIdx, AttackTypes.Simple));

                Stat<ImageUIC>(UnitStatTypes.Steps).FillAmount = (float)stepUnit_sel.Amount / (float)CellUnitEntities.MaxAmountSteps(selIdx);
                UIEntRightStats.Stat<ImageUIC>(UnitStatTypes.Water).FillAmount = (float)waterUnit_sel.Amount / (float)CellUnitEntities.MaxWater(selIdx);
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
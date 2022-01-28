using static Game.Game.CellUnitEs;
using static Game.Game.UIEntRightStats;

namespace Game.Game
{
    struct StatsUIS : IEcsRunSystem
    {
        public void Run()
        {
            var selIdx = Entities.SelectedIdxE.IdxC.Idx;

            ref var unit_sel = ref Entities.CellEs.UnitEs.Else(selIdx).UnitC;

            ref var hpUnit_sel = ref Entities.CellEs.UnitEs.Hp(selIdx).AmountC;
            ref var stepUnit_sel = ref Entities.CellEs.UnitEs.Step(selIdx).Steps;
            ref var waterUnit_sel = ref Entities.CellEs.UnitEs.Water(selIdx).AmountC;


            if (unit_sel.Have)
            {
                Stat<ImageUIC>(UnitStatTypes.Hp).SetActiveParent(true);
                Stat<ImageUIC>(UnitStatTypes.Damage).SetActiveParent(true);
                Stat<ImageUIC>(UnitStatTypes.Steps).SetActiveParent(true);
                Stat<ImageUIC>(UnitStatTypes.Water).SetActiveParent(true);


                Stat<TextUIC>(UnitStatTypes.Hp).Text = hpUnit_sel.Amount.ToString();
                Stat<TextUIC>(UnitStatTypes.Damage).Text = Entities.CellEs.UnitEs.DamageOnCell(selIdx).ToString();
                Stat<TextUIC>(UnitStatTypes.Steps).Text = stepUnit_sel.Amount.ToString();
                Stat<TextUIC>(UnitStatTypes.Water).Text = waterUnit_sel.Amount.ToString();




                UIEntRightStats.Stat<ImageUIC>(UnitStatTypes.Hp).FillAmount = hpUnit_sel.Amount / 100f;



                UIEntRightStats.Stat<ImageUIC>(UnitStatTypes.Damage).FillAmount
                    = (float)(Entities.CellEs.UnitEs.DamageOnCell(selIdx) / (float)Entities.CellEs.UnitEs.DamageAttack(selIdx, AttackTypes.Simple));

                Stat<ImageUIC>(UnitStatTypes.Steps).FillAmount = (float)stepUnit_sel.Amount / (float)Entities.CellEs.UnitEs.Step(selIdx).MaxAmountSteps(Entities.CellEs.UnitEs.Else(selIdx));
                UIEntRightStats.Stat<ImageUIC>(UnitStatTypes.Water).FillAmount = (float)waterUnit_sel.Amount / (float)Entities.CellEs.UnitEs.MaxWater(selIdx);
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
using static Game.Game.UIEntRightStats;

namespace Game.Game
{
    sealed class StatsUIS : SystemViewAbstract, IEcsRunSystem
    {
        internal StatsUIS(in Entities ents, in EntitiesView entsView) : base(ents, entsView)
        {
        }

        public void Run()
        {
            var cellEs = Es.CellEs;
            var unitEs = cellEs.UnitEs;


            var idx_sel = Es.SelectedIdxE.IdxC.Idx;

            var hpUnit_sel = UnitEs.StatEs.Hp(idx_sel).Health;
            var stepUnit_sel = UnitEs.StatEs.Step(idx_sel).Steps;
            var waterUnit_sel = UnitEs.StatEs.Water(idx_sel).Water;


            if (UnitEs.Main(idx_sel).HaveUnit(UnitStatEs))
            {
                var damageOnCell = unitEs.Main(idx_sel).DamageOnCell(CellEs, Es.UnitStatUpgradesEs);
                var damageAttack = unitEs.Main(idx_sel).DamageAttack(CellEs, Es.UnitStatUpgradesEs, AttackTypes.Simple);


                Stat<ImageUIC>(UnitStatTypes.Hp).SetActiveParent(true);
                Stat<ImageUIC>(UnitStatTypes.Damage).SetActiveParent(true);
                Stat<ImageUIC>(UnitStatTypes.Steps).SetActiveParent(true);
                Stat<ImageUIC>(UnitStatTypes.Water).SetActiveParent(true);


                Stat<TextUIC>(UnitStatTypes.Hp).Text = hpUnit_sel.Amount.ToString();
                Stat<TextUIC>(UnitStatTypes.Damage).Text = damageOnCell.ToString();
                Stat<TextUIC>(UnitStatTypes.Steps).Text = stepUnit_sel.Amount.ToString();
                Stat<TextUIC>(UnitStatTypes.Water).Text = waterUnit_sel.Amount.ToString();




                UIEntRightStats.Stat<ImageUIC>(UnitStatTypes.Hp).FillAmount = hpUnit_sel.Amount / 100f;



                UIEntRightStats.Stat<ImageUIC>(UnitStatTypes.Damage).FillAmount = (float)(damageOnCell / (float)damageAttack);

                Stat<ImageUIC>(UnitStatTypes.Steps).FillAmount = (float)stepUnit_sel.Amount / (float)UnitEs.StatEs.Step(idx_sel).MaxAmountSteps(UnitEs.Main(idx_sel));
                UIEntRightStats.Stat<ImageUIC>(UnitStatTypes.Water).FillAmount = (float)waterUnit_sel.Amount / (float)UnitEs.StatEs.Water(idx_sel).MaxWater(UnitEs.Main(idx_sel), Es.UnitStatUpgradesEs);
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
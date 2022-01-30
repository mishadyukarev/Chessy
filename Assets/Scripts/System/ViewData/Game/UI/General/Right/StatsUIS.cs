using static Game.Game.UIEntRightStats;

namespace Game.Game
{
    sealed class StatsUIS : SystemViewAbstract, IEcsRunSystem
    {
        public StatsUIS(in Entities ents, in EntitiesView entsView) : base(ents, entsView)
        {
        }

        public void Run()
        {
            var cellEs = Es.CellEs;
            var unitEs = cellEs.UnitEs;
            var buildEs = cellEs.BuildEs;
            var envEs = cellEs.EnvironmentEs;



            var idx_sel = Es.SelectedIdxE.IdxC.Idx;

            ref var unit_sel = ref Es.CellEs.UnitEs.Main(idx_sel).UnitC;

            ref var hpUnit_sel = ref Es.CellEs.UnitEs.StatEs.Hp(idx_sel).Health;
            ref var stepUnit_sel = ref Es.CellEs.UnitEs.StatEs.Step(idx_sel).Steps;
            ref var waterUnit_sel = ref Es.CellEs.UnitEs.StatEs.Water(idx_sel).Water;


            if (unit_sel.Have)
            {
                var damageOnCell = unitEs.DamageOnCell(idx_sel, cellEs, Es.UnitStatUpgradesEs);
                var damageAttack = unitEs.DamageAttack(idx_sel, Es.UnitStatUpgradesEs, AttackTypes.Simple);


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

                Stat<ImageUIC>(UnitStatTypes.Steps).FillAmount = (float)stepUnit_sel.Amount / (float)Es.CellEs.UnitEs.StatEs.Step(idx_sel).MaxAmountSteps(Es.CellEs.UnitEs.Main(idx_sel));
                UIEntRightStats.Stat<ImageUIC>(UnitStatTypes.Water).FillAmount = (float)waterUnit_sel.Amount / (float)Es.CellEs.UnitEs.StatEs.Water(idx_sel).MaxWater(Es.CellEs.UnitEs.Main(idx_sel), Es.UnitStatUpgradesEs);
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
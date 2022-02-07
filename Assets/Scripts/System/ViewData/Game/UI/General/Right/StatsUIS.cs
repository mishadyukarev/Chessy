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
            var idx_sel = Es.SelectedIdxE.IdxC.Idx;

            var hpUnit_sel = UnitEs(idx_sel).StatEs.Hp.HealthC;
            var stepUnit_sel = UnitEs(idx_sel).StatEs.StepE.StepsC;
            var waterUnit_sel = UnitEs(idx_sel).StatEs.WaterE.WaterC;


            if (UnitEs(idx_sel).TypeE.HaveUnit)
            {
                var damageOnCell = UnitEs(idx_sel).DamageOnCell(CellEs(idx_sel), Es.UnitStatUpgradesEs);
                var damageAttack = UnitEs(idx_sel).DamageAttack(CellEs(idx_sel), Es.UnitStatUpgradesEs, AttackTypes.Simple);


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

                Stat<ImageUIC>(UnitStatTypes.Steps).FillAmount = (float)stepUnit_sel.Amount / (float)UnitStatEs(idx_sel).StepE.MaxAmountSteps(UnitEs(idx_sel).TypeE);
                UIEntRightStats.Stat<ImageUIC>(UnitStatTypes.Water).FillAmount = (float)waterUnit_sel.Amount / (float)UnitStatEs(idx_sel).WaterE.MaxWater(UnitEs(idx_sel), Es.UnitStatUpgradesEs);
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
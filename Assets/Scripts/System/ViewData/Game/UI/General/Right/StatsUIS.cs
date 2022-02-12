using static Game.Game.UIEntRightStats;

namespace Game.Game
{
    sealed class StatsUIS : SystemUIAbstract, IEcsRunSystem
    {
        internal StatsUIS(in Entities ents, in EntitiesUI entsUI) : base(ents, entsUI)
        {
        }

        public void Run()
        {
            var idx_sel = Es.SelectedIdxE.IdxC.Idx;


            if (Es.UnitEs(idx_sel).UnitE.HaveUnit)
            {
                var damageOnCell = Es.UnitE(idx_sel).DamageOnCell(Es.CellEs(idx_sel), Es.UnitStatUpgradesEs);
                var damageAttack = Es.UnitE(idx_sel).DamageAttack(Es.UnitEs(idx_sel).ExtraToolWeaponE, Es.UnitStatUpgradesEs, AttackTypes.Simple);


                Stat<ImageUIC>(UnitStatTypes.Hp).SetActiveParent(true);
                Stat<ImageUIC>(UnitStatTypes.Damage).SetActiveParent(true);
                Stat<ImageUIC>(UnitStatTypes.Steps).SetActiveParent(true);
                Stat<ImageUIC>(UnitStatTypes.Water).SetActiveParent(true);


                Stat<TextUIC>(UnitStatTypes.Hp).Text = Es.UnitE(idx_sel).Health.ToString();
                Stat<TextUIC>(UnitStatTypes.Damage).Text = damageOnCell.ToString();
                Stat<TextUIC>(UnitStatTypes.Steps).Text = Es.UnitE(idx_sel).Steps.ToString();
                Stat<TextUIC>(UnitStatTypes.Water).Text = Es.UnitE(idx_sel).Water.ToString();




                UIEntRightStats.Stat<ImageUIC>(UnitStatTypes.Hp).FillAmount = Es.UnitE(idx_sel).Health / 100f;



                UIEntRightStats.Stat<ImageUIC>(UnitStatTypes.Damage).FillAmount = (float)(damageOnCell / (float)damageAttack);

                Stat<ImageUIC>(UnitStatTypes.Steps).FillAmount = (float)Es.UnitE(idx_sel).Steps / (float)Es.UnitE(idx_sel).MaxAmountSteps;
                UIEntRightStats.Stat<ImageUIC>(UnitStatTypes.Water).FillAmount = Es.UnitE(idx_sel).Water / (float)Es.UnitE(idx_sel).MaxWater(Es.UnitStatUpgradesEs);
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
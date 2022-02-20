using static Game.Game.UIEntRightStats;

namespace Game.Game
{
    sealed class StatsUIS : SystemUIAbstract, IEcsRunSystem
    {
        internal StatsUIS(in Entities ents, in EntitiesViewUI entsUI) : base(ents, entsUI)
        {
        }

        public void Run()
        {
            var idx_sel = E.SelectedIdxC.Idx;


            if (E.UnitTC(idx_sel).HaveUnit)
            {
                var damageOnCell = E.UnitDamageOnCellC(idx_sel).Damage;
                var damageAttack = E.UnitDamageAttackC(idx_sel).Damage;


                Stat<ImageUIC>(UnitStatTypes.Hp).SetActiveParent(true);
                Stat<ImageUIC>(UnitStatTypes.Damage).SetActiveParent(true);
                Stat<ImageUIC>(UnitStatTypes.Steps).SetActiveParent(true);
                Stat<ImageUIC>(UnitStatTypes.Water).SetActiveParent(true);


                Stat<TextUIC>(UnitStatTypes.Hp).Text = ((int)(E.UnitHpC(idx_sel).Health * 100)).ToString();
                Stat<TextUIC>(UnitStatTypes.Damage).Text = damageOnCell.ToString();
                Stat<TextUIC>(UnitStatTypes.Steps).Text = E.UnitStepC(idx_sel).Steps.ToString();
                Stat<TextUIC>(UnitStatTypes.Water).Text = E.UnitWaterC(idx_sel).Water.ToString();


                var v = E.UnitHpC(idx_sel).Health / CellUnitStatHp_Values.MAX_HP;

                UIEntRightStats.Stat<ImageUIC>(UnitStatTypes.Hp).FillAmount = E.UnitHpC(idx_sel).Health / CellUnitStatHp_Values.MAX_HP;



                UIEntRightStats.Stat<ImageUIC>(UnitStatTypes.Damage).FillAmount = (float)(damageOnCell / (float)damageAttack);

                Stat<ImageUIC>(UnitStatTypes.Steps).FillAmount = (float)E.UnitStepC(idx_sel).Steps / CellUnitStatStep_Values.StandartForUnit(E.UnitTC(idx_sel).Unit);
                UIEntRightStats.Stat<ImageUIC>(UnitStatTypes.Water).FillAmount = E.UnitWaterC(idx_sel).Water / (float)CellUnitStatWater_Values.MAX;
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
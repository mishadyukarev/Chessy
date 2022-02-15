﻿using static Game.Game.UIEntRightStats;

namespace Game.Game
{
    sealed class StatsUIS : SystemUIAbstract, IEcsRunSystem
    {
        internal StatsUIS(in Entities ents, in EntitiesViewUI entsUI) : base(ents, entsUI)
        {
        }

        public void Run()
        {
            var idx_sel = Es.SelectedIdxC.Idx;


            if (Es.UnitTC(idx_sel).HaveUnit)
            {
                var damageOnCell = Es.UnitE(idx_sel).DamageOnCell(Es.CellEs(idx_sel), Es.UnitStatUpgradesEs);
                var damageAttack = Es.UnitE(idx_sel).DamageAttack(Es.UnitEs(idx_sel), Es.UnitStatUpgradesEs, AttackTypes.Simple);


                Stat<ImageUIC>(UnitStatTypes.Hp).SetActiveParent(true);
                Stat<ImageUIC>(UnitStatTypes.Damage).SetActiveParent(true);
                Stat<ImageUIC>(UnitStatTypes.Steps).SetActiveParent(true);
                Stat<ImageUIC>(UnitStatTypes.Water).SetActiveParent(true);


                Stat<TextUIC>(UnitStatTypes.Hp).Text = ((int)(Es.UnitHpC(idx_sel).Health * 100)).ToString();
                Stat<TextUIC>(UnitStatTypes.Damage).Text = damageOnCell.ToString();
                Stat<TextUIC>(UnitStatTypes.Steps).Text = Es.UnitStepC(idx_sel).Steps.ToString();
                Stat<TextUIC>(UnitStatTypes.Water).Text = Es.UnitWaterC(idx_sel).Water.ToString();


                var v = Es.UnitHpC(idx_sel).Health / CellUnitStatHpValues.MAX_HP;

                UIEntRightStats.Stat<ImageUIC>(UnitStatTypes.Hp).FillAmount = Es.UnitHpC(idx_sel).Health / CellUnitStatHpValues.MAX_HP;



                UIEntRightStats.Stat<ImageUIC>(UnitStatTypes.Damage).FillAmount = (float)(damageOnCell / (float)damageAttack);

                Stat<ImageUIC>(UnitStatTypes.Steps).FillAmount = (float)Es.UnitStepC(idx_sel).Steps / CellUnitStatStepValues.StandartStepsUnit(Es.UnitTC(idx_sel).Unit);
                UIEntRightStats.Stat<ImageUIC>(UnitStatTypes.Water).FillAmount = Es.UnitWaterC(idx_sel).Water / (float)CellUnitStatWaterValues.WATER_MAX_STANDART;
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
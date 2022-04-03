using Chessy.Game.Values.Cell.Unit.Stats;
using System;

namespace Chessy.Game
{
    sealed class StatsUIS : SystemUIAbstract, IEcsRunSystem
    {
        readonly EntitiesViewUIGame eUI;

        internal StatsUIS(in EntitiesViewUIGame entsUI, in Chessy.Game.Model.Entity.EntitiesModelGame ents) : base(ents)
        {
            eUI = entsUI;
        }

        public void Run()
        {
            var idx_sel = e.CellsC.Selected;


            if (e.UnitTC(idx_sel).HaveUnit && !e.UnitTC(idx_sel).IsAnimal)
            {
                var damageOnCell = e.DamageOnCellC(idx_sel).Damage;
                var damageAttack = e.DamageAttackC(idx_sel).Damage;


                eUI.RightEs.StatsE.Stat(UnitStatTypes.Hp).ImageUIC.SetActiveParent(true);
                eUI.RightEs.StatsE.Stat(UnitStatTypes.Damage).ImageUIC.SetActiveParent(true);
                eUI.RightEs.StatsE.Stat(UnitStatTypes.Steps).ImageUIC.SetActiveParent(true);


                if (e.UnitTC(idx_sel).Is(UnitTypes.Elfemale))
                {
                    eUI.RightEs.StatsE.Stat(UnitStatTypes.Water).ImageUIC.SetActiveParent(false);
                }
                else
                {
                    eUI.RightEs.StatsE.Stat(UnitStatTypes.Water).ImageUIC.SetActiveParent(true);
                }




                eUI.RightEs.StatsE.Stat(UnitStatTypes.Hp).TextUIC.TextUI.text = Math.Truncate(100 * e.UnitHpC(idx_sel).Health).ToString();
                eUI.RightEs.StatsE.Stat(UnitStatTypes.Damage).TextUIC.TextUI.text = (Math.Truncate(10 * damageOnCell) / 10).ToString();
                eUI.RightEs.StatsE.Stat(UnitStatTypes.Steps).TextUIC.TextUI.text = (Math.Truncate(100 * e.UnitStepC(idx_sel).Steps) / 100).ToString();
                eUI.RightEs.StatsE.Stat(UnitStatTypes.Water).TextUIC.TextUI.text = (Math.Truncate(100 * e.UnitWaterC(idx_sel).Water) / 100).ToString();

                var v = e.UnitHpC(idx_sel).Health / HpValues.MAX;

                eUI.RightEs.StatsE.Stat(UnitStatTypes.Hp).ImageUIC.Image.fillAmount = e.UnitHpC(idx_sel).Health / HpValues.MAX;



                eUI.RightEs.StatsE.Stat(UnitStatTypes.Damage).ImageUIC.Image.fillAmount = (float)(damageOnCell / (float)damageAttack);

                eUI.RightEs.StatsE.Stat(UnitStatTypes.Steps).ImageUIC.Image.fillAmount = (float)e.UnitStepC(idx_sel).Steps / StepValues.MAX;
                eUI.RightEs.StatsE.Stat(UnitStatTypes.Water).ImageUIC.Image.fillAmount = e.UnitWaterC(idx_sel).Water / (float)WaterValues.MAX;
            }

            else
            {
                eUI.RightEs.StatsE.Stat(UnitStatTypes.Hp).ImageUIC.SetActiveParent(false);
                eUI.RightEs.StatsE.Stat(UnitStatTypes.Damage).ImageUIC.SetActiveParent(false);
                eUI.RightEs.StatsE.Stat(UnitStatTypes.Steps).ImageUIC.SetActiveParent(false);
                eUI.RightEs.StatsE.Stat(UnitStatTypes.Water).ImageUIC.SetActiveParent(false);
            }
        }
    }
}
using Chessy.Game.Values.Cell.Unit.Stats;
using System;

namespace Chessy.Game
{
    sealed class StatsUIS : SystemUIAbstract, IEcsRunSystem
    {
        internal StatsUIS(in EntitiesViewUI entsUI, in EntitiesModel ents) : base(entsUI, ents)
        {
        }

        public void Run()
        {
            var idx_sel = E.SelectedIdxC.Idx;


            if (E.UnitTC(idx_sel).HaveUnit && !E.IsAnimal(E.UnitTC(idx_sel).Unit))
            {
                var damageOnCell = E.UnitDamageOnCellC(idx_sel).Damage;
                var damageAttack = E.UnitDamageAttackC(idx_sel).Damage;


                UIE.RightEs.StatsE.Stat(UnitStatTypes.Hp).ImageUIC.SetActiveParent(true);
                UIE.RightEs.StatsE.Stat(UnitStatTypes.Damage).ImageUIC.SetActiveParent(true);
                UIE.RightEs.StatsE.Stat(UnitStatTypes.Steps).ImageUIC.SetActiveParent(true);
                UIE.RightEs.StatsE.Stat(UnitStatTypes.Water).ImageUIC.SetActiveParent(true);


                UIE.RightEs.StatsE.Stat(UnitStatTypes.Hp).TextUIC.TextUI.text = Math.Truncate(100 * E.UnitHpC(idx_sel).Health).ToString();
                UIE.RightEs.StatsE.Stat(UnitStatTypes.Damage).TextUIC.TextUI.text = (Math.Truncate(10 * damageOnCell) / 10).ToString();
                UIE.RightEs.StatsE.Stat(UnitStatTypes.Steps).TextUIC.TextUI.text = (Math.Truncate(100 * E.UnitStepC(idx_sel).Steps) / 100).ToString();
                UIE.RightEs.StatsE.Stat(UnitStatTypes.Water).TextUIC.TextUI.text = (Math.Truncate(100 * E.UnitWaterC(idx_sel).Water) / 100).ToString();

                var v = E.UnitHpC(idx_sel).Health / HpValues.MAX;

                UIE.RightEs.StatsE.Stat(UnitStatTypes.Hp).ImageUIC.Image.fillAmount = E.UnitHpC(idx_sel).Health / HpValues.MAX;



                UIE.RightEs.StatsE.Stat(UnitStatTypes.Damage).ImageUIC.Image.fillAmount = (float)(damageOnCell / (float)damageAttack);

                UIE.RightEs.StatsE.Stat(UnitStatTypes.Steps).ImageUIC.Image.fillAmount = (float)E.UnitStepC(idx_sel).Steps / StepValues.MAX;
                UIE.RightEs.StatsE.Stat(UnitStatTypes.Water).ImageUIC.Image.fillAmount = E.UnitWaterC(idx_sel).Water / (float)WaterValues.MAX;
            }

            else
            {
                UIE.RightEs.StatsE.Stat(UnitStatTypes.Hp).ImageUIC.SetActiveParent(false);
                UIE.RightEs.StatsE.Stat(UnitStatTypes.Damage).ImageUIC.SetActiveParent(false);
                UIE.RightEs.StatsE.Stat(UnitStatTypes.Steps).ImageUIC.SetActiveParent(false);
                UIE.RightEs.StatsE.Stat(UnitStatTypes.Water).ImageUIC.SetActiveParent(false);
            }
        }
    }
}
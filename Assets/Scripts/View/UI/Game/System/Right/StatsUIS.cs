using Chessy.Game.Model.Entity;
using Chessy.Game.Values.Cell.Unit.Stats;
using System;

namespace Chessy.Game
{
    sealed class StatsUIS : SystemUIAbstract
    {
        readonly RightStatsUIEs _statsUIE;

        internal StatsUIS(in RightStatsUIEs rightStatsUIE, in EntitiesModelGame ents) : base(ents)
        {
            _statsUIE = rightStatsUIE;
        }

        internal override void Sync()
        {
            var idx_sel = e.CellsC.Selected;


            if (e.UnitTC(idx_sel).HaveUnit && !e.UnitTC(idx_sel).IsAnimal)
            {
                var damageOnCell = e.DamageOnCellC(idx_sel).Damage;
                var damageAttack = e.DamageAttackC(idx_sel).Damage;



                var needActiveWater = false;
                var needActiveHp = false;
                var needActiveDamage = false;


                if (!e.LessonTC.HaveLesson || e.LessonT >= Enum.LessonTypes.Install3WarriorsNextToTheRiver)
                {
                    needActiveHp = true;
                }

                if (!e.LessonTC.HaveLesson || e.LessonT >= Enum.LessonTypes.ThatsYourDamage)
                {
                    needActiveDamage = true;
                }

                if (!e.LessonTC.HaveLesson || e.LessonTC.LessonT >= Enum.LessonTypes.Install3WarriorsNextToTheRiver)
                {
                    if (!e.UnitTC(idx_sel).Is(UnitTypes.Elfemale))
                    {
                        needActiveWater = true;
                    }
                }


                _statsUIE.WaterE.ImageC.SetActiveParent(needActiveWater);
                _statsUIE.Stat(UnitStatTypes.Hp).ImageC.SetActiveParent(needActiveHp);
                _statsUIE.DamageE.ImageC.SetActiveParent(needActiveDamage);
                _statsUIE.EnergyE.ImageUIC.SetActiveParent(true);




                _statsUIE.Stat(UnitStatTypes.Hp).TextC.TextUI.text = Math.Truncate(100 * e.HpUnitC(idx_sel).Health).ToString();
                _statsUIE.DamageE.TextC.TextUI.text = (Math.Truncate(10 * damageAttack) / 10) + "/" + (Math.Truncate(10 * damageOnCell) / 10).ToString();
                _statsUIE.EnergyE.TextUIC.TextUI.text = (Math.Truncate(100 * e.StepUnitC(idx_sel).Steps) / 100).ToString();
                _statsUIE.WaterE.TextC.TextUI.text = (Math.Truncate(100 * e.WaterUnitC(idx_sel).Water) / 100).ToString();

                _statsUIE.Stat(UnitStatTypes.Hp).ImageC.Image.fillAmount = (float)(e.HpUnitC(idx_sel).Health / HpValues.MAX);



                _statsUIE.DamageE.ImageC.Image.fillAmount = (float)(damageOnCell / (float)damageAttack);

                _statsUIE.EnergyE.ImageUIC.Image.fillAmount = (float)e.StepUnitC(idx_sel).Steps / StepValues.MAX;
                _statsUIE.WaterE.ImageC.Image.fillAmount = (float)(e.WaterUnitC(idx_sel).Water / WaterValues.MAX);
            }

            else
            {
                _statsUIE.Stat(UnitStatTypes.Hp).ImageC.SetActiveParent(false);
                _statsUIE.DamageE.ImageC.SetActiveParent(false);
                _statsUIE.EnergyE.ImageUIC.SetActiveParent(false);
                _statsUIE.WaterE.ImageC.SetActiveParent(false);
            }
        }
    }
}
using Chessy.Model.Model.Entity;
using Chessy.Model.Values.Cell.Unit.Stats;
using System;

namespace Chessy.Model
{
    sealed class StatsUIS : SystemUIAbstract
    {
        readonly RightStatsUIEs _statsUIE;

        internal StatsUIS(in RightStatsUIEs rightStatsUIE, in EntitiesModel ents) : base(ents)
        {
            _statsUIE = rightStatsUIE;
        }

        internal override void Sync()
        {
            var idx_sel = _e.CellsC.Selected;


            if (_e.UnitT(idx_sel).HaveUnit() && !_e.UnitT(idx_sel).IsAnimal())
            {
                var damageOnCell = _e.DamageOnCellC(idx_sel).Damage;
                var damageAttack = _e.DamageAttackC(idx_sel).Damage;



                var needActiveWater = false;
                var needActiveHp = false;
                var needActiveDamage = false;


                if (!_e.LessonT.HaveLesson() || _e.LessonT >= Enum.LessonTypes.Install3WarriorsNextToTheRiver)
                {
                    needActiveHp = true;
                }

                if (!_e.LessonT.HaveLesson() || _e.LessonT >= Enum.LessonTypes.ThatsYourDamage)
                {
                    needActiveDamage = true;
                }

                if (!_e.LessonT.HaveLesson() || _e.LessonT >= Enum.LessonTypes.Install3WarriorsNextToTheRiver)
                {
                    if (!_e.UnitT(idx_sel).Is(UnitTypes.Elfemale))
                    {
                        needActiveWater = true;
                    }
                }


                _statsUIE.WaterE.ImageC.SetActiveParent(needActiveWater);
                _statsUIE.Stat(UnitStatTypes.Hp).ImageC.SetActiveParent(needActiveHp);
                _statsUIE.DamageE.ImageC.SetActiveParent(needActiveDamage);
                _statsUIE.EnergyE.ImageUIC.SetActiveParent(true);




                _statsUIE.Stat(UnitStatTypes.Hp).TextC.TextUI.text = Math.Truncate(100 * _e.HpUnitC(idx_sel).Health).ToString();
                _statsUIE.DamageE.TextC.TextUI.text = (Math.Truncate(10 * damageAttack) / 10) + "/" + (Math.Truncate(10 * damageOnCell) / 10).ToString();
                _statsUIE.EnergyE.TextUIC.TextUI.text = (Math.Truncate(100 * _e.StepUnitC(idx_sel).Steps) / 100).ToString();
                _statsUIE.WaterE.TextC.TextUI.text = (Math.Truncate(100 * _e.WaterUnitC(idx_sel).Water) / 100).ToString();

                _statsUIE.Stat(UnitStatTypes.Hp).ImageC.Image.fillAmount = (float)(_e.HpUnitC(idx_sel).Health / HpValues.MAX);



                _statsUIE.DamageE.ImageC.Image.fillAmount = (float)(damageOnCell / (float)damageAttack);

                _statsUIE.EnergyE.ImageUIC.Image.fillAmount = (float)_e.StepUnitC(idx_sel).Steps / StepValues.MAX;
                _statsUIE.WaterE.ImageC.Image.fillAmount = (float)(_e.WaterUnitC(idx_sel).Water / WaterValues.MAX);
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
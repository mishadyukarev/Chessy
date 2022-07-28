using Chessy.Model.Entity;
using Chessy.Model.Values;
using Chessy.View.UI.Entity;
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
            var idx_sel = IndexesCellsC.Selected;


            if (_unitCs[idx_sel].HaveUnit && !_unitCs[idx_sel].UnitType.IsAnimal())
            {
                var damageOnCell = UnitAttackC(idx_sel).DamageOnCellP;
                var damageAttack = UnitAttackC(idx_sel).DamageSimpleAttackP;



                var needActiveWater = false;
                var needActiveHp = false;
                var needActiveDamage = false;


                if (!AboutGameC.LessonType.HaveLesson() || AboutGameC.LessonType >= Enum.LessonTypes.Install1WarriorsNextToTheRiver)
                {
                    needActiveHp = true;
                }

                if (!AboutGameC.LessonType.HaveLesson() || AboutGameC.LessonType >= Enum.LessonTypes.UniqueAttackInfo)
                {
                    needActiveDamage = true;
                }

                if (!AboutGameC.LessonType.HaveLesson() || AboutGameC.LessonType >= Enum.LessonTypes.Install1WarriorsNextToTheRiver)
                {
                    //if (!_unitCs[idx_sel).Is(UnitTypes.Elfemale))
                    //{
                    needActiveWater = true;
                    //}
                }


                _statsUIE.WaterE.ImageC.SetActiveParent(needActiveWater);
                _statsUIE.Stat(UnitStatsTypes.Hp).ImageC.SetActiveParent(needActiveHp);
                _statsUIE.DamageE.ImageC.SetActiveParent(needActiveDamage);
                _statsUIE.EnergyE.ImageUIC.SetActiveParent(false);




                _statsUIE.Stat(UnitStatsTypes.Hp).TextC.TextUI.text = Math.Truncate(100 * _hpUnitCs[idx_sel].HealthP).ToString();
                _statsUIE.DamageE.TextC.TextUI.text = (Math.Truncate(10 * damageAttack) / 10) + "/" + (Math.Truncate(10 * damageOnCell) / 10).ToString();
                //_statsUIE.EnergyE.TextUIC.TextUI.text = (Math.Truncate(100 * _e.EnergyUnitC(idx_sel).Energy) / 100).ToString();
                _statsUIE.WaterE.TextC.TextUI.text = (Math.Truncate(100 * _unitWaterCs[idx_sel].WaterP) / 100).ToString();

                _statsUIE.Stat(UnitStatsTypes.Hp).ImageC.Image.fillAmount = (float)(_hpUnitCs[idx_sel].HealthP / HpUnitValues.MAX);



                _statsUIE.DamageE.ImageC.Image.fillAmount = (float)(damageOnCell / (float)damageAttack);

                //_statsUIE.EnergyE.ImageUIC.Image.fillAmount = (float)_e.EnergyUnitC(idx_sel).Energy / StepValues.MAX;
                _statsUIE.WaterE.ImageC.Image.fillAmount = (float)(_unitWaterCs[idx_sel].WaterP / ValuesChessy.MAX_WATER_FOR_ANY_UNIT);
            }

            else
            {
                _statsUIE.Stat(UnitStatsTypes.Hp).ImageC.SetActiveParent(false);
                _statsUIE.DamageE.ImageC.SetActiveParent(false);
                _statsUIE.EnergyE.ImageUIC.SetActiveParent(false);
                _statsUIE.WaterE.ImageC.SetActiveParent(false);
            }
        }
    }
}
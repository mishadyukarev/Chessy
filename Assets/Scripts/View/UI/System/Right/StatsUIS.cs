using Chessy.Model.Entity;
using Chessy.Model.Values;
using System;
using Chessy.View.UI.Entity; namespace Chessy.Model
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
            var idx_sel = _e.SelectedCellIdx;


            if (_e.UnitT(idx_sel).HaveUnit() && !_e.UnitT(idx_sel).IsAnimal())
            {
                var damageOnCell = _e.DamageOnCell(idx_sel);
                var damageAttack = _e.DamageSimpleAttack(idx_sel);



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
                    //if (!_e.UnitT(idx_sel).Is(UnitTypes.Elfemale))
                    //{
                        needActiveWater = true;
                    //}
                }


                _statsUIE.WaterE.ImageC.SetActiveParent(needActiveWater);
                _statsUIE.Stat(UnitStatsTypes.Hp).ImageC.SetActiveParent(needActiveHp);
                _statsUIE.DamageE.ImageC.SetActiveParent(needActiveDamage);
                _statsUIE.EnergyE.ImageUIC.SetActiveParent(false);




                _statsUIE.Stat(UnitStatsTypes.Hp).TextC.TextUI.text = Math.Truncate(100 * _e.HpUnitC(idx_sel).Health).ToString();
                _statsUIE.DamageE.TextC.TextUI.text = (Math.Truncate(10 * damageAttack) / 10) + "/" + (Math.Truncate(10 * damageOnCell) / 10).ToString();
                //_statsUIE.EnergyE.TextUIC.TextUI.text = (Math.Truncate(100 * _e.EnergyUnitC(idx_sel).Energy) / 100).ToString();
                _statsUIE.WaterE.TextC.TextUI.text = (Math.Truncate(100 * _e.WaterUnitC(idx_sel).Water) / 100).ToString();

                _statsUIE.Stat(UnitStatsTypes.Hp).ImageC.Image.fillAmount = (float)(_e.HpUnitC(idx_sel).Health / HpUnitValues.MAX);



                _statsUIE.DamageE.ImageC.Image.fillAmount = (float)(damageOnCell / (float)damageAttack);

                //_statsUIE.EnergyE.ImageUIC.Image.fillAmount = (float)_e.EnergyUnitC(idx_sel).Energy / StepValues.MAX;
                _statsUIE.WaterE.ImageC.Image.fillAmount = (float)(_e.WaterUnitC(idx_sel).Water / ValuesChessy.MAX_WATER_FOR_ANY_UNIT);
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
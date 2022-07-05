using Chessy.Model;
using Chessy.Model.Entity;
using Chessy.Model.Enum;
using Chessy.Model.Values;
using Chessy.View.UI.Entity;

namespace Chessy.View.UI.System
{
    sealed class UniqueButtonUIS : SystemUIAbstract
    {
        readonly ButtonTypes _buttonT;
        readonly UniqueButtonUIE _buttonE;
        readonly FromResourcesC _resources;

        internal UniqueButtonUIS(in ButtonTypes buttonT, in UniqueButtonUIE uniqueButtonUIE, in FromResourcesC res, in EntitiesModel ents) : base(ents)
        {
            _buttonT = buttonT;
            _buttonE = uniqueButtonUIE;
            _resources = res;
        }

        internal override void Sync()
        {
            var ability_cur = _e.UnitButtonAbilitiesC(_e.SelectedCellIdx).Ability(_buttonT);

            var needActive = false;

            if (_e.UnitPlayerT(_e.SelectedCellIdx) == _e.CurrentPlayerIT && ability_cur != AbilityTypes.None)
            {
                if (_buttonT == ButtonTypes.First)
                {
                    if (!_e.LessonT.HaveLesson() || _e.LessonT >= LessonTypes.SeedingPawn)
                    {
                        needActive = true;
                    }
                }
                else if (_buttonT == ButtonTypes.Second)
                {
                    if (!_e.LessonT.HaveLesson() || _e.LessonT >= LessonTypes.Build3Farms)
                    {
                        needActive = true;
                    }
                }
                else if (_buttonT == ButtonTypes.Third)
                {
                    if (!_e.LessonT.HaveLesson() || _e.LessonT >= LessonTypes.PawnFireAdultForest)
                    {
                        needActive = true;
                    }
                }
                else if (_buttonT == ButtonTypes.Fourth)
                {
                    if (!_e.LessonT.HaveLesson())
                    {
                        needActive = true;
                    }
                }
                else if (_buttonT == ButtonTypes.Fifth)
                {
                    if (!_e.LessonT.HaveLesson())
                    {
                        needActive = true;
                    }
                }
            }


            _buttonE.ParenC.SetActive(needActive);


            if (needActive)
            {
                _buttonE.CooldonwTextC.SetActiveParent(_e.UnitCooldownAbilitiesC(_e.SelectedCellIdx).HaveCooldown(ability_cur));
                _buttonE.CooldonwTextC.TextUI.text = _e.UnitCooldownAbilitiesC(_e.SelectedCellIdx).Cooldown(ability_cur).ToString();



                _buttonE.AbilityImageC.Image.sprite = _resources.Sprite(ability_cur);

                for (var abilityT = AbilityTypes.None + 1; abilityT < AbilityTypes.End; abilityT++)
                {
                    _buttonE.Zone(abilityT).SetActive(false);
                }
                _buttonE.Zone(ability_cur).SetActive(true);


                _buttonE.WaterTextC.ParentG.SetActive(false);
                switch (ability_cur)
                {
                    //case AbilityTypes.IceWall:
                    //    //_buttonE.WaterTextC.ParentG.SetActive(true);
                    //    //_buttonE.WaterTextC.TextUI.text = WaterValues..ToString();
                    //    break;

                    //case AbilityTypes.ActiveAroundBonusSnowy:
                    //    _buttonE.WaterTextC.ParentG.SetActive(true);
                    //    _buttonE.WaterTextC.TextUI.text = WaterValues.BONUS_AROUND_SNOWY.ToString();
                    //    break;

                    //case AbilityTypes.DirectWave:
                    //    _buttonE.WaterTextC.ParentG.SetActive(true);
                    //    _buttonE.WaterTextC.TextUI.text = WaterValues.DIRECT_WAVE.ToString();
                    //    break;

                    //case AbilityTypes.ChangeDirectionWind:
                    //    _buttonE.WaterTextC.ParentG.SetActive(true);
                    //    _buttonE.WaterTextC.TextUI.text = WaterValues.Need(ability_cur).ToString();
                    //    break;
                }

                _buttonE.WoodTextC.ParentG.SetActive(false);
                switch (ability_cur)
                {
                    case AbilityTypes.SetFarm:
                        _buttonE.WoodTextC.ParentG.SetActive(true);
                        _buttonE.WoodTextC.TextUI.text = ((int)(100 * EconomyValues.WOOD_FOR_BUILDING_FARM)).ToString();
                        break;
                }

                if (ability_cur == AbilityTypes.KingPassiveNearBonus)
                {
                    _buttonE.StepsTextC.ParentG.SetActive(false);
                }
                else
                {
                    _buttonE.StepsTextC.ParentG.SetActive(true);
                }

                if (ability_cur != AbilityTypes.KingPassiveNearBonus)
                {
                    _buttonE.StepsTextC.TextUI.text = StepValues.Need(ability_cur).ToString();
                }
            }
        }
    }
}
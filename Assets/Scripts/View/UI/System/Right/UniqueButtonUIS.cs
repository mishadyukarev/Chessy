using Chessy.Model;
using Chessy.Model.Entity;
using Chessy.Model.Enum;
using Chessy.Model.Values;
using Chessy.View.UI.Entity;

namespace Chessy.View.UI.System
{
    sealed class UniqueButtonUIS : SystemUIAbstract
    {
        bool _needActive;
        bool _wasActivated;

        readonly ButtonTypes _buttonT;
        readonly UniqueButtonUIE _buttonE;

        internal UniqueButtonUIS(in ButtonTypes buttonT, in UniqueButtonUIE uniqueButtonUIE, in EntitiesModel ents) : base(ents)
        {
            _buttonT = buttonT;
            _buttonE = uniqueButtonUIE;
        }

        internal override void Sync()
        {
            var ability_cur = _buttonsAbilitiesUnitCs[_cellsC.Selected].Ability(_buttonT);

            _needActive = false;

            if (_unitCs[_cellsC.Selected].PlayerType == _aboutGameC.CurrentPlayerIType && ability_cur != AbilityTypes.None)
            {
                if (_buttonT == ButtonTypes.First)
                {
                    if (!_aboutGameC.LessonType.HaveLesson() || _aboutGameC.LessonType >= LessonTypes.SeedingPawn)
                    {
                        _needActive = true;
                    }
                }
                else if (_buttonT == ButtonTypes.Second)
                {
                    if (!_aboutGameC.LessonType.HaveLesson() || _aboutGameC.LessonType >= LessonTypes.Build1Farms)
                    {
                        _needActive = true;
                    }
                }
                else if (_buttonT == ButtonTypes.Third)
                {
                    if (!_aboutGameC.LessonType.HaveLesson())
                    {
                        _needActive = true;
                    }
                }
                else if (_buttonT == ButtonTypes.Fourth)
                {
                    if (!_aboutGameC.LessonType.HaveLesson())
                    {
                        _needActive = true;
                    }
                }
                else if (_buttonT == ButtonTypes.Fifth)
                {
                    if (!_aboutGameC.LessonType.HaveLesson())
                    {
                        _needActive = true;
                    }
                }
            }


            if (_needActive != _wasActivated) _buttonE.ParenC.GO.SetActive(_needActive);

            _wasActivated = _needActive;

            if (_needActive)
            {
                _buttonE.CooldonwTextC.SetActiveParent(_cooldownAbilityCs[_cellsC.Selected].HaveCooldown(ability_cur));
                _buttonE.CooldonwTextC.TextUI.text = _cooldownAbilityCs[_cellsC.Selected].Cooldown(ability_cur).ToString();



                _buttonE.AbilityImageC.Image.sprite = _fromResourcesC.Sprite(ability_cur);

                for (var abilityT = AbilityTypes.None + 1; abilityT < AbilityTypes.End; abilityT++)
                {
                    _buttonE.Zone(abilityT).TrySetActive(false);
                }
                _buttonE.Zone(ability_cur).TrySetActive(true);


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

                //if (ability_cur == AbilityTypes.KingPassiveNearBonus)
                //{
                //    _buttonE.StepsTextC.ParentG.SetActive(false);
                //}
                //else
                //{
                //    _buttonE.StepsTextC.ParentG.SetActive(true);
                //}

                //if (ability_cur != AbilityTypes.KingPassiveNearBonus)
                //{
                //    _buttonE.StepsTextC.TextUI.text = StepValues.Need(ability_cur).ToString();
                //}
            }
        }
    }
}
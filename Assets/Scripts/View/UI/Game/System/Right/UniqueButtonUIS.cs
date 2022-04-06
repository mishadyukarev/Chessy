using Chessy.Common;
using Chessy.Game.Values;
using Chessy.Game.Values.Cell.Unit.Stats;
using Chessy.Game.View.UI.Entity.Right;

namespace Chessy.Game.View.UI.System
{
    sealed class UniqueButtonUIS : SystemUIAbstract
    {
        readonly ButtonTypes _buttonT;
        readonly UniqueButtonUIE _buttonE;
        readonly ResourcesE _resources;

        internal UniqueButtonUIS(in ButtonTypes buttonT, in UniqueButtonUIE uniqueButtonUIE, in ResourcesE res, in Chessy.Game.Model.Entity.EntitiesModelGame ents) : base(ents)
        {
            _buttonT = buttonT;
            _buttonE = uniqueButtonUIE;
            _resources = res;
        }

        internal override void Sync()
        {
            var ability_cur = e.UnitButtonAbilitiesC(e.SelectedCell).Ability(_buttonT);

            var needActive = false;

            if (e.UnitPlayerT(e.SelectedCell) == e.CurPlayerIT && ability_cur != AbilityTypes.None)
            {
                if (_buttonT == ButtonTypes.First)
                {
                    if (!e.LessonTC.HaveLesson || e.LessonTC.LessonT >= Enum.LessonTypes.SeedingPawn)
                    {
                        needActive = true;
                    }
                }
                else if (_buttonT == ButtonTypes.Second)
                {
                    if (!e.LessonTC.HaveLesson || e.LessonTC.LessonT >= Enum.LessonTypes.BuildingFarmHere)
                    {
                        needActive = true;
                    }
                }
                else if (_buttonT == ButtonTypes.Third)
                {
                    if (!e.LessonTC.HaveLesson)
                    {
                        needActive = true;
                    }
                }
            }


            _buttonE.ParenC.SetActive(needActive);


            if (needActive)
            {
                _buttonE.CooldonwTextC.SetActiveParent(e.UnitCooldownAbilitiesC(e.CellsC.Selected).HaveCooldown(ability_cur));
                _buttonE.CooldonwTextC.TextUI.text = e.UnitCooldownAbilitiesC(e.CellsC.Selected).Cooldown(ability_cur).ToString();



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

                _buttonE.StepsTextC.TextUI.text = StepValues.Need(ability_cur).ToString();
            }
        }
    }
}
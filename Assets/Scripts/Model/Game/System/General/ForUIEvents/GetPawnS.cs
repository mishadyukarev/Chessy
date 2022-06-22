﻿using Chessy.Common.Enum;
using Chessy.Game.Enum;

namespace Chessy.Game.Model.System
{
    public sealed partial class SystemsModelGameForUI
    {
        public void GetPawn()
        {
            _e.Com.SoundActionC(ClipCommonTypes.Click).Invoke();

            if (!_e.LessonT.Is(LessonTypes.TryBuyingHouse, LessonTypes.ThatsYourEffects, LessonTypes.ThatsYourDamage, LessonTypes.ClickDefend))
            {
                if (_e.CurPlayerIT.Is(_e.WhoseMovePlayerT))
                {
                    if (_e.PlayerInfoE(_e.CurPlayerIT).PawnInfoC.HaveAnyPeopleInCity)
                    {
                        if (_e.PlayerInfoE(_e.CurPlayerIT).PawnInfoC.AmountInGame < _e.PlayerInfoE(_e.CurPlayerIT).PawnInfoC.MaxAvailable)
                        {
                            _e.SelectedCellIdx = 0;

                            _e.SelectedUnitE.UnitT = UnitTypes.Pawn;
                            _e.SelectedUnitE.LevelT = LevelTypes.First;

                            _e.CellClickT = CellClickTypes.SetUnit;
                        }
                        else
                        {
                            if (_e.LessonT.Is(LessonTypes.SettingPawn))
                            {
                                _e.LessonT.SetNextLesson();
                            }
                            else if (_e.LessonT.Is(LessonTypes.OpeningTown, LessonTypes.TryBuyingHouse))
                            {

                            }

                            else
                            {

                                _s.SetMistake(MistakeTypes.NeedBuildingHouses, 0);
                                _e.SoundAction(ClipTypes.WritePensil).Invoke();
                                _e.IsSelectedCity = true;
                            }

                        }
                    }
                    else
                    {
                        _e.SoundAction(ClipTypes.WritePensil).Invoke();

                        _s.SetMistake(MistakeTypes.NeedMorePeopleInCity, 0);
                        //..E.Sound(ClipTypes.Mistake).Action.Invoke();
                    }


                }
                else
                {
                    _e.MistakeT = MistakeTypes.NeedWaitQueue;
                    _e.MistakeTimer = 0;
                    _e.SoundAction(ClipTypes.WritePensil).Invoke();
                }
            }

            _e.NeedUpdateView = true;
        }
    }
}
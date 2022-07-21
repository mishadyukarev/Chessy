using Chessy.Model.Enum;
namespace Chessy.Model.System
{
    public sealed partial class ForButtonsSystemsModel
    {
        public void GetPawn()
        {
            _dataFromViewC.SoundAction(ClipTypes.Click).Invoke();

            if (!_aboutGameC.LessonT.Is(LessonTypes.TryBuyingHouse, LessonTypes.HoldPressWarrior))
            {
                if (PawnPeopleInfoC(_aboutGameC.CurrentPlayerIT).HaveAnyPeopleInCity)
                {
                    if (PawnPeopleInfoC(_aboutGameC.CurrentPlayerIT).AmountInGame < PawnPeopleInfoC(_aboutGameC.CurrentPlayerIT).MaxAvailablePawns(PlayerInfoC(_aboutGameC.CurrentPlayerIT).AmountBuiltHouses))
                    {
                        _cellsC.Selected = 0;

                        _selectedUnitC.UnitT = UnitTypes.Pawn;
                        _selectedUnitC.LevelT = LevelTypes.First;

                        _aboutGameC.CellClickT = CellClickTypes.SetUnit;
                    }
                    else
                    {
                        if (_aboutGameC.LessonT.Is(LessonTypes.SettingPawn))
                        {
                            _s.SetNextLesson();
                        }
                        else if (_aboutGameC.LessonT.Is(LessonTypes.OpeningTown, LessonTypes.TryBuyingHouse))
                        {

                        }

                        else
                        {

                            _s.SetMistake(MistakeTypes.NeedBuildingHouses, 0);
                            _dataFromViewC.SoundAction(ClipTypes.WritePensil).Invoke();
                            _aboutGameC.IsSelectedCity = true;
                        }

                    }
                }
                else
                {
                    _dataFromViewC.SoundAction(ClipTypes.WritePensil).Invoke();

                    _s.SetMistake(MistakeTypes.NeedMorePeopleInCity, 0);
                    //..E.Sound(ClipTypes.Mistake).Action.Invoke();
                }
            }

            _updateAllViewC.NeedUpdateView = true;
        }
    }
}
using Chessy.Model.Enum;
namespace Chessy.Model.System
{
    public sealed partial class ForButtonsSystemsModel
    {
        public void GetPawn()
        {
            _e.SoundAction(ClipTypes.Click).Invoke();

            if (!_e.LessonT.Is(LessonTypes.TryBuyingHouse, LessonTypes.HoldPressWarrior))
            {
                if (_e.PawnPeopleInfoC(_aboutGameC.CurrentPlayerIT).HaveAnyPeopleInCity)
                {
                    if (_e.PawnPeopleInfoC(_aboutGameC.CurrentPlayerIT).AmountInGame < _e.PawnPeopleInfoC(_aboutGameC.CurrentPlayerIT).MaxAvailablePawns(_e.PlayerInfoC(_aboutGameC.CurrentPlayerIT).AmountBuiltHouses))
                    {
                        _e.SelectedCellIdx = 0;

                        _e.SelectedUnitC.UnitT = UnitTypes.Pawn;
                        _e.SelectedUnitC.LevelT = LevelTypes.First;

                        _e.CellClickT = CellClickTypes.SetUnit;
                    }
                    else
                    {
                        if (_e.LessonT.Is(LessonTypes.SettingPawn))
                        {
                            _s.SetNextLesson();
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

            _e.NeedUpdateView = true;
        }
    }
}
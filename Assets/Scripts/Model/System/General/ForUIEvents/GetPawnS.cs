using Chessy.Model.Enum;
namespace Chessy.Model.System
{
    public sealed partial class ForButtonsSystemsModel
    {
        public void GetPawn()
        {
            _dataFromViewC.SoundAction(ClipTypes.Click).Invoke();

            if (!AboutGameC.LessonT.Is(LessonTypes.TryBuyingHouse, LessonTypes.HoldPressWarrior))
            {
                if (PawnPeopleInfoC(AboutGameC.CurrentPlayerIT).HaveAnyPeopleInCity)
                {
                    if (PawnPeopleInfoC(AboutGameC.CurrentPlayerIT).AmountInGame < PawnPeopleInfoC(AboutGameC.CurrentPlayerIT).MaxAvailablePawns(PlayerInfoC(AboutGameC.CurrentPlayerIT).AmountBuiltHouses))
                    {
                        IndexesCellsC.Selected = 0;

                        _selectedUnitC.UnitT = UnitTypes.Pawn;
                        _selectedUnitC.LevelT = LevelTypes.First;

                        AboutGameC.CellClickT = CellClickTypes.SetUnit;
                    }
                    else
                    {
                        if (AboutGameC.LessonT.Is(LessonTypes.SettingPawn))
                        {
                            _s.SetNextLesson();
                        }
                        else if (AboutGameC.LessonT.Is(LessonTypes.OpeningTown, LessonTypes.TryBuyingHouse))
                        {

                        }

                        else
                        {

                            _s.SetMistake(MistakeTypes.NeedBuildingHouses, 0);
                            _dataFromViewC.SoundAction(ClipTypes.WritePensil).Invoke();
                            AboutGameC.IsSelectedCity = true;
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
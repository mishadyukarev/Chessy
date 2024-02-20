using Chessy.Model.Enum;
namespace Chessy.Model.System
{
    public sealed partial class ForButtonsSystemsModel
    {
        public void GetPawn()
        {
            dataFromViewC.SoundAction(ClipTypes.Click).Invoke();

            if (!aboutGameC.LessonT.Is(LessonTypes.TryBuyingHouse, LessonTypes.HoldPressWarrior))
            {
                if (PawnPeopleInfoC(aboutGameC.CurrentPlayerIT).HaveAnyPeopleInCity)
                {
                    if (PawnPeopleInfoC(aboutGameC.CurrentPlayerIT).AmountInGame < PawnPeopleInfoC(aboutGameC.CurrentPlayerIT).MaxAvailablePawns(playerInfoCs[(byte)aboutGameC.CurrentPlayerIT].AmountBuiltHouses))
                    {
                        indexesCellsC.Selected = 0;

                        selectedUnitC.UnitT = UnitTypes.Pawn;
                        selectedUnitC.LevelT = LevelTypes.First;

                        aboutGameC.CellClickT = CellClickTypes.SetUnit;
                    }
                    else
                    {
                        if (aboutGameC.LessonT.Is(LessonTypes.SettingPawn))
                        {
                            s.SetNextLesson();
                        }
                        else if (aboutGameC.LessonT.Is(LessonTypes.OpeningTown, LessonTypes.TryBuyingHouse))
                        {

                        }

                        else
                        {

                            s.SetMistake(MistakeTypes.NeedBuildingHouses, 0);
                            dataFromViewC.SoundAction(ClipTypes.WritePensil).Invoke();
                            aboutGameC.IsSelectedCity = true;
                        }

                    }
                }
                else
                {
                    dataFromViewC.SoundAction(ClipTypes.WritePensil).Invoke();

                    s.SetMistake(MistakeTypes.NeedMorePeopleInCity, 0);
                    //..E.Sound(ClipTypes.Mistake).Action.Invoke();
                }
            }

            updateAllViewC.NeedUpdateView = true;
        }
    }
}
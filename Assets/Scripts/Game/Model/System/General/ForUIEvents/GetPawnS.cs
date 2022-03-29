using Chessy.Common.Interface;
using Chessy.Game.Entity.Model;
using Chessy.Game.Enum;

namespace Chessy.Game.System.Model
{
    public sealed class GetPawnS : SystemModelGameAbs, IClickUI
    {
        public GetPawnS(in EntitiesModelGame eMGame) : base(eMGame)
        {
        }

        public void Click()
        {
            e.CellsC.Selected = 0;

            e.Sound(ClipTypes.Click).Invoke();

            var curPlayerI = e.CurPlayerITC.Player;


            if (e.CurPlayerITC.Is(e.WhoseMove.Player))
            {
                if (e.PlayerInfoE(curPlayerI).PeopleInCity >= 1)
                {
                    var pawnsInGame = e.UnitInfoE(curPlayerI, LevelTypes.First).UnitsInGame(UnitTypes.Pawn)
                        + e.UnitInfoE(curPlayerI, LevelTypes.Second).UnitsInGame(UnitTypes.Pawn);

                    if (pawnsInGame < e.PlayerInfoE(curPlayerI).MaxAvailablePawns)
                    {
                        e.SelectedUnitE.UnitTC.Unit = UnitTypes.Pawn;
                        e.SelectedUnitE.LevelTC.Level = LevelTypes.First;

                        e.CellClickTC.Click = CellClickTypes.SetUnit;
                    }
                    else
                    {
                        if (e.LessonTC.LessonT == LessonTypes.SettingPawn)
                        {
                            e.LessonTC.SetNextLesson();
                        }
                        else if (e.LessonTC.LessonT == LessonTypes.OpenTown || e.LessonTC.LessonT == LessonTypes.BuyingHouse)
                        {

                        }

                        else
                        {

                            e.MistakeC.Set(MistakeTypes.NeedBuildingHouses, 0);
                            e.Sound(ClipTypes.WritePensil).Action.Invoke();
                            e.IsSelectedCity = true;
                        }

                    }
                }
                else
                {
                    e.Sound(ClipTypes.WritePensil).Action.Invoke();

                    e.MistakeC.Set(MistakeTypes.NeedMorePeopleInCity, 0);
                    //..E.Sound(ClipTypes.Mistake).Action.Invoke();
                }


            }
            else
            {
                e.MistakeC.MistakeT = MistakeTypes.NeedWaitQueue;
                e.MistakeC.Timer = 0;
                e.Sound(ClipTypes.WritePensil).Action.Invoke();
            }


            e.NeedUpdateView = true;
        }
    }
}
namespace Chessy.Game.System.Model
{
    public struct GetPawnS
    {
        public void Get(in EntitiesModel e)
        {
            e.CellsC.Selected = 0;

            e.Sound(ClipTypes.Click).Invoke();

            if (e.CurPlayerITC.Is(e.WhoseMove.Player))
            {
                var curPlayerI = e.CurPlayerITC.Player;

                if (e.PlayerInfoE(curPlayerI).PeopleInCity >= 1)
                {
                    var pawnsInGame = e.UnitInfoE(curPlayerI, LevelTypes.First).UnitsInGame(UnitTypes.Pawn)
                        + e.UnitInfoE(curPlayerI, LevelTypes.Second).UnitsInGame(UnitTypes.Pawn);

                    if (pawnsInGame < e.PlayerInfoE(curPlayerI).MaxAvailablePawns)
                    {
                        e.SelectedE.UnitC.Set(UnitTypes.Pawn, LevelTypes.First);
                        e.CellClickTC.Click = CellClickTypes.SetUnit;
                    }
                    else
                    {
                        e.MistakeC.Set(MistakeTypes.NeedBuildingHouses, 0);
                        e.Sound(ClipTypes.WritePensil).Action.Invoke();
                        e.IsSelectedCity = true;
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
        }
    }
}
using Chessy.Common.Interface;
using Chessy.Game.Entity.Model;

namespace Chessy.Game.System.Model
{
    public sealed class GetPawnS : SystemModelGameAbs, IClickUI
    {
        public GetPawnS(in EntitiesModelGame eMGame) : base(eMGame)
        {
        }

        public void Click()
        {
            eMGame.CellsC.Selected = 0;

            eMGame.Sound(ClipTypes.Click).Invoke();

            if (eMGame.CurPlayerITC.Is(eMGame.WhoseMove.Player))
            {
                var curPlayerI = eMGame.CurPlayerITC.Player;

                if (eMGame.PlayerInfoE(curPlayerI).PeopleInCity >= 1)
                {
                    var pawnsInGame = eMGame.UnitInfoE(curPlayerI, LevelTypes.First).UnitsInGame(UnitTypes.Pawn)
                        + eMGame.UnitInfoE(curPlayerI, LevelTypes.Second).UnitsInGame(UnitTypes.Pawn);

                    if (pawnsInGame < eMGame.PlayerInfoE(curPlayerI).MaxAvailablePawns)
                    {
                        eMGame.SelectedE.UnitC.Set(UnitTypes.Pawn, LevelTypes.First);
                        eMGame.CellClickTC.Click = CellClickTypes.SetUnit;
                    }
                    else
                    {
                        eMGame.MistakeC.Set(MistakeTypes.NeedBuildingHouses, 0);
                        eMGame.Sound(ClipTypes.WritePensil).Action.Invoke();
                        eMGame.IsSelectedCity = true;
                    }
                }
                else
                {
                    eMGame.Sound(ClipTypes.WritePensil).Action.Invoke();

                    eMGame.MistakeC.Set(MistakeTypes.NeedMorePeopleInCity, 0);
                    //..E.Sound(ClipTypes.Mistake).Action.Invoke();
                }


            }
            else
            {
                eMGame.MistakeC.MistakeT = MistakeTypes.NeedWaitQueue;
                eMGame.MistakeC.Timer = 0;
                eMGame.Sound(ClipTypes.WritePensil).Action.Invoke();
            }
        }
    }
}
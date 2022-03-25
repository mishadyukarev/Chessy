using Chessy.Common.Interface;
using Chessy.Game.Entity.Model;

namespace Chessy.Game.System.Model
{
    public sealed class GetKingClickS : SystemModelGameAbs, IClickUI
    {
        public GetKingClickS(in EntitiesModelGame eMGame) : base(eMGame)
        {
        }

        public void Click()
        {
            eMGame.CellsC.Selected = 0;

            if (eMGame.CurPlayerITC.Is(eMGame.WhoseMove.Player))
            {
                eMGame.Sound(ClipTypes.Click).Invoke();

                if (eMGame.PlayerInfoE(eMGame.CurPlayerITC.Player).HaveKingInInventor)
                {
                    eMGame.SelectedE.UnitC.Set(UnitTypes.King, LevelTypes.First);
                    eMGame.CellClickTC.Click = CellClickTypes.SetUnit;
                }
            }
            else eMGame.Sound(ClipTypes.Mistake).Action.Invoke();
        }
    }
}
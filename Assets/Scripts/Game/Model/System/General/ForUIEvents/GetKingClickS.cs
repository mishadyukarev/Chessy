using Chessy.Common.Interface;
using Chessy.Game.Entity.Model;

namespace Chessy.Game.System.Model
{
    public sealed class GetKingClickS : SystemModelGameAbs, IClickUI
    {
        internal GetKingClickS(in SystemsModelGame sMGame, in EntitiesModelGame eMGame) : base(sMGame, eMGame) { }

        public void Click()
        {
            e.CellsC.Selected = 0;

            if (e.CurPlayerITC.Is(e.WhoseMove.Player))
            {
                e.Sound(ClipTypes.Click).Invoke();

                if (e.PlayerInfoE(e.CurPlayerITC.Player).HaveKingInInventor)
                {
                    e.SelectedUnitE.UnitTC.Unit = UnitTypes.King;
                    e.SelectedUnitE.LevelTC.Level = LevelTypes.First;

                    e.CellClickTC.Click = CellClickTypes.SetUnit;
                }
            }
            else e.Sound(ClipTypes.Mistake).Action.Invoke();

            e.NeedUpdateView = true;
        }
    }
}
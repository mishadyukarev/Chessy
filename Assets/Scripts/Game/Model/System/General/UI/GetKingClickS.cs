namespace Chessy.Game.System.Model
{
    public struct GetKingClickS
    {
        public void Click(in Chessy.Game.Entity.Model.EntitiesModelGame e)
        {
            e.CellsC.Selected = 0;

            if (e.CurPlayerITC.Is(e.WhoseMove.Player))
            {
                e.Sound(ClipTypes.Click).Invoke();

                if (e.PlayerInfoE(e.CurPlayerITC.Player).HaveKingInInventor)
                {
                    e.SelectedE.UnitC.Set(UnitTypes.King, LevelTypes.First);
                    e.CellClickTC.Click = CellClickTypes.SetUnit;
                }
            }
            else e.Sound(ClipTypes.Mistake).Action.Invoke();
        }
    }
}
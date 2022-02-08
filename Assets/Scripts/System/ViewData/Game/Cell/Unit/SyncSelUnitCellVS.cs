using System;

namespace Game.Game
{
    sealed class SyncSelUnitCellVS : SystemViewAbstract, IEcsRunSystem
    {
        internal SyncSelUnitCellVS(in Entities ents, in EntitiesView entsView) : base(ents, entsView) { }

        public void Run()
        {
            if (Es.ClickerObjectE.CellClickC.Is(CellClickTypes.SetUnit))
            {
                var idx_cur = Es.CurrentIdxE.IdxC.Idx;

                //if (UnitEs(idx_cur).TypeE.HaveUnit)
                //{
                //    if (UnitEs(idx_cur).VisibleE(Es.WhoseMoveE.CurPlayerI).IsVisibleC.IsVisible)
                //    {
                //        mainUnit_pre.Enable();
                //    }

                //    else
                //    {
                //        mainUnit_cur.Enable();
                //    }
                //}

                //else
                //{
                //    mainUnit_cur.Enable();

                //}


                var selUnitT = Es.SelectedUnitE.UnitTC.Unit;
                var selLevelUnitT = Es.SelectedUnitE.LevelTC.Level;

                if(selUnitT == UnitTypes.Archer)
                {
                    VEs.UnitE(idx_cur, selUnitT).Enable();
                }
                else
                {
                    VEs.UnitE(idx_cur, selUnitT).Enable();
                }
            }
        }
    }
}

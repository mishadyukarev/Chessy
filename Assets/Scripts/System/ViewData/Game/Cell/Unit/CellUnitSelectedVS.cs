using System;

namespace Game.Game
{
    sealed class CellUnitSelectedVS : SystemViewAbstract, IEcsRunSystem
    {
        internal CellUnitSelectedVS(in Entities ents, in EntitiesView entsView) : base(ents, entsView) { }

        public void Run()
        {
            if (Es.ClickerObjectE.CellClickCRef.Is(CellClickTypes.SetUnit))
            {
                var idx_cur = Es.CurrentIdxE.IdxC.Idx;
                var selUnitT = Es.SelectedUnitE.UnitTC.Unit;
                var levT = Es.SelectedUnitE.Level;

                if (selUnitT == UnitTypes.Pawn)
                {
                    VEs.UnitEs(idx_cur).MainToolWeaponE(true, LevelTypes.First, ToolWeaponTypes.Axe).Enable(true);
                }
                else
                {
                    VEs.UnitE(idx_cur, true, levT,  selUnitT).Enable(true);
                }
            }
        }
    }
}

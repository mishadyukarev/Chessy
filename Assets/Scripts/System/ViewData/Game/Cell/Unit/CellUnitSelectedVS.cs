using System;

namespace Game.Game
{
    sealed class CellUnitSelectedVS : SystemViewAbstract, IEcsRunSystem
    {
        internal CellUnitSelectedVS(in Entities ents, in EntitiesView entsView) : base(ents, entsView) { }

        public void Run()
        {
            if (Es.CellClickTC.Is(CellClickTypes.SetUnit))
            {
                var idx_cur = Es.CurrentIdxC.Idx;
                var selUnitT = Es.SelUnitTC.Unit;
                var levT = Es.SelUnitLevelTC.Level;

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

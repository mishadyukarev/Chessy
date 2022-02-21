using System;

namespace Game.Game
{
    sealed class CellUnitSelectedVS : SystemViewAbstract, IEcsRunSystem
    {
        internal CellUnitSelectedVS(in EntitiesModel ents, in EntitiesView entsView) : base(ents, entsView) { }

        public void Run()
        {
            if (E.CellClickTC.Is(CellClickTypes.SetUnit))
            {
                var idx_cur = E.CurrentIdxC.Idx;
                var selUnitT = E.SelectedUnitE.UnitTC.Unit;
                var levT = E.SelectedUnitE.LevelTC.Level;

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

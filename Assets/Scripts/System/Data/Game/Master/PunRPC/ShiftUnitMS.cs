namespace Game.Game
{
    sealed class ShiftUnitMS : SystemCellAbstract, IEcsRunSystem
    {
        public ShiftUnitMS(in Entities ents) : base(ents) { }

        public void Run()
        {
            Es.MasterEs.Shift<IdxFromToC>().Get(out var idx_from, out var idx_to);

            var whoseMove = Es.WhoseMove.WhoseMove.Player;


            if (CellsForShiftUnitsEs.CellsForShift<IdxsC>(whoseMove, idx_from).Contains(idx_to))
            {
                UnitStatEs(idx_from).StepE.TakeForShift(idx_to, Es);

                UnitEs(idx_from).MainE.Shift(idx_to, Es);

                Es.Rpc.SoundToGeneral(InfoC.Sender(MGOTypes.Master), ClipTypes.ClickToTable);
            }
        }
    }
}
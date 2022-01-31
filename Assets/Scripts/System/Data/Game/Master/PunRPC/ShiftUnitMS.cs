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
                UnitEs.StatEs.Step(idx_from).Steps.Amount -= UnitEs.Main(idx_to).StepsForShiftOrAttack(CellEs.GetDirect(idx_from, idx_to), EnvironmentEs, CellEs.TrailEs);

                UnitEs.Shift(idx_from, idx_to, Es);

                Es.Rpc.SoundToGeneral(InfoC.Sender(MGOTypes.Master), ClipTypes.ClickToTable);
            }
        }
    }
}
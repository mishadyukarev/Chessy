namespace Game.Game
{
    struct ShiftUnitMS : IEcsRunSystem
    {
        public void Run()
        {
            Entities.MasterEs.Shift<IdxFromToC>().Get(out var idx_from, out var idx_to);

            var whoseMove = Entities.WhoseMove.WhoseMove.Player;


            if (CellsForShiftUnitsEs.CellsForShift<IdxsC>(whoseMove, idx_from).Contains(idx_to))
            {
                Entities.CellEs.UnitEs.Step(idx_from).Steps.Take(Entities.CellEs.UnitEs.Step(idx_to).StepsForShiftOrAttack(CellSpaceSupport.GetDirect(idx_from, idx_to), Entities.CellEs.EnvironmentEs.Environments(idx_to), Entities.CellEs.TrailEs.Trails(idx_to)));

                Entities.CellEs.UnitEs.Shift(idx_from, idx_to, true);

                Entities.Rpc.SoundToGeneral(InfoC.Sender(MGOTypes.Master), ClipTypes.ClickToTable);
            }
        }
    }
}
namespace Game.Game
{
    struct ShiftUnitMS : IEcsRunSystem
    {
        public void Run()
        {
            EntitiesMaster.Shift<IdxFromToC>().Get(out var idx_from, out var idx_to);

            var whoseMove = Entities.WhoseMove.WhoseMove.Player;


            if (CellsForShiftUnitsEs.CellsForShift<IdxsC>(whoseMove, idx_from).Contains(idx_to))
            {
                CellUnitEs.Step(idx_from).AmountC.Take(CellUnitEs.StepsForDoing(idx_from, idx_to));

                CellUnitEs.Shift(idx_from, idx_to, true);

                Entities.Rpc.SoundToGeneral(InfoC.Sender(MGOTypes.Master), ClipTypes.ClickToTable);
            }
        }
    }
}
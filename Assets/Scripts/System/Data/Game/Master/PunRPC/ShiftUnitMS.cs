using static Game.Game.CellUnitEs;

namespace Game.Game
{
    struct ShiftUnitMS : IEcsRunSystem
    {
        public void Run()
        {
            EntityMPool.Shift<IdxFromToC>().Get(out var idx_from, out var idx_to);

            var whoseMove = WhoseMoveE.WhoseMove.Player;


            if (CellsForShiftUnitsEs.CellsForShift<IdxsC>(whoseMove, idx_from).Contains(idx_to))
            {
                EntitiesPool.UnitStep.TakeStepsForDoing(idx_from, idx_to);

                CellUnitEs.Shift(idx_from, idx_to, true);

                EntityPool.Rpc.SoundToGeneral(InfoC.Sender(MGOTypes.Master), ClipTypes.ClickToTable);
            }
        }
    }
}
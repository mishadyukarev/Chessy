using static Game.Game.CellUnitEs;

namespace Game.Game
{
    struct ShiftUnitMS : IEcsRunSystem
    {
        public void Run()
        {
            EntityMPool.Shift<IdxFromToC>().Get(out var idx_from, out var idx_to);

            var whoseMove = WhoseMoveE.WhoseMove<PlayerTC>().Player;


            if (CellsForShiftUnitsEs.CellsForShift<IdxsC>(whoseMove, idx_from).Contains(idx_to))
            {
                CellUnitStepEs.TakeStepsForDoing(idx_from, idx_to);

                CellUnitEs.Shift(idx_from, idx_to);

                EntityPool.Rpc<RpcC>().SoundToGeneral(InfoC.Sender(MGOTypes.Master), ClipTypes.ClickToTable);
            }
        }
    }
}
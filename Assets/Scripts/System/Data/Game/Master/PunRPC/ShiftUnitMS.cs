using Leopotam.Ecs;
using static Game.Game.EntityCellPool;

namespace Game.Game
{
    public sealed class ShiftUnitMS : IEcsRunSystem
    {
        public void Run()
        {
            FromToDoingMC.Get(out var idx_from, out var idx_to);

            var whoseMove = WhoseMoveC.WhoseMove;


            if (Unit<UnitCellEC>(idx_from).CanShift(whoseMove, idx_to))
            {
                Unit<UnitCellEC>(idx_from).TakeStepsForDoing(idx_to);

                Unit<UnitCellEC>(idx_from).Shift(idx_to); 

                RpcSys.SoundToGeneral(InfoC.Sender(MGOTypes.Master), ClipTypes.ClickToTable);
            }
        }
    }
}
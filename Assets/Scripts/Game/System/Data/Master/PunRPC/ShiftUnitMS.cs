using Leopotam.Ecs;
using static Game.Game.EntityPool;

namespace Game.Game
{
    public sealed class ShiftUnitMS : IEcsRunSystem
    {
        public void Run()
        {
            FromToDoingMC.Get(out var idx_from, out var idx_to);

            var whoseMove = WhoseMoveC.WhoseMove;


            if (Unit<UnitCellWC>(idx_from).CanShift(idx_to))
            {
                Unit<StepUnitWC>(idx_from).TakeStepsForDoing(idx_to);

                Unit<UnitCellWC>(idx_from).Shift(idx_to); 

                RpcSys.SoundToGeneral(InfoC.Sender(MGOTypes.Master), ClipTypes.ClickToTable);
            }
        }
    }
}
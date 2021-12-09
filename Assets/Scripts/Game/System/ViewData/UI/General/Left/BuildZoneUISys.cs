using Leopotam.Ecs;
using static Game.Game.EntityPool;

namespace Game.Game
{
    public sealed class BuildZoneUISys : IEcsRunSystem
    {
        public void Run()
        {
            ref var unit_sel = ref Build<BuildC>(SelIdx<IdxC>().Idx);
            ref var own_sel = ref Build<OwnerC>(SelIdx<IdxC>().Idx);


            if (SelIdx<SelIdxC>().IsSelCell && unit_sel.Is(BuildTypes.City))
            {
                if (own_sel.Is(WhoseMoveC.CurPlayerI))
                {
                    CutyLeftUIC.SetActiveZone(true);
                }
                else CutyLeftUIC.SetActiveZone(false);
            }
            else
            {
                CutyLeftUIC.SetActiveZone(false);
            }
        }
    }
}
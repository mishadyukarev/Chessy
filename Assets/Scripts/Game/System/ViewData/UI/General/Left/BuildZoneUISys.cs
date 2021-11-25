using Leopotam.Ecs;

namespace Game.Game
{
    public sealed class BuildZoneUISys : IEcsRunSystem
    {
        public void Run()
        {
            ref var unit_sel = ref EntityDataPool.GetBuildCellC<BuildC>(SelIdx.Idx);
            ref var own_sel = ref EntityDataPool.GetBuildCellC<OwnerC>(SelIdx.Idx);


            if (SelIdx.IsSelCell && unit_sel.Is(BuildTypes.City))
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
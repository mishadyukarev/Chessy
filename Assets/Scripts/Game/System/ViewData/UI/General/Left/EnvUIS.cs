using Leopotam.Ecs;

namespace Game.Game
{
    public sealed class EnvUIS : IEcsRunSystem
    {
        public void Run()
        {
            ref var build_sel = ref EntityDataPool.GetBuildCellC<BuildC>(SelIdx.Idx);

            ref var env_sel = ref EntityDataPool.GetEnvCellC<EnvC>(SelIdx.Idx);
            ref var envRes_sel = ref EntityDataPool.GetEnvCellC<EnvResC>(SelIdx.Idx);


            if (SelIdx.IsSelCell && !build_sel.Is(BuildTypes.City))
            {
                EnvirUIC.SetActiveParent(true);
            }
            else
            {
                EnvirUIC.SetActiveParent(false);
            }


            EnvirUIC.SetTextResour(ResTypes.Food, envRes_sel.AmountRes(EnvTypes.Fertilizer).ToString());
            EnvirUIC.SetTextResour(ResTypes.Wood, envRes_sel.AmountRes(EnvTypes.AdultForest).ToString());
            EnvirUIC.SetTextResour(ResTypes.Ore, envRes_sel.AmountRes(EnvTypes.Hill).ToString());
        }
    }
}
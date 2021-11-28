using Leopotam.Ecs;

namespace Game.Game
{
    public sealed class EnvUIS : IEcsRunSystem
    {
        public void Run()
        {
            ref var build_sel = ref EntityPool.Build<BuildC>(SelIdx.Idx);

            ref var env_sel = ref EntityPool.Environment<EnvC>(SelIdx.Idx);
            ref var envRes_sel = ref EntityPool.Environment<EnvResC>(SelIdx.Idx);


            if (SelIdx.IsSelCell && !build_sel.Is(BuildTypes.City))
            {
                EnvirUIC.SetActiveParent(true);
            }
            else
            {
                EnvirUIC.SetActiveParent(false);
            }


            EnvirUIC.SetTextResour(ResTypes.Food, envRes_sel.Amount(EnvTypes.Fertilizer).ToString());
            EnvirUIC.SetTextResour(ResTypes.Wood, envRes_sel.Amount(EnvTypes.AdultForest).ToString());
            EnvirUIC.SetTextResour(ResTypes.Ore, envRes_sel.Amount(EnvTypes.Hill).ToString());
        }
    }
}
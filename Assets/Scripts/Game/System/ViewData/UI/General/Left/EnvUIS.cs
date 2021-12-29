using Leopotam.Ecs;
using static Game.Game.EntityCellPool;
using static Game.Game.EntityPool;

namespace Game.Game
{
    public sealed class EnvUIS : IEcsRunSystem
    {
        public void Run()
        {
            ref var build_sel = ref Build<BuildC>(SelIdx<IdxC>().Idx);

            ref var env_sel = ref Environment<EnvC>(SelIdx<IdxC>().Idx);
            ref var envRes_sel = ref Environment<EnvResC>(SelIdx<IdxC>().Idx);


            if (SelIdx<SelIdxC>().IsSelCell && !build_sel.Is(BuildTypes.City))
            {
                EnvirUIC.SetActiveParent(true);

                EnvirUIC.SetTextResour(ResTypes.Food, envRes_sel.Amount(EnvTypes.Fertilizer).ToString());
                EnvirUIC.SetTextResour(ResTypes.Wood, envRes_sel.Amount(EnvTypes.AdultForest).ToString());
                EnvirUIC.SetTextResour(ResTypes.Ore, envRes_sel.Amount(EnvTypes.Hill).ToString());
            }
            else
            {
                EnvirUIC.SetActiveParent(false);
            }
        }
    }
}
using static Game.Game.CellBuildE;
using static Game.Game.CellEnvironmentE;
using static Game.Game.EntityPool;
using static Game.Game.EntityLeftEnvUIPool;

namespace Game.Game
{
    struct EnvUIS : IEcsRunSystem
    {
        public void Run()
        {
            ref var build_sel = ref Build<BuildingC>(SelIdx<IdxC>().Idx);


            if (SelIdx<SelIdxEC>().IsSelCell && !build_sel.Is(BuildTypes.City))
            {
                Info<ButtonUIC>().SetActiveParent(true);

                Resources<TextMPUGUIC>(ResTypes.Food).Text = Environment<AmountResourcesC>(EnvTypes.Fertilizer, SelIdx<IdxC>().Idx).Resources.ToString();
                Resources<TextMPUGUIC>(ResTypes.Wood).Text = Environment<AmountResourcesC>(EnvTypes.AdultForest, SelIdx<IdxC>().Idx).Resources.ToString();
                Resources<TextMPUGUIC>(ResTypes.Ore).Text = Environment<AmountResourcesC>(EnvTypes.Hill, SelIdx<IdxC>().Idx).Resources.ToString();
            }
            else
            {
                Info<ButtonUIC>().SetActiveParent(false);
            }
        }
    }
}
using static Game.Game.EntityCellBuildPool;
using static Game.Game.EntityCellEnvPool;
using static Game.Game.EntityPool;
using static Game.Game.EntityLeftEnvUIPool;

namespace Game.Game
{
    struct EnvUIS : IEcsRunSystem
    {
        public void Run()
        {
            ref var build_sel = ref Build<BuildC>(SelIdx<IdxC>().Idx);


            if (SelIdx<SelIdxEC>().IsSelCell && !build_sel.Is(BuildTypes.City))
            {
                Info<ButtonUIC>().SetActiveParent(true);

                Resources<TextUIC>(ResTypes.Food).Text = Environment<ResourcesC>(EnvTypes.Fertilizer, SelIdx<IdxC>().Idx).Resources.ToString();
                Resources<TextUIC>(ResTypes.Wood).Text = Environment<ResourcesC>(EnvTypes.AdultForest, SelIdx<IdxC>().Idx).Resources.ToString();
                Resources<TextUIC>(ResTypes.Ore).Text = Environment<ResourcesC>(EnvTypes.Hill, SelIdx<IdxC>().Idx).Resources.ToString();
            }
            else
            {
                Info<ButtonUIC>().SetActiveParent(false);
            }
        }
    }
}
using static Game.Game.CellBuildE;
using static Game.Game.CellEnvironmentEs;
using static Game.Game.EntityPool;
using static Game.Game.EntityLeftEnvUIPool;

namespace Game.Game
{
    struct EnvUIS : IEcsRunSystem
    {
        public void Run()
        {
            ref var build_sel = ref Build<BuildingTC>(SelIdx<IdxC>().Idx);


            if (SelIdx<SelIdxEC>().IsSelCell && !build_sel.Is(BuildingTypes.City))
            {
                Info<ButtonUIC>().SetActiveParent(true);

                Resources<TextMPUGUIC>(ResourceTypes.Food).Text = Environment<AmountC>(EnvironmentTypes.Fertilizer, SelIdx<IdxC>().Idx).Amount.ToString();
                Resources<TextMPUGUIC>(ResourceTypes.Wood).Text = Environment<AmountC>(EnvironmentTypes.AdultForest, SelIdx<IdxC>().Idx).Amount.ToString();
                Resources<TextMPUGUIC>(ResourceTypes.Ore).Text = Environment<AmountC>(EnvironmentTypes.Hill, SelIdx<IdxC>().Idx).Amount.ToString();
            }
            else
            {
                Info<ButtonUIC>().SetActiveParent(false);
            }
        }
    }
}
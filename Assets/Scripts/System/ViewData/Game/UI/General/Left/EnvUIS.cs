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

                Resources<TextMPUGUIC>(ResTypes.Food).Text = Environment<AmountC>(EnvTypes.Fertilizer, SelIdx<IdxC>().Idx).Amount.ToString();
                Resources<TextMPUGUIC>(ResTypes.Wood).Text = Environment<AmountC>(EnvTypes.AdultForest, SelIdx<IdxC>().Idx).Amount.ToString();
                Resources<TextMPUGUIC>(ResTypes.Ore).Text = Environment<AmountC>(EnvTypes.Hill, SelIdx<IdxC>().Idx).Amount.ToString();
            }
            else
            {
                Info<ButtonUIC>().SetActiveParent(false);
            }
        }
    }
}
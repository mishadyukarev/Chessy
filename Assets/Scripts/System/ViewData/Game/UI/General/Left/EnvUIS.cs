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
            ref var build_sel = ref Build<BuildingTC>(SelectedIdxE.IdxC.Idx);


            if (SelectedIdxE.IsSelCell && !build_sel.Is(BuildingTypes.City))
            {
                Info<ButtonUIC>().SetActiveParent(true);

                Resources<TextMPUGUIC>(ResourceTypes.Food).Text = Resources(EnvironmentTypes.Fertilizer, SelectedIdxE.IdxC.Idx).Amount.ToString();
                Resources<TextMPUGUIC>(ResourceTypes.Wood).Text = Resources(EnvironmentTypes.AdultForest, SelectedIdxE.IdxC.Idx).Amount.ToString();
                Resources<TextMPUGUIC>(ResourceTypes.Ore).Text = Resources(EnvironmentTypes.Hill, SelectedIdxE.IdxC.Idx).Amount.ToString();
            }
            else
            {
                Info<ButtonUIC>().SetActiveParent(false);
            }
        }
    }
}
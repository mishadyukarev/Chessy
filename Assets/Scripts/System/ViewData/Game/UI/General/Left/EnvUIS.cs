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
            ref var build_sel = ref Build<BuildingTC>(EntitiesPool.SelectedIdxE.IdxC.Idx);


            if (EntitiesPool.SelectedIdxE.IsSelCell && !build_sel.Is(BuildingTypes.City))
            {
                Info<ButtonUIC>().SetActiveParent(true);

                Resources<TextUIC>(ResourceTypes.Food).Text = Resources(EnvironmentTypes.Fertilizer, EntitiesPool.SelectedIdxE.IdxC.Idx).Amount.ToString();
                Resources<TextUIC>(ResourceTypes.Wood).Text = Resources(EnvironmentTypes.AdultForest, EntitiesPool.SelectedIdxE.IdxC.Idx).Amount.ToString();
                Resources<TextUIC>(ResourceTypes.Ore).Text = Resources(EnvironmentTypes.Hill, EntitiesPool.SelectedIdxE.IdxC.Idx).Amount.ToString();
            }
            else
            {
                Info<ButtonUIC>().SetActiveParent(false);
            }
        }
    }
}
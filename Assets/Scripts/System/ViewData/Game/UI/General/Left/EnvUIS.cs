using static Game.Game.CellEnvironmentEs;
using static Game.Game.EntityLeftEnvUIPool;

namespace Game.Game
{
    struct EnvUIS : IEcsRunSystem
    {
        public void Run()
        {
            ref var build_sel = ref CellBuildEs.Build(Entities.SelectedIdxE.IdxC.Idx).BuildTC;


            if (Entities.SelectedIdxE.IsSelCell && !build_sel.Is(BuildingTypes.City))
            {
                Info<ButtonUIC>().SetActiveParent(true);

                Resources<TextUIC>(ResourceTypes.Food).Text = Environment(EnvironmentTypes.Fertilizer, Entities.SelectedIdxE.IdxC.Idx).Resources.Amount.ToString();
                Resources<TextUIC>(ResourceTypes.Wood).Text = Environment(EnvironmentTypes.AdultForest, Entities.SelectedIdxE.IdxC.Idx).Resources.Amount.ToString();
                Resources<TextUIC>(ResourceTypes.Ore).Text = Environment(EnvironmentTypes.Hill, Entities.SelectedIdxE.IdxC.Idx).Resources.Amount.ToString();
            }
            else
            {
                Info<ButtonUIC>().SetActiveParent(false);
            }
        }
    }
}
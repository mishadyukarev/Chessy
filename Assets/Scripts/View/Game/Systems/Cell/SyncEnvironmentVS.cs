using Chessy.Game.Model.Entity;

namespace Chessy.Game
{
    public struct SyncEnvironmentVS
    {
        public void Run(in byte cell_start, in EntitiesViewGame vEs, in EntitiesModelGame e)
        {
            if (e.SelectedCell == cell_start)
            {
                vEs.EnvironmentVEs(cell_start).AnimationC.Play();
            }


            if (e.AdultForestC(cell_start).HaveAnyResources)
            {
                vEs.EnvironmentVE(cell_start, EnvironmentTypes.AdultForest).GO.SetActive(true);

                vEs.EnvironmentVEs(cell_start).HillUnderC.GO.SetActive(e.HillC(cell_start).HaveAnyResources);

                vEs.EnvironmentVE(cell_start, EnvironmentTypes.Hill).GO.SetActive(false);
            }
            else
            {
                vEs.EnvironmentVE(cell_start, EnvironmentTypes.AdultForest).GO.SetActive(false);
                vEs.EnvironmentVEs(cell_start).HillUnderC.GO.SetActive(false);
                vEs.EnvironmentVE(cell_start, EnvironmentTypes.Hill).GO.SetActive(e.HillC(cell_start).HaveAnyResources);
            }

            vEs.EnvironmentVE(cell_start, EnvironmentTypes.Fertilizer).GO.SetActive(e.FertilizeC(cell_start).HaveAnyResources);
            vEs.EnvironmentVE(cell_start, EnvironmentTypes.YoungForest).GO.SetActive(e.YoungForestC(cell_start).HaveAnyResources);
            vEs.EnvironmentVE(cell_start, EnvironmentTypes.Mountain).GO.SetActive(e.MountainC(cell_start).HaveAnyResources);
        }
    }
}
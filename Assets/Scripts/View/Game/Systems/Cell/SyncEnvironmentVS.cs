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
                vEs.EnvironmentVE(cell_start, EnvironmentTypes.AdultForest).GameObject.SetActive(true);

                vEs.EnvironmentVEs(cell_start).HillUnderC.GameObject.SetActive(e.HillC(cell_start).HaveAnyResources);

                vEs.EnvironmentVE(cell_start, EnvironmentTypes.Hill).GameObject.SetActive(false);
            }
            else
            {
                vEs.EnvironmentVE(cell_start, EnvironmentTypes.AdultForest).GameObject.SetActive(false);
                vEs.EnvironmentVEs(cell_start).HillUnderC.GameObject.SetActive(false);
                vEs.EnvironmentVE(cell_start, EnvironmentTypes.Hill).GameObject.SetActive(e.HillC(cell_start).HaveAnyResources);
            }

            vEs.EnvironmentVE(cell_start, EnvironmentTypes.Fertilizer).GameObject.SetActive(e.FertilizeC(cell_start).HaveAnyResources);
            vEs.EnvironmentVE(cell_start, EnvironmentTypes.YoungForest).GameObject.SetActive(e.YoungForestC(cell_start).HaveAnyResources);
            vEs.EnvironmentVE(cell_start, EnvironmentTypes.Mountain).GameObject.SetActive(e.MountainC(cell_start).HaveAnyResources);
        }
    }
}
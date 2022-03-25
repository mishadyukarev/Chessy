namespace Chessy.Game
{
    sealed class FarmExtractFertilizeUpdateMS : SystemModelGameAbs, IEcsRunSystem
    {
        internal FarmExtractFertilizeUpdateMS(in Chessy.Game.Entity.Model.EntitiesModelGame ents) : base(ents)
        {
        }

        public void Run()
        {
            for (byte cell_0 = 0; cell_0 < eMGame.LengthCells; cell_0++)
            {
                if (eMGame.FarmExtractFertilizeE(cell_0).HaveAnyResources)
                {
                    var extract = eMGame.FarmExtractFertilizeE(cell_0).Resources;

                    eMGame.ResourcesC(eMGame.BuildingPlayerTC(cell_0).Player, ResourceTypes.Food).Resources += extract;
                    eMGame.FertilizeC(cell_0).Resources -= extract;

                    //if (!E.FertilizeC(cell_0).HaveAnyResources)
                    //{
                    //    E.BuildingTC(cell_0).Building = BuildingTypes.None;
                    //}
                }
            }
        }
    }
}
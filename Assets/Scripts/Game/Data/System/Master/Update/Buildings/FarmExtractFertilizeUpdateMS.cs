﻿namespace Chessy.Game
{
    sealed class FarmExtractFertilizeUpdateMS : SystemAbstract, IEcsRunSystem
    {
        internal FarmExtractFertilizeUpdateMS(in Chessy.Game.Entity.Model.EntitiesModel ents) : base(ents)
        {
        }

        public void Run()
        {
            for (byte idx_0 = 0; idx_0 < E.LengthCells; idx_0++)
            {
                if (E.FarmExtractFertilizeE(idx_0).HaveAnyResources)
                {
                    var extract = E.FarmExtractFertilizeE(idx_0).Resources;

                    E.ResourcesC(E.BuildingPlayerTC(idx_0).Player, ResourceTypes.Food).Resources += extract;
                    E.FertilizeC(idx_0).Resources -= extract;

                    //if (!E.FertilizeC(idx_0).HaveAnyResources)
                    //{
                    //    E.BuildingTC(idx_0).Building = BuildingTypes.None;
                    //}
                }
            }
        }
    }
}
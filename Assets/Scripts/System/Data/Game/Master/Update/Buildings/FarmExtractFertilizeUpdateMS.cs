﻿namespace Game.Game
{
    sealed class FarmExtractFertilizeUpdateMS : SystemAbstract, IEcsRunSystem
    {
        internal FarmExtractFertilizeUpdateMS(in Entities ents) : base(ents)
        {
        }

        public void Run()
        {
            for (byte idx_0 = 0; idx_0 < E.LengthCells; idx_0++)
            {
                if (E.FarmExtractFertilizeE(idx_0).ResourcesC.HaveAny)
                {
                    var extract = E.FarmExtractFertilizeE(idx_0).ResourcesC.Resources;

                    E.ResourcesC(E.BuildPlayerTC(idx_0).Player, ResourceTypes.Food).Resources += extract;
                    E.FertilizeC(idx_0).Resources -= extract;

                    if (!E.FertilizeC(idx_0).HaveAny)
                    {
                        E.BuildTC(idx_0).Build = BuildingTypes.None;
                    }
                }
            }
        }
    }
}
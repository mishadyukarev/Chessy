using Chessy.Game.Values.Cell.Environment;

namespace Chessy.Game.System.Model
{
    sealed class FarmExtractGetCellsS : CellSystem, IEcsRunSystem
    {
        internal FarmExtractGetCellsS(in byte idx, in EntitiesModel eM) : base(idx, eM)
        {
        }

        public void Run()
        {
            E.FarmExtractFertilizeE(Idx).Resources = 0;

            if (E.BuildingTC(Idx).Is(BuildingTypes.Farm))
            {
                if (E.FertilizeC(Idx).HaveAnyResources)
                {
                    var extract = EnvironmentValues.FARM_EXTRACT;

                    //if (E.BuildingsInfo(E.BuildingMainE(Idx)).HaveCenterUpgrade)
                    //{
                    //    extract += Environment_Values.FARM_CENTER_UPGRADE;
                    //}

                    if (E.FertilizeC(Idx).Resources < extract) extract = E.FertilizeC(Idx).Resources;

                    E.FarmExtractFertilizeE(Idx).Resources = extract;
                }
            }
        }
    }
}
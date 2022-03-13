namespace Chessy.Game
{
    sealed class PawnExtractHillUpdateMS : SystemAbstract, IEcsRunSystem
    {
        internal PawnExtractHillUpdateMS(in EntitiesModel ents) : base(ents)
        {
        }

        public void Run()
        {
            for (byte idx_0 = 0; idx_0 < E.LengthCells; idx_0++)
            {
                if (E.PawnExtractHillE(idx_0).HaveAnyResources)
                {
                    var extract = E.PawnExtractHillE(idx_0).Resources;

                    E.HillC(idx_0).Resources -= extract;
                    E.PlayerInfoE(E.UnitPlayerTC(idx_0).Player).ResourcesC(ResourceTypes.Ore).Resources += extract;


                    //if (E.AdultForestC(idx_0).HaveAny)
                    //{

                    //}
                    //else
                    //{
                    //    E.BuildTC(idx_0).Build = BuildingTypes.None;

                    //    E.YoungForestC(idx_0).Resources = CellEnvironment_Values.ENVIRONMENT_MAX;
                    //}
                }
            }
        }
    }
}
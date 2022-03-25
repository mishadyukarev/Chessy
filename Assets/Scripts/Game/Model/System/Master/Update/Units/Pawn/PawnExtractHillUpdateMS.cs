namespace Chessy.Game
{
    sealed class PawnExtractHillUpdateMS : SystemModelGameAbs, IEcsRunSystem
    {
        internal PawnExtractHillUpdateMS(in Chessy.Game.Entity.Model.EntitiesModelGame ents) : base(ents)
        {
        }

        public void Run()
        {
            for (byte cell_0 = 0; cell_0 < eMGame.LengthCells; cell_0++)
            {
                if (eMGame.PawnExtractHillE(cell_0).HaveAnyResources)
                {
                    var extract = eMGame.PawnExtractHillE(cell_0).Resources;

                    eMGame.HillC(cell_0).Resources -= extract;
                    eMGame.PlayerInfoE(eMGame.UnitPlayerTC(cell_0).Player).ResourcesC(ResourceTypes.Ore).Resources += extract;


                    //if (E.AdultForestC(cell_0).HaveAny)
                    //{

                    //}
                    //else
                    //{
                    //    E.BuildTC(cell_0).Build = BuildingTypes.None;

                    //    E.YoungForestC(cell_0).Resources = CellEnvironment_Values.ENVIRONMENT_MAX;
                    //}
                }
            }
        }
    }
}
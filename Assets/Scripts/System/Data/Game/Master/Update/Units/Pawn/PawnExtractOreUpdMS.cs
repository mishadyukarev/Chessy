namespace Game.Game
{
    sealed class PawnExtractOreUpdMS : SystemAbstract, IEcsRunSystem
    {
        internal PawnExtractOreUpdMS(in Entities ents) : base(ents)
        {
        }

        public void Run()
        {
            for (byte idx_0 = 0; idx_0 < Es.LengthCells; idx_0++)
            {
                //unitEs.UnitE.UnitTC.Is(UnitTypes.Pawn) && unitEs.UnitE.ConditionTC.Is(ConditionUnitTypes.Relaxed)
                //&& unitEs.ExtaToolWeaponTC.Is(ToolWeaponTypes.Pick)
                //&& HaveEnvironment && !envEs.AdultForest.HaveAny;

                //if (Es.HillE(idx_0).CanExtractPawn(Es.UnitEs(idx_0), Es.EnvironmentEs(idx_0)))
                //{



                //    //var extract = AmountExtractPawnPick();

                //    //invResEs.Resource(Resource, unitE.PlayerTC.Player).ResourceC.Add(extract);
                //    //Take(extract);
                //}
            }
        }
    }
}
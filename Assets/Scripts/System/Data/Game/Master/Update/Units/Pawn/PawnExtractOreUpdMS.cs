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
                if (Es.HillE(idx_0).CanExtractPawn(Es.UnitEs(idx_0), Es.EnvironmentEs(idx_0)))
                {
                    Es.HillE(idx_0).ExtractPawnPick(Es.UnitE(idx_0), Es.InventorResourcesEs);
                }
            }
        }
    }
}
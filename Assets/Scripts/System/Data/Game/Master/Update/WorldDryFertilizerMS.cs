namespace Game.Game
{
    sealed class WorldDryFertilizerMS : SystemAbstract, IEcsRunSystem
    {
        internal WorldDryFertilizerMS(in Entities ents) : base(ents)
        {
        }

        public void Run()
        {
            for (byte idx_0 = 0; idx_0 < E.LengthCells; idx_0++)
            {
                if (E.FertilizeC(idx_0).HaveAny)
                {
                    E.FertilizeC(idx_0).Resources -= CellEnvironment_Values.DRY_FERTILIZE;
                }
            }
        }
    }
}
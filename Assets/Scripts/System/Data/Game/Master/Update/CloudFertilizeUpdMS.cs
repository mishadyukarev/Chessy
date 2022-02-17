namespace Game.Game
{
    sealed class CloudFertilizeUpdMS : SystemAbstract, IEcsRunSystem
    {
        internal CloudFertilizeUpdMS(in Entities ents) : base(ents)
        {
        }

        public void Run()
        {
            var idx_0 = Es.CenterCloudIdxC.Idx;

            foreach (var idx_1 in Es.CellSpaceWorker.GetIdxsAround(idx_0))
            {
                if (!Es.MountainC(idx_1).HaveAny)
                {
                    Es.FertilizeC(idx_1).Resources = CellEnvironment_Values.STANDART_MAX_AMOUNT_RESOURCES;
                }
            }
        }
    }
}
namespace Game.Game
{
    sealed class CloudFertilizeUpdMS : SystemAbstract, IEcsRunSystem
    {
        internal CloudFertilizeUpdMS(in Entities ents) : base(ents)
        {
        }

        public void Run()
        {
            var idx_0 = Es.WindCloudE.CenterCloud.Idx;

            foreach (var idx_1 in Es.CellSpaceWorker.GetIdxsAround(idx_0))
            {
                if (!Es.MountainE(idx_1).HaveEnvironment)
                {
                    Es.FertilizeE(idx_1).SetMaxResources();
                }
            }
        }
    }
}
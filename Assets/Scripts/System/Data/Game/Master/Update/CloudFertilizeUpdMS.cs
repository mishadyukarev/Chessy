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

            foreach (var idx_1 in Es.CellWorker.GetIdxsAround(idx_0))
            {
                if (!Es.EnvMountainE(idx_1).HaveEnvironment)
                {
                    Es.EnvFertilizeE(idx_1).SetMaxResources();
                }
            }
        }
    }
}
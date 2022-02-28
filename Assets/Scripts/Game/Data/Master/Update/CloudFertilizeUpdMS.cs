namespace Chessy.Game
{
    sealed class CloudFertilizeUpdMS : SystemAbstract, IEcsRunSystem
    {
        internal CloudFertilizeUpdMS(in EntitiesModel ents) : base(ents)
        {
        }

        public void Run()
        {
            var idx_0 = E.CenterCloudIdxC.Idx;

            for (var dirT = DirectTypes.None + 1; dirT < DirectTypes.End; dirT++)
            {
                var idx_1 = E.CellEs(idx_0).AroundCellE(dirT).IdxC.Idx;

                if (!E.MountainC(idx_1).HaveAnyResources)
                {
                    E.FertilizeC(idx_1).Resources = Environment_Values.ENVIRONMENT_MAX;
                }
            }
        }
    }
}
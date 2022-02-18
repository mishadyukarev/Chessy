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

            for (var dirT = DirectTypes.None + 1; dirT < DirectTypes.End; dirT++)
            {
                var idx_1 = Es.CellEs(idx_0).AroundCellE(dirT).IdxC.Idx;

                if (!Es.MountainC(idx_1).HaveAny)
                {
                    Es.FertilizeC(idx_1).Resources = CellEnvironment_Values.STANDART_MAX_AMOUNT_RESOURCES;
                }
            }
        }
    }
}
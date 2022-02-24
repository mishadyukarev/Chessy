namespace Game.Game
{
    sealed class WoodcutterExtractAdultForestUpdateMS : SystemAbstract, IEcsRunSystem
    {
        internal WoodcutterExtractAdultForestUpdateMS(in EntitiesModel ents) : base(ents)
        {
        }

        public void Run()
        {
            for (byte idx_0 = 0; idx_0 < E.LengthCells; idx_0++)
            {
                if (E.WoodcutterExtractE(idx_0).HaveAnyResources)
                {
                    var extract = E.WoodcutterExtractE(idx_0).Resources;

                    E.ResourcesC(E.BuildPlayerTC(idx_0).Player, ResourceTypes.Wood).Resources += extract;

                    E.AdultForestC(idx_0).Resources -= extract;

                    if (!E.AdultForestC(idx_0).HaveAnyResources)
                    {
                        E.AdultForestC(idx_0).Resources = 0;

                        E.BuildingTC(idx_0).Building = BuildingTypes.None;

                        if (UnityEngine.Random.Range(0, 100) < 30)
                        {
                            E.YoungForestC(idx_0).Resources = Environment_Values.ENVIRONMENT_MAX;
                        }
                    }
                }
            }
        }
    }
}
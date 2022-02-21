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
                if (E.WoodcutterExtractE(idx_0).ResourcesC.HaveAny)
                {
                    var extract = E.WoodcutterExtractE(idx_0).ResourcesC.Resources;

                    E.ResourcesC(E.BuildPlayerTC(idx_0).Player, ResourceTypes.Wood).Resources += extract;

                    E.AdultForestC(idx_0).Resources -= extract;

                    if (!E.AdultForestC(idx_0).HaveAny)
                    {
                        E.BuildTC(idx_0).Build = BuildingTypes.None;

                        if (UnityEngine.Random.Range(0, 100) < 30)
                        {
                            E.YoungForestC(idx_0).Resources = CellEnvironment_Values.ENVIRONMENT_MAX;
                        }
                    }
                }
            }
        }
    }
}
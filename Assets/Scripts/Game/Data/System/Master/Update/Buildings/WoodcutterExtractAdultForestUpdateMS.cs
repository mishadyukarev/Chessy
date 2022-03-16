using Chessy.Game.System.Model;
using Chessy.Game.Values.Cell;
using Chessy.Game.Values.Cell.Environment;

namespace Chessy.Game
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

                    E.ResourcesC(E.BuildingPlayerTC(idx_0).Player, ResourceTypes.Wood).Resources += extract;

                    TakeAdultForestResourcesS.TakeAdultForestResources(extract, idx_0, E);

                    if (!E.AdultForestC(idx_0).HaveAnyResources)
                    {
                        E.BuildingTC(idx_0).Building = BuildingTypes.None;

                        if (UnityEngine.Random.Range(0, 100) < 30)
                        {
                            E.YoungForestC(idx_0).Resources = EnvironmentValues.MAX_RESOURCES;
                        }
                    }
                }
            }
        }
    }
}
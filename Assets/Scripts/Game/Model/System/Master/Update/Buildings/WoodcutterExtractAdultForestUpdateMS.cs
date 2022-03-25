using Chessy.Game.Entity.Model;
using Chessy.Game.System.Model;
using Chessy.Game.Values;
using Chessy.Game.Values.Cell.Environment;

namespace Chessy.Game
{
    sealed class WoodcutterExtractAdultForestUpdateMS : SystemModelGameAbs, IEcsRunSystem
    {
        readonly TakeAdultForestResourcesS _takeAdultForestS;

        internal WoodcutterExtractAdultForestUpdateMS(in TakeAdultForestResourcesS takeAdultForestS, in EntitiesModelGame ents) : base(ents)
        {
            _takeAdultForestS = takeAdultForestS;
        }

        public void Run()
        {
            for (byte cell_0 = 0; cell_0 < StartValues.CELLS; cell_0++)
            {
                if (eMGame.WoodcutterExtractE(cell_0).HaveAnyResources)
                {
                    var extract = eMGame.WoodcutterExtractE(cell_0).Resources;

                    eMGame.ResourcesC(eMGame.BuildingPlayerTC(cell_0).Player, ResourceTypes.Wood).Resources += extract;

                    _takeAdultForestS.Take(extract, cell_0);

                    if (!eMGame.AdultForestC(cell_0).HaveAnyResources)
                    {
                        eMGame.BuildingTC(cell_0).Building = BuildingTypes.None;

                        if (UnityEngine.Random.Range(0, 100) < 30)
                        {
                            eMGame.YoungForestC(cell_0).Resources = EnvironmentValues.MAX_RESOURCES;
                        }
                    }
                }
            }
        }
    }
}
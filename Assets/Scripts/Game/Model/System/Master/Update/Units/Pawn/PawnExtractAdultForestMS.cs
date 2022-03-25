using Chessy.Game.Entity.Model;
using Chessy.Game.System.Model;
using Chessy.Game.Values.Cell.Environment;
using Chessy.Game.Values.Cell.Unit.Stats;

namespace Chessy.Game
{
    sealed class PawnExtractAdultForestMS : SystemModelGameAbs, IEcsRunSystem
    {
        readonly TakeAdultForestResourcesS _takeAdultForestS;
        readonly BuildS _buildS;

        public PawnExtractAdultForestMS(in TakeAdultForestResourcesS takeAdultForestS, in BuildS buildS, in EntitiesModelGame ents) : base(ents)
        {
            _takeAdultForestS = takeAdultForestS;
            _buildS = buildS;
        }

        public void Run()
        {
            for (byte cell_0 = 0; cell_0 < eMGame.LengthCells; cell_0++)
            {
                if (eMGame.PawnExtractAdultForestE(cell_0).HaveAnyResources)
                {
                    var extract = eMGame.PawnExtractAdultForestE(cell_0).Resources;

                    eMGame.PlayerInfoE(eMGame.UnitPlayerTC(cell_0).Player).ResourcesC(ResourceTypes.Wood).Resources += extract;
                    _takeAdultForestS.Take(extract, cell_0);

                    if (eMGame.AdultForestC(cell_0).HaveAnyResources)
                    {
                        if (eMGame.BuildingTC(cell_0).Is(BuildingTypes.Camp) || !eMGame.BuildingTC(cell_0).HaveBuilding)
                        {
                            _buildS.Build(BuildingTypes.Woodcutter, LevelTypes.First, eMGame.UnitPlayerTC(cell_0).Player, 1, cell_0);
                        }

                        else if (!eMGame.BuildingTC(cell_0).Is(BuildingTypes.Woodcutter))
                        {
                            eMGame.UnitConditionTC(cell_0).Condition = ConditionUnitTypes.Protected;
                        }
                    }
                    else
                    {
                        eMGame.BuildingTC(cell_0).Building = BuildingTypes.None;

                        eMGame.YoungForestC(cell_0).Resources = EnvironmentValues.MAX_RESOURCES;
                    }
                }
                else if (eMGame.UnitConditionTC(cell_0).Is(ConditionUnitTypes.Relaxed)
                    && eMGame.UnitHpC(cell_0).Health >= HpValues.MAX)
                {
                    eMGame.UnitConditionTC(cell_0).Condition = ConditionUnitTypes.Protected;
                }
            }
        }
    }
}
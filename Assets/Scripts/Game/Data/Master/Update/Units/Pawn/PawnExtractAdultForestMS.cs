using Chessy.Game.System.Model;
using Chessy.Game.Values.Cell;
using Chessy.Game.Values.Cell.Environment;
using Chessy.Game.Values.Cell.Unit.Stats;

namespace Chessy.Game
{
    sealed class PawnExtractAdultForestMS : SystemAbstract, IEcsRunSystem
    {
        public PawnExtractAdultForestMS(in EntitiesModel ents) : base(ents)
        {
        }

        public void Run()
        {
            for (byte idx_0 = 0; idx_0 < E.LengthCells; idx_0++)
            {
                if (E.PawnExtractAdultForestE(idx_0).HaveAnyResources)
                {
                    var extract = E.PawnExtractAdultForestE(idx_0).Resources;

                    E.AdultForestC(idx_0).Resources -= extract;
                    E.PlayerInfoE(E.UnitPlayerTC(idx_0).Player).ResourcesC(ResourceTypes.Wood).Resources += extract;


                    if (E.AdultForestC(idx_0).HaveAnyResources)
                    {
                        if (E.BuildingTC(idx_0).Is(BuildingTypes.Camp) || !E.BuildingTC(idx_0).HaveBuilding)
                        {
                            new BuildS(BuildingTypes.Woodcutter, LevelTypes.First, E.UnitPlayerTC(idx_0).Player, 1, idx_0, E);
                        }

                        else if (!E.BuildingTC(idx_0).Is(BuildingTypes.Woodcutter))
                        {
                            E.UnitConditionTC(idx_0).Condition = ConditionUnitTypes.Protected;
                        }
                    }
                    else
                    {
                        E.BuildingTC(idx_0).Building = BuildingTypes.None;

                        E.YoungForestC(idx_0).Resources = EnvironmentValues.MAX_RESOURCES;
                    }
                }
                else if (E.UnitConditionTC(idx_0).Is(ConditionUnitTypes.Relaxed)
                    && E.UnitHpC(idx_0).Health >= HpValues.MAX)
                {
                    E.UnitConditionTC(idx_0).Condition = ConditionUnitTypes.Protected;
                }
            }
        }
    }
}
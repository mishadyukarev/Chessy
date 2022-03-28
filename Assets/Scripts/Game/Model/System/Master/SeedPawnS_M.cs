using Chessy.Game.Entity.Model;
using Chessy.Game.Values.Cell.Environment;
using Chessy.Game.Values.Cell.Unit.Stats;
using Photon.Realtime;

namespace Chessy.Game.Model.System
{
    public sealed class SeedPawnS_M : SystemModelGameAbs
    {
        readonly CellEs _cellEs;

        public SeedPawnS_M(in CellEs cellEs, in EntitiesModelGame eMGame) : base(eMGame)
        {
            _cellEs = cellEs;
        }

        public void Seed(in AbilityTypes abilityT, in Player sender)
        {
            if (_cellEs.UnitStatsE.StepC.Steps >= StepValues.SEED_PAWN)
            {
                if (_cellEs.BuildEs.MainE.BuildingTC.HaveBuilding && !_cellEs.BuildEs.MainE.BuildingTC.Is(BuildingTypes.Camp))
                {
                    eMGame.RpcPoolEs.SimpleMistake_ToGeneral(MistakeTypes.NeedOtherPlaceSeed, sender);
                }

                else
                {
                    if (!_cellEs.EnvironmentEs.AdultForestC.HaveAnyResources)
                    {
                        if (!_cellEs.EnvironmentEs.YoungForestC.HaveAnyResources)
                        {
                            eMGame.RpcPoolEs.SoundToGeneral(sender, abilityT);

                            _cellEs.EnvironmentEs.YoungForestC.Resources = EnvironmentValues.MAX_RESOURCES;

                            _cellEs.UnitStatsE.StepC.Steps -= StepValues.SEED_PAWN;
                        }

                        else
                        {
                            eMGame.RpcPoolEs.SimpleMistake_ToGeneral(MistakeTypes.NeedOtherPlaceSeed, sender);
                        }
                    }

                    else
                    {
                        eMGame.RpcPoolEs.SimpleMistake_ToGeneral(MistakeTypes.NeedOtherPlaceFarm, sender);
                    }
                }
            }

            else
            {
                eMGame.RpcPoolEs.SimpleMistake_ToGeneral(MistakeTypes.NeedMoreSteps, sender);
            }
        }
    }
}
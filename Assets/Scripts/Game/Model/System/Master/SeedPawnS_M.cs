using Chessy.Game.Entity.Model;
using Chessy.Game.Values.Cell.Environment;
using Chessy.Game.Values.Cell.Unit.Stats;
using Photon.Realtime;

namespace Chessy.Game.Model.System
{
    public sealed class SeedPawnS_M : SystemModelGameAbs
    {
        public SeedPawnS_M(in EntitiesModelGame eMGame) : base(eMGame)
        {
        }

        public void Seed(in byte cell_0, in AbilityTypes abilityT, in Player sender)
        {
            if (eMGame.UnitStepC(cell_0).Steps >= StepValues.SEED_PAWN)
            {
                if (eMGame.BuildingTC(cell_0).HaveBuilding && !eMGame.BuildingTC(cell_0).Is(BuildingTypes.Camp))
                {
                    eMGame.RpcPoolEs.SimpleMistake_ToGeneral(MistakeTypes.NeedOtherPlaceSeed, sender);
                }

                else
                {
                    if (!eMGame.AdultForestC(cell_0).HaveAnyResources)
                    {
                        if (!eMGame.YoungForestC(cell_0).HaveAnyResources)
                        {
                            eMGame.RpcPoolEs.SoundToGeneral(sender, abilityT);

                            eMGame.YoungForestC(cell_0).Resources = EnvironmentValues.MAX_RESOURCES;

                            eMGame.UnitStepC(cell_0).Steps -= StepValues.SEED_PAWN;
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
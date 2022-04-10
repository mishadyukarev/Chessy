using Chessy.Common.Entity;
using Chessy.Common.Model.System;
using Chessy.Game.Model.Entity;
using Chessy.Game.Values;
using Chessy.Game.Values.Cell.Unit;
using Chessy.Game.Values.Cell.Unit.Stats;
using Photon.Realtime;

namespace Chessy.Game.Model.System
{
    sealed class GrowAdultForestS_M : SystemModel
    {
        internal GrowAdultForestS_M(in SystemsModelGame sMG, in EntitiesModelGame eMG) : base(sMG, eMG) { }

        internal void Grow(in byte cell_0, in AbilityTypes abilityT, in Player sender)
        {
            if (!eMG.UnitCooldownAbilitiesC(cell_0).HaveCooldown(abilityT))
            {
                if (eMG.StepUnitC(cell_0).Steps >= StepValues.GROW_ADULT_FOREST)
                {
                    if (eMG.YoungForestC(cell_0).HaveAnyResources)
                    {
                        eMG.YoungForestC(cell_0).Resources = 0;
                        eMG.AdultForestC(cell_0).Resources = EnvironmentValues.MAX_RESOURCES;

                        eMG.StepUnitC(cell_0).Steps -= StepValues.GROW_ADULT_FOREST;
                        eMG.UnitCooldownAbilitiesC(cell_0).Set(abilityT, AbilityCooldownValues.NeedAfterAbility(abilityT));

                        foreach (var idx_1 in eMG.AroundCellsE(cell_0).CellsAround)
                        {
                            if (eMG.YoungForestC(idx_1).HaveAnyResources)
                            {
                                eMG.AdultForestC(idx_1).Resources = EnvironmentValues.MAX_RESOURCES;
                            }
                        }
                        eMG.RpcPoolEs.SoundToGeneral(sender, abilityT);
                    }

                    else eMG.RpcPoolEs.SimpleMistake_ToGeneral(MistakeTypes.NeedOtherPlaceGrowAdultForest, sender);
                }

                else eMG.RpcPoolEs.SimpleMistake_ToGeneral(MistakeTypes.NeedMoreSteps, sender);
            }

            else
            {
                eMG.RpcPoolEs.SoundToGeneral(sender, ClipTypes.Mistake);
            }
        }
    }
}
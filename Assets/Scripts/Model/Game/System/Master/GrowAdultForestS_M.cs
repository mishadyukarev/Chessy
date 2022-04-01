﻿using Chessy.Common.Entity;
using Chessy.Common.Model.System;
using Chessy.Game.Entity.Model;
using Chessy.Game.Model.System;
using Chessy.Game.Values.Cell.Environment;
using Chessy.Game.Values.Cell.Unit;
using Chessy.Game.Values.Cell.Unit.Stats;
using Photon.Realtime;

namespace Chessy.Game.Model.System
{
    sealed class GrowAdultForestS_M : SystemModelGameAbs
    {
        internal GrowAdultForestS_M(in SystemsModelCommon sMC, in EntitiesModelCommon eMC, in SystemsModelGame sMG, in EntitiesModelGame eMG) : base(sMC, eMC, sMG, eMG) { }

        internal void Grow(in byte cell_0, in AbilityTypes abilityT, in Player sender)
        {
            if (!eMG.UnitEs(cell_0).CoolDownC(abilityT).HaveCooldown)
            {
                if (eMG.UnitStepC(cell_0).Steps >= StepValues.GROW_ADULT_FOREST)
                {
                    if (eMG.YoungForestC(cell_0).HaveAnyResources)
                    {
                        eMG.YoungForestC(cell_0).Resources = 0;

                        eMG.AdultForestC(cell_0).Resources = EnvironmentValues.MAX_RESOURCES;

                        eMG.UnitStepC(cell_0).Steps -= StepValues.GROW_ADULT_FOREST;

                        eMG.UnitEs(cell_0).CoolDownC(abilityT).Cooldown = AbilityCooldownValues.AFTER_GROW_ADULT_FOREST;


                        foreach (var idx_1 in eMG.CellEs(cell_0).AroundCellsEs.IdxsAround)
                        {
                            if (eMG.YoungForestC(idx_1).HaveAnyResources)
                            {
                                eMG.AdultForestC(idx_1).Resources = EnvironmentValues.MAX_RESOURCES;
                            }
                        }



                        eMG.RpcPoolEs.SoundToGeneral(sender, abilityT);


                        foreach (var idxC_1 in eMG.CellEs(cell_0).AroundCellsEs.AroundCellIdxsC)
                        {
                            var idx_1 = idxC_1.Idx;

                            if (eMG.UnitTC(idx_1).HaveUnit)
                            {
                                if (eMG.UnitPlayerTC(idx_1).Is(eMG.UnitPlayerTC(cell_0).PlayerT))
                                {
                                    //if (!CellUnitEffectsEs.HaveEffect<HaveEffectC>(UnitStatTypes.Steps, idx_1).Have)
                                    //{
                                    //    CellUnitEffectsEs.HaveEffect<HaveEffectC>(UnitStatTypes.Steps, idx_1).Have = true;
                                    //}
                                }
                            }
                        }

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
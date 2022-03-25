using Chessy.Game.Entity.Model;
using Chessy.Game.Values.Cell.Environment;
using Chessy.Game.Values.Cell.Unit;
using Chessy.Game.Values.Cell.Unit.Stats;
using Photon.Realtime;

namespace Chessy.Game.Model.System
{
    public sealed class GrowAdultForestS_M : SystemModelGameAbs
    {
        public GrowAdultForestS_M(in EntitiesModelGame eMGame) : base(eMGame)
        {
        }

        public void Grow(in byte cell_0, in AbilityTypes abilityT, in Player sender)
        {
            if (!eMGame.UnitEs(cell_0).CoolDownC(abilityT).HaveCooldown)
            {
                if (eMGame.UnitStepC(cell_0).Steps >= StepValues.GROW_ADULT_FOREST)
                {
                    if (eMGame.YoungForestC(cell_0).HaveAnyResources)
                    {
                        eMGame.YoungForestC(cell_0).Resources = 0;

                        eMGame.AdultForestC(cell_0).Resources = EnvironmentValues.MAX_RESOURCES;

                        eMGame.UnitStepC(cell_0).Steps -= StepValues.GROW_ADULT_FOREST;

                        eMGame.UnitEs(cell_0).CoolDownC(abilityT).Cooldown = AbilityCooldownValues.AFTER_GROW_ADULT_FOREST;


                        foreach (var idx_1 in eMGame.CellEs(cell_0).IdxsAround)
                        {
                            if (eMGame.YoungForestC(idx_1).HaveAnyResources)
                            {
                                eMGame.AdultForestC(idx_1).Resources = EnvironmentValues.MAX_RESOURCES;
                            }
                        }



                        eMGame.RpcPoolEs.SoundToGeneral(sender, abilityT);


                        foreach (var idxC_1 in eMGame.CellEs(cell_0).AroundCellIdxsC)
                        {
                            var idx_1 = idxC_1.Idx;

                            if (eMGame.UnitTC(idx_1).HaveUnit)
                            {
                                if (eMGame.UnitPlayerTC(idx_1).Is(eMGame.UnitPlayerTC(cell_0).Player))
                                {
                                    //if (!CellUnitEffectsEs.HaveEffect<HaveEffectC>(UnitStatTypes.Steps, idx_1).Have)
                                    //{
                                    //    CellUnitEffectsEs.HaveEffect<HaveEffectC>(UnitStatTypes.Steps, idx_1).Have = true;
                                    //}
                                }
                            }
                        }

                    }

                    else eMGame.RpcPoolEs.SimpleMistake_ToGeneral(MistakeTypes.NeedOtherPlaceGrowAdultForest, sender);
                }

                else eMGame.RpcPoolEs.SimpleMistake_ToGeneral(MistakeTypes.NeedMoreSteps, sender);
            }

            else
            {
                eMGame.RpcPoolEs.SoundToGeneral(sender, ClipTypes.Mistake);
            }
        }
    }
}
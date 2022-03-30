using Chessy.Game.Entity.Model;
using Chessy.Game.System.Model;
using Chessy.Game.Values.Cell.Environment;
using Chessy.Game.Values.Cell.Unit;
using Chessy.Game.Values.Cell.Unit.Stats;
using Photon.Realtime;

namespace Chessy.Game.Model.System
{
    sealed class GrowAdultForestS_M : SystemModelGameAbs
    {
        internal GrowAdultForestS_M(in SystemsModelGame sMGame, in EntitiesModelGame eMGame) : base(sMGame, eMGame) { }

        internal void Grow(in byte cell_0, in AbilityTypes abilityT, in Player sender)
        {
            if (!e.UnitEs(cell_0).CoolDownC(abilityT).HaveCooldown)
            {
                if (e.UnitStepC(cell_0).Steps >= StepValues.GROW_ADULT_FOREST)
                {
                    if (e.YoungForestC(cell_0).HaveAnyResources)
                    {
                        e.YoungForestC(cell_0).Resources = 0;

                        e.AdultForestC(cell_0).Resources = EnvironmentValues.MAX_RESOURCES;

                        e.UnitStepC(cell_0).Steps -= StepValues.GROW_ADULT_FOREST;

                        e.UnitEs(cell_0).CoolDownC(abilityT).Cooldown = AbilityCooldownValues.AFTER_GROW_ADULT_FOREST;


                        foreach (var idx_1 in e.CellEs(cell_0).AroundCellsEs.IdxsAround)
                        {
                            if (e.YoungForestC(idx_1).HaveAnyResources)
                            {
                                e.AdultForestC(idx_1).Resources = EnvironmentValues.MAX_RESOURCES;
                            }
                        }



                        e.RpcPoolEs.SoundToGeneral(sender, abilityT);


                        foreach (var idxC_1 in e.CellEs(cell_0).AroundCellsEs.AroundCellIdxsC)
                        {
                            var idx_1 = idxC_1.Idx;

                            if (e.UnitTC(idx_1).HaveUnit)
                            {
                                if (e.UnitPlayerTC(idx_1).Is(e.UnitPlayerTC(cell_0).Player))
                                {
                                    //if (!CellUnitEffectsEs.HaveEffect<HaveEffectC>(UnitStatTypes.Steps, idx_1).Have)
                                    //{
                                    //    CellUnitEffectsEs.HaveEffect<HaveEffectC>(UnitStatTypes.Steps, idx_1).Have = true;
                                    //}
                                }
                            }
                        }

                    }

                    else e.RpcPoolEs.SimpleMistake_ToGeneral(MistakeTypes.NeedOtherPlaceGrowAdultForest, sender);
                }

                else e.RpcPoolEs.SimpleMistake_ToGeneral(MistakeTypes.NeedMoreSteps, sender);
            }

            else
            {
                e.RpcPoolEs.SoundToGeneral(sender, ClipTypes.Mistake);
            }
        }
    }
}
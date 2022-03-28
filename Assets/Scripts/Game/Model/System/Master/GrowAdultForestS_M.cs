using Chessy.Game.Entity.Model;
using Chessy.Game.Values.Cell.Environment;
using Chessy.Game.Values.Cell.Unit;
using Chessy.Game.Values.Cell.Unit.Stats;
using Photon.Realtime;

namespace Chessy.Game.Model.System
{
    public sealed class GrowAdultForestS_M : SystemModelGameAbs
    {
        readonly CellEs _cellEs;

        public GrowAdultForestS_M(in CellEs cellEs, in EntitiesModelGame eMGame) : base(eMGame)
        {
            _cellEs = cellEs;
        }

        public void Grow(in AbilityTypes abilityT, in Player sender)
        {
            if (!_cellEs.UnitEs.CoolDownC(abilityT).HaveCooldown)
            {
                if (_cellEs.UnitStatsE.StepC.Steps >= StepValues.GROW_ADULT_FOREST)
                {
                    if (_cellEs.EnvironmentEs.YoungForestC.HaveAnyResources)
                    {
                        _cellEs.EnvironmentEs.YoungForestC.Resources = 0;

                        _cellEs.EnvironmentEs.AdultForestC.Resources = EnvironmentValues.MAX_RESOURCES;

                        _cellEs.UnitStatsE.StepC.Steps -= StepValues.GROW_ADULT_FOREST;

                        _cellEs.UnitEs.CoolDownC(abilityT).Cooldown = AbilityCooldownValues.AFTER_GROW_ADULT_FOREST;


                        foreach (var idx_1 in _cellEs.AroundCellsEs.IdxsAround)
                        {
                            if (e.YoungForestC(idx_1).HaveAnyResources)
                            {
                                e.AdultForestC(idx_1).Resources = EnvironmentValues.MAX_RESOURCES;
                            }
                        }



                        e.RpcPoolEs.SoundToGeneral(sender, abilityT);


                        foreach (var idxC_1 in _cellEs.AroundCellsEs.AroundCellIdxsC)
                        {
                            var idx_1 = idxC_1.Idx;

                            if (e.UnitTC(idx_1).HaveUnit)
                            {
                                if (e.UnitPlayerTC(idx_1).Is(_cellEs.UnitEs.MainE.PlayerTC.Player))
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
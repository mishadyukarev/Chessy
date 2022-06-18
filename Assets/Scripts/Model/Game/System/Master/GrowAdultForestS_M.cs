using Chessy.Game.Model.Entity;
using Chessy.Game.Values;
using Chessy.Game.Values.Cell.Unit;
using Chessy.Game.Values.Cell.Unit.Stats;
using Photon.Realtime;

namespace Chessy.Game.Model.System
{
    sealed partial class UnitAbilitiesSystems : SystemModel
    {
        internal void Grow(in byte cell_0, in AbilityTypes abilityT, in Player sender)
        {
            if (!_eMG.UnitCooldownAbilitiesC(cell_0).HaveCooldown(abilityT))
            {
                if (_eMG.StepUnitC(cell_0).Steps >= StepValues.GROW_ADULT_FOREST)
                {
                    if (_eMG.YoungForestC(cell_0).HaveAnyResources)
                    {
                        _eMG.YoungForestC(cell_0).Resources = 0;
                        _eMG.AdultForestC(cell_0).Resources = EnvironmentValues.MAX_RESOURCES;

                        _eMG.StepUnitC(cell_0).Steps -= StepValues.GROW_ADULT_FOREST;
                        _eMG.UnitCooldownAbilitiesC(cell_0).Set(abilityT, AbilityCooldownValues.NeedAfterAbility(abilityT));

                        foreach (var idx_1 in _eMG.AroundCellsE(cell_0).CellsAround)
                        {
                            if (_eMG.YoungForestC(idx_1).HaveAnyResources)
                            {
                                _eMG.AdultForestC(idx_1).Resources = EnvironmentValues.MAX_RESOURCES;
                            }
                        }
                        _eMG.RpcPoolEs.SoundToGeneral(sender, abilityT);
                    }

                    else _eMG.RpcPoolEs.SimpleMistake_ToGeneral(MistakeTypes.NeedOtherPlaceGrowAdultForest, sender);
                }

                else _eMG.RpcPoolEs.SimpleMistake_ToGeneral(MistakeTypes.NeedMoreSteps, sender);
            }

            else
            {
                _eMG.RpcPoolEs.SoundToGeneral(sender, ClipTypes.Mistake);
            }
        }
    }
}
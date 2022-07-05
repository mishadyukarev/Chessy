﻿using Chessy.Model.Values;
using Photon.Realtime;
namespace Chessy.Model.System
{
    sealed partial class UnitAbilitiesSystems : SystemModelAbstract
    {
        internal void TryGrowAdultForestWithElfemaleM(in byte cell_0, in AbilityTypes abilityT, in Player sender)
        {
            if (!_e.UnitCooldownAbilitiesC(cell_0).HaveCooldown(abilityT))
            {
                if (_e.EnergyUnitC(cell_0).Energy >= StepValues.GROW_ADULT_FOREST)
                {
                    if (_e.YoungForestC(cell_0).HaveAnyResources)
                    {
                        _e.YoungForestC(cell_0).Resources = 0;
                        _e.AdultForestC(cell_0).Resources = ValuesChessy.MAX_RESOURCES;

                        _e.EnergyUnitC(cell_0).Energy -= StepValues.GROW_ADULT_FOREST;
                        _e.UnitCooldownAbilitiesC(cell_0).Set(abilityT, AbilityCooldownUnitValues.NeedAfterAbility(abilityT));

                        foreach (var idx_1 in _e.AroundCellsE(cell_0).CellsAround)
                        {
                            if (_e.YoungForestC(idx_1).HaveAnyResources)
                            {
                                _e.AdultForestC(idx_1).Resources = ValuesChessy.MAX_RESOURCES;
                            }
                        }
                        _s.RpcSs.SoundToGeneral(sender, abilityT);
                    }

                    else _s.RpcSs.SimpleMistakeToGeneral(MistakeTypes.NeedOtherPlaceGrowAdultForest, sender);
                }

                else _s.RpcSs.SimpleMistakeToGeneral(MistakeTypes.NeedMoreSteps, sender);
            }

            else
            {
                _s.RpcSs.ExecuteSoundActionToGeneral(sender, ClipTypes.Mistake);
            }
        }
    }
}
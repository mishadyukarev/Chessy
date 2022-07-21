using Chessy.Model.Enum;
using Chessy.Model.Values;
using Photon.Realtime;
namespace Chessy.Model.System
{
    sealed partial class UnitAbilitiesSystems : SystemModelAbstract
    {
        internal void TryGrowAdultForestWithElfemaleM(in byte cell_0, in AbilityTypes abilityT, in Player sender)
        {
            if (!_cooldownAbilityCs[cell_0].HaveCooldown(abilityT))
            {
                if (_e.YoungForestC(cell_0).HaveAnyResources)
                {
                    _e.YoungForestC(cell_0).Resources = 0;
                    _e.AdultForestC(cell_0).Resources = ValuesChessy.MAX_RESOURCES_ENVIRONMENT;

                    _cooldownAbilityCs[cell_0].Set(abilityT, AbilityCooldownUnitValues.NeedAfterAbility(abilityT));

                    foreach (var idx_1 in _e.IdxsCellsAround(cell_0))
                    {
                        if (_e.YoungForestC(idx_1).HaveAnyResources)
                        {
                            _e.AdultForestC(idx_1).Resources = ValuesChessy.MAX_RESOURCES_ENVIRONMENT;
                        }
                    }
                    _s.RpcSs.SoundToGeneral(sender, abilityT);
                }

                else _s.RpcSs.SimpleMistakeToGeneral(MistakeTypes.NeedOtherPlaceGrowAdultForest, sender);
            }

            else
            {
                _s.RpcSs.ExecuteSoundActionToGeneral(sender, ClipTypes.Mistake);
            }
        }
    }
}
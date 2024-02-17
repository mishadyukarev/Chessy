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
                if (environmentCs[cell_0].HaveEnvironment(EnvironmentTypes.YoungForest))
                {
                    environmentCs[cell_0].Set(EnvironmentTypes.YoungForest, 0);
                    environmentCs[cell_0].Set(EnvironmentTypes.AdultForest, ValuesChessy.MAX_RESOURCES_ENVIRONMENT);

                    _cooldownAbilityCs[cell_0].Set(abilityT, AbilityCooldownUnitValues.NeedAfterAbility(abilityT));

                    foreach (var idx_1 in IdxsAroundCellC(cell_0).IdxCellsAroundArray)
                    {
                        if (environmentCs[idx_1].HaveEnvironment(EnvironmentTypes.YoungForest))
                        {
                            environmentCs[idx_1].Set(EnvironmentTypes.AdultForest, ValuesChessy.MAX_RESOURCES_ENVIRONMENT);
                        }
                    }
                    s.RpcSs.SoundToGeneral(sender, abilityT);
                }

                else s.RpcSs.SimpleMistakeToGeneral(MistakeTypes.NeedOtherPlaceGrowAdultForest, sender);
            }

            else
            {
                s.RpcSs.ExecuteSoundActionToGeneral(sender, ClipTypes.Mistake);
            }
        }
    }
}
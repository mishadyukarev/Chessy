using Chessy.Model.Entity;
using Chessy.Model.Values;
using Photon.Pun;
using Photon.Realtime;
namespace Chessy.Model.System
{
    sealed class StunElfemaleS_M : SystemModelAbstract
    {
        internal StunElfemaleS_M(in SystemsModel sM, EntitiesModel eM) : base(sM, eM)
        {
        }

        internal void TryStunEnemyWithElfemaleM(in byte cell_from, in byte cell_to, in AbilityTypes abilityT, in Player sender)
        {
            if (!_cooldownAbilityCs[cell_from].HaveCooldown(abilityT))
            {
                if (environmentCs[cell_to].HaveEnvironment(EnvironmentTypes.AdultForest))
                {
                    if (unitCs[cell_from].PlayerT != unitCs[cell_to].PlayerT)
                    {
                        effectsUnitCs[cell_to].StunHowManyUpdatesNeedStay = StunUnitValues.AMOUNT_STUN_AFTER_ABILITY_ELFEMALE;
                        _cooldownAbilityCs[cell_from].Set(abilityT, AbilityCooldownUnitValues.NeedAfterAbility(abilityT));

                        s.RpcSs.SoundToGeneral(RpcTarget.All, abilityT);


                        foreach (var idx_1 in idxsAroundCellCs[cell_to].IdxCellsAroundArray)
                        {
                            if (environmentCs[idx_1].HaveEnvironment(EnvironmentTypes.AdultForest))
                            {
                                if (unitCs[idx_1].HaveUnit && unitCs[idx_1].PlayerT == unitCs[cell_to].PlayerT)
                                {
                                    effectsUnitCs[idx_1].StunHowManyUpdatesNeedStay = StunUnitValues.AMOUNT_STUN_AFTER_ABILITY_ELFEMALE;
                                }
                            }
                        }
                    }
                }
            }

            else s.RpcSs.ExecuteSoundActionToGeneral(sender, ClipTypes.Mistake);
        }
    }
}
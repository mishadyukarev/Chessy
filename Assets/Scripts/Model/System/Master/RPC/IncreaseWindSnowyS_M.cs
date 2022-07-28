using Chessy.Model.Values;
using Photon.Realtime;
namespace Chessy.Model.System
{
    sealed partial class UnitAbilitiesSystems : SystemModelAbstract
    {
        internal void IncreaseWindWithRainyM(in byte cell_0, in AbilityTypes abilityT, in Player sender, in bool needIncrese = true)
        {
            if (!_cooldownAbilityCs[cell_0].HaveCooldown(abilityT))
            {
                if (needIncrese)
                {
                    if (!WindC.IsMaxSpeed())
                    {
                        _cooldownAbilityCs[cell_0].Set(abilityT, AbilityCooldownUnitValues.NeedAfterAbility(abilityT));

                        WindC.Speed++;

                        _s.RpcSs.SoundToGeneral(Photon.Pun.RpcTarget.All, AbilityTypes.ChangeDirectionWind);
                    }
                    else
                    {
                        _s.RpcSs.SimpleMistakeToGeneral(MistakeTypes.MaxSpeedWind, sender);
                    }
                }

                else
                {
                    if (!WindC.IsMinSpeed())
                    {
                        _cooldownAbilityCs[cell_0].Set(abilityT, AbilityCooldownUnitValues.NeedAfterAbility(abilityT));

                        WindC.Speed--;

                        _s.RpcSs.SoundToGeneral(Photon.Pun.RpcTarget.All, AbilityTypes.ChangeDirectionWind);
                    }
                    else
                    {
                        _s.RpcSs.SimpleMistakeToGeneral(MistakeTypes.MinSpeedWind, sender);
                    }
                }
            }
        }

        internal void DecreaseWindWithRainyM(in byte cell_0, in AbilityTypes abilityT, in Player sender)
        {
            IncreaseWindWithRainyM(cell_0, abilityT, sender, false);
        }
    }
}
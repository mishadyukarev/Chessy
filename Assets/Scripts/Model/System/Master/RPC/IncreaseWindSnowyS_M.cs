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
                    if (!windC.IsMaxSpeed())
                    {
                        _cooldownAbilityCs[cell_0].Set(abilityT, AbilityCooldownUnitValues.NeedAfterAbility(abilityT));

                        windC.Speed++;

                        s.RpcSs.SoundToGeneral(Photon.Pun.RpcTarget.All, AbilityTypes.ChangeDirectionWind);
                    }
                    else
                    {
                        s.RpcSs.SimpleMistakeToGeneral(MistakeTypes.MaxSpeedWind, sender);
                    }
                }

                else
                {
                    if (!windC.IsMinSpeed())
                    {
                        _cooldownAbilityCs[cell_0].Set(abilityT, AbilityCooldownUnitValues.NeedAfterAbility(abilityT));

                        windC.Speed--;

                        s.RpcSs.SoundToGeneral(Photon.Pun.RpcTarget.All, AbilityTypes.ChangeDirectionWind);
                    }
                    else
                    {
                        s.RpcSs.SimpleMistakeToGeneral(MistakeTypes.MinSpeedWind, sender);
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
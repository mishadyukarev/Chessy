using Chessy.Game.Model.Entity;
using Chessy.Game.Values.Cell.Unit;
using Chessy.Game.Values.Cell.Unit.Effect;
using Chessy.Game.Values.Cell.Unit.Stats;
using Photon.Pun;
using Photon.Realtime;

namespace Chessy.Game.Model.System
{
    sealed partial class UnitAbilitiesSystems : SystemModel
    {
        internal void Stun(in byte cell_from, in byte cell_to, in AbilityTypes abilityT, in Player sender)
        {
            if (!eMG.UnitCooldownAbilitiesC(cell_from).HaveCooldown(abilityT))
            {
                if (eMG.AdultForestC(cell_to).HaveAnyResources)
                {
                    if (eMG.StepUnitC(cell_from).Steps >= StepValues.STUN_ELFEMALE)
                    {
                        if (!eMG.UnitPlayerTC(cell_from).Is(eMG.UnitPlayerTC(cell_to).PlayerT))
                        {
                            eMG.StunUnitC(cell_to).Stun = StunValues.ELFEMALE;
                            eMG.UnitCooldownAbilitiesC(cell_from).Set(abilityT, AbilityCooldownValues.NeedAfterAbility(abilityT));

                            eMG.StepUnitC(cell_from).Steps -= StepValues.STUN_ELFEMALE;

                            eMG.RpcPoolEs.SoundToGeneral(RpcTarget.All, abilityT);


                            foreach (var idx_1 in eMG.AroundCellsE(cell_to).CellsAround)
                            {
                                if (eMG.AdultForestC(idx_1).HaveAnyResources)
                                {
                                    if (eMG.UnitTC(idx_1).HaveUnit && eMG.UnitPlayerTC(idx_1).Is(eMG.UnitPlayerTC(cell_to).PlayerT))
                                    {
                                        eMG.StunUnitC(idx_1).Stun = StunValues.ELFEMALE;
                                    }
                                }
                            }
                        }
                    }

                    else eMG.RpcPoolEs.SimpleMistake_ToGeneral(MistakeTypes.NeedMoreSteps, sender);
                }
            }

            else eMG.RpcPoolEs.SoundToGeneral(sender, ClipTypes.Mistake);
        }
    }
}
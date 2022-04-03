using Chessy.Common.Entity;
using Chessy.Common.Model.System;
using Chessy.Game.Model.Entity;
using Chessy.Game.Values.Cell.Unit;
using Chessy.Game.Values.Cell.Unit.Effect;
using Chessy.Game.Values.Cell.Unit.Stats;
using Photon.Pun;
using Photon.Realtime;

namespace Chessy.Game.Model.System
{
    sealed class StunElfemaleS_M : SystemModelGameAbs
    {
        internal StunElfemaleS_M(in SystemsModelCommon sMC, in EntitiesModelCommon eMC, in SystemsModelGame sMG, in EntitiesModelGame eMG) : base(sMC, eMC, sMG, eMG) { }

        internal void Stun(in byte cell_from, in byte cell_to, in AbilityTypes abilityT, in Player sender)
        {
            if (!eMG.UnitAbilityE(cell_from).HaveCooldown(abilityT))
            {
                if (eMG.AdultForestC(cell_to).HaveAnyResources)
                {
                    if (eMG.UnitStepC(cell_from).Steps >= StepValues.STUN_ELFEMALE)
                    {
                        if (!eMG.UnitPlayerTC(cell_from).Is(eMG.UnitPlayerTC(cell_to).PlayerT))
                        {
                            eMG.UnitEffectStunC(cell_to).Stun = StunValues.ELFEMALE;
                            eMG.UnitAbilityE(cell_from).Cooldown(abilityT) = AbilityCooldownValues.NeedAfterAbility(abilityT);

                            eMG.UnitStepC(cell_from).Steps -= StepValues.STUN_ELFEMALE;

                            eMG.RpcPoolEs.SoundToGeneral(RpcTarget.All, abilityT);


                            foreach (var idx_1 in eMG.AroundCellsE(cell_to).CellsAround)
                            {
                                if (eMG.AdultForestC(idx_1).HaveAnyResources)
                                {
                                    if (eMG.UnitTC(idx_1).HaveUnit && eMG.UnitPlayerTC(idx_1).Is(eMG.UnitPlayerTC(cell_to).PlayerT))
                                    {
                                        eMG.UnitEffectStunC(idx_1).Stun = StunValues.ELFEMALE;
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
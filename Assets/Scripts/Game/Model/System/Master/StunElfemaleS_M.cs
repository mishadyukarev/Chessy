using Chessy.Game.Entity.Model;
using Chessy.Game.Values.Cell.Unit;
using Chessy.Game.Values.Cell.Unit.Effect;
using Chessy.Game.Values.Cell.Unit.Stats;
using Photon.Pun;
using Photon.Realtime;

namespace Chessy.Game.Model.System
{
    public sealed class StunElfemaleS_M : SystemModelGameAbs
    {
        public StunElfemaleS_M(in EntitiesModelGame eMGame) : base(eMGame)
        {
        }

        public void Stun(in byte idx_from, in byte idx_to, in AbilityTypes abilityT, in Player sender)
        {
            if (!eMGame.UnitEs(idx_from).CoolDownC(abilityT).HaveCooldown)
            {
                if (eMGame.AdultForestC(idx_to).HaveAnyResources)
                {
                    if (eMGame.UnitStepC(idx_from).Steps >= StepValues.STUN_ELFEMALE)
                    {
                        if (!eMGame.UnitPlayerTC(idx_from).Is(eMGame.UnitPlayerTC(idx_to).Player))
                        {
                            eMGame.UnitEffectStunC(idx_to).Stun = StunValues.ELFEMALE;
                            eMGame.UnitEs(idx_from).CoolDownC(abilityT).Cooldown = AbilityCooldownValues.NeedAfterAbility(abilityT);

                            eMGame.UnitStepC(idx_from).Steps -= StepValues.STUN_ELFEMALE;

                            eMGame.RpcPoolEs.SoundToGeneral(RpcTarget.All, abilityT);


                            foreach (var idx_1 in eMGame.CellEs(idx_to).IdxsAround)
                            {
                                if (eMGame.AdultForestC(idx_1).HaveAnyResources)
                                {
                                    if (eMGame.UnitTC(idx_1).HaveUnit && eMGame.UnitPlayerTC(idx_1).Is(eMGame.UnitPlayerTC(idx_to).Player))
                                    {
                                        eMGame.UnitEffectStunC(idx_1).Stun = StunValues.ELFEMALE;
                                    }
                                }
                            }
                        }
                    }

                    else eMGame.RpcPoolEs.SimpleMistake_ToGeneral(MistakeTypes.NeedMoreSteps, sender);
                }
            }

            else eMGame.RpcPoolEs.SoundToGeneral(sender, ClipTypes.Mistake);
        }
    }
}
using Chessy.Game.Entity.Model;
using Chessy.Game.System.Model;
using Chessy.Game.Values.Cell.Unit;
using Chessy.Game.Values.Cell.Unit.Effect;
using Chessy.Game.Values.Cell.Unit.Stats;
using Photon.Pun;
using Photon.Realtime;

namespace Chessy.Game.Model.System
{
    sealed class StunElfemaleS_M : SystemModelGameAbs
    {
        internal StunElfemaleS_M(in SystemsModelGame sMGame, in EntitiesModelGame eMGame) : base(sMGame, eMGame) { }

        internal void Stun(in byte cell_from, in byte cell_to, in AbilityTypes abilityT, in Player sender)
        {
            if (!e.UnitEs(cell_from).CoolDownC(abilityT).HaveCooldown)
            {
                if (e.AdultForestC(cell_to).HaveAnyResources)
                {
                    if (e.UnitStepC(cell_from).Steps >= StepValues.STUN_ELFEMALE)
                    {
                        if (!e.UnitPlayerTC(cell_from).Is(e.UnitPlayerTC(cell_to).Player))
                        {
                            e.UnitEffectStunC(cell_to).Stun = StunValues.ELFEMALE;
                            e.UnitEs(cell_from).CoolDownC(abilityT).Cooldown = AbilityCooldownValues.NeedAfterAbility(abilityT);

                            e.UnitStepC(cell_from).Steps -= StepValues.STUN_ELFEMALE;

                            e.RpcPoolEs.SoundToGeneral(RpcTarget.All, abilityT);


                            foreach (var idx_1 in e.CellEs(cell_to).AroundCellsEs.IdxsAround)
                            {
                                if (e.AdultForestC(idx_1).HaveAnyResources)
                                {
                                    if (e.UnitTC(idx_1).HaveUnit && e.UnitPlayerTC(idx_1).Is(e.UnitPlayerTC(cell_to).Player))
                                    {
                                        e.UnitEffectStunC(idx_1).Stun = StunValues.ELFEMALE;
                                    }
                                }
                            }
                        }
                    }

                    else e.RpcPoolEs.SimpleMistake_ToGeneral(MistakeTypes.NeedMoreSteps, sender);
                }
            }

            else e.RpcPoolEs.SoundToGeneral(sender, ClipTypes.Mistake);
        }
    }
}
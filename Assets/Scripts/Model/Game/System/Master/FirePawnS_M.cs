using Chessy.Common.Entity;
using Chessy.Common.Model.System;
using Chessy.Game.Model.Entity;
using Chessy.Game.Values.Cell.Unit.Stats;
using Photon.Pun;
using Photon.Realtime;
using System;

namespace Chessy.Game.Model.System
{
    sealed class FirePawnS_M : SystemModel
    {
        internal FirePawnS_M(in SystemsModelGame sMG, in EntitiesModelGame eMG) : base(sMG, eMG) { }

        internal void Fire(in byte cell_0, in Player sender)
        {
            if (eMG.StepUnitC(cell_0).Steps >= StepValues.FIRE_PAWN)
            {
                if (eMG.AdultForestC(cell_0).HaveAnyResources)
                {
                    eMG.RpcPoolEs.SoundToGeneral(RpcTarget.All, AbilityTypes.FirePawn);

                    eMG.HaveFire(cell_0) = true;
                    eMG.StepUnitC(cell_0).Steps -= StepValues.FIRE_PAWN;
                }
                else
                {
                    eMG.RpcPoolEs.SoundToGeneral(sender, ClipTypes.Mistake);
                }
            }

            else
            {
                eMG.RpcPoolEs.SimpleMistake_ToGeneral(MistakeTypes.NeedMoreSteps, sender);
            }
        }
    }
}
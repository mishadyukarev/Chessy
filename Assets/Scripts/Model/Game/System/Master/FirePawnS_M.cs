using Chessy.Game.Entity.Model;
using Chessy.Game.Model.System;
using Chessy.Game.Values.Cell.Unit.Stats;
using Photon.Pun;
using Photon.Realtime;
using System;
using Chessy.Common.Entity;
using Chessy.Common.Model.System;

namespace Chessy.Game.Model.System
{
    sealed class FirePawnS_M : SystemModelGameAbs
    {
        internal FirePawnS_M(in SystemsModelCommon sMC, in EntitiesModelCommon eMC, in SystemsModelGame sMG, in EntitiesModelGame eMG) : base(sMC, eMC, sMG, eMG) { }

        internal void Fire(in byte cell_0, in Player sender)
        {
            if (eMG.UnitStepC(cell_0).Steps >= StepValues.FIRE_PAWN)
            {
                if (eMG.AdultForestC(cell_0).HaveAnyResources)
                {
                    eMG.RpcPoolEs.SoundToGeneral(RpcTarget.All, AbilityTypes.FirePawn);

                    eMG.HaveFire(cell_0) = true;
                    eMG.UnitStepC(cell_0).Steps -= StepValues.FIRE_PAWN;
                }

                else
                {
                    throw new Exception();
                }
            }

            else
            {
                eMG.RpcPoolEs.SimpleMistake_ToGeneral(MistakeTypes.NeedMoreSteps, sender);
            }
        }
    }
}
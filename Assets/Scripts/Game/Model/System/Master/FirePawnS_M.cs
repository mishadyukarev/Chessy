using Chessy.Game.Entity.Model;
using Chessy.Game.System.Model;
using Chessy.Game.Values.Cell.Unit.Stats;
using Photon.Pun;
using Photon.Realtime;
using System;

namespace Chessy.Game.Model.System
{
    sealed class FirePawnS_M : SystemModelGameAbs
    {
        internal FirePawnS_M(in SystemsModelGame sMGame, in EntitiesModelGame eMGame) : base(sMGame, eMGame) { }

        internal void Fire(in byte cell_0, in Player sender)
        {
            if (e.UnitStepC(cell_0).Steps >= StepValues.FIRE_PAWN)
            {
                if (e.AdultForestC(cell_0).HaveAnyResources)
                {
                    e.RpcPoolEs.SoundToGeneral(RpcTarget.All, AbilityTypes.FirePawn);

                    e.HaveFire(cell_0) = true;
                    e.UnitStepC(cell_0).Steps -= StepValues.FIRE_PAWN;
                }

                else
                {
                    throw new Exception();
                }
            }

            else
            {
                e.RpcPoolEs.SimpleMistake_ToGeneral(MistakeTypes.NeedMoreSteps, sender);
            }
        }
    }
}
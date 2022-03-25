using Chessy.Game.Entity.Model;
using Chessy.Game.Values.Cell.Unit.Stats;
using Photon.Pun;
using Photon.Realtime;
using System;

namespace Chessy.Game.Model.System
{
    public sealed class FirePawnS_M : SystemModelGameAbs
    {
        public FirePawnS_M(in EntitiesModelGame eMGame) : base(eMGame)
        {
        }

        public void Fire(in byte cell_0, in Player sender)
        {
            if (eMGame.UnitStepC(cell_0).Steps >= StepValues.FIRE_PAWN)
            {
                if (eMGame.AdultForestC(cell_0).HaveAnyResources)
                {
                    eMGame.RpcPoolEs.SoundToGeneral(RpcTarget.All, AbilityTypes.FirePawn);

                    eMGame.HaveFire(cell_0) = true;
                    eMGame.UnitStepC(cell_0).Steps -= StepValues.FIRE_PAWN;
                }

                else
                {
                    throw new Exception();
                }
            }

            else
            {
                eMGame.RpcPoolEs.SimpleMistake_ToGeneral(MistakeTypes.NeedMoreSteps, sender);
            }
        }
    }
}
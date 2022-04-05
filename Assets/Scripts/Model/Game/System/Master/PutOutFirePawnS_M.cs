using Chessy.Common.Entity;
using Chessy.Common.Model.System;
using Chessy.Game.Model.Entity;
using Chessy.Game.Values.Cell.Unit.Stats;
using Photon.Realtime;

namespace Chessy.Game.Model.System
{
    sealed class PutOutFirePawnS_M : SystemModelGameAbs
    {
        internal PutOutFirePawnS_M(in SystemsModelCommon sMC, in EntitiesModelCommon eMC, in SystemsModelGame sMG, in EntitiesModelGame eMG) : base(sMC, eMC, sMG, eMG) { }

        public void PutOut(in byte cell_0, in Player sender)
        {
            if (eMG.StepUnitC(cell_0).Steps >= StepValues.PUT_OUT_FIRE_PAWN)
            {
                eMG.HaveFire(cell_0) = false;

                eMG.StepUnitC(cell_0).Steps -= StepValues.PUT_OUT_FIRE_PAWN;
            }

            else
            {
                eMG.RpcPoolEs.SimpleMistake_ToGeneral(MistakeTypes.NeedMoreSteps, sender);
            }
        }
    }
}
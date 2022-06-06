using Chessy.Game.Model.Entity;
using Chessy.Game.Values.Cell.Unit.Stats;
using Photon.Realtime;

namespace Chessy.Game.Model.System
{
    sealed class PutOutFirePawnS_M : SystemModel
    {
        internal PutOutFirePawnS_M(in SystemsModelGame sMG, in EntitiesModelGame eMG) : base(sMG, eMG) { }

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
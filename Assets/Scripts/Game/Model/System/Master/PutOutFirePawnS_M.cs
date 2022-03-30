using Chessy.Game.Entity.Model;
using Chessy.Game.System.Model;
using Chessy.Game.Values.Cell.Unit.Stats;
using Photon.Realtime;

namespace Chessy.Game.Model.System
{
    sealed class PutOutFirePawnS_M : SystemModelGameAbs
    {
        internal PutOutFirePawnS_M(in SystemsModelGame sMGame, in EntitiesModelGame eMGame) : base(sMGame, eMGame) { }

        public void PutOut(in byte cell_0, in Player sender)
        {
            if (e.UnitStepC(cell_0).Steps >= StepValues.PUT_OUT_FIRE_PAWN)
            {
                e.HaveFire(cell_0) = false;

                e.UnitStepC(cell_0).Steps -= StepValues.PUT_OUT_FIRE_PAWN;
            }

            else
            {
                e.RpcPoolEs.SimpleMistake_ToGeneral(MistakeTypes.NeedMoreSteps, sender);
            }
        }
    }
}
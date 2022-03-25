using Chessy.Game.Entity.Model;
using Chessy.Game.Values.Cell.Unit.Stats;
using Photon.Realtime;

namespace Chessy.Game.Model.System
{
    public sealed class PutOutFirePawnS_M : SystemModelGameAbs
    {
        public PutOutFirePawnS_M(in EntitiesModelGame eMGame) : base(eMGame)
        {
        }

        public void PutOut(in byte cell_0, in Player sender)
        {
            if (eMGame.UnitStepC(cell_0).Steps >= StepValues.PUT_OUT_FIRE_PAWN)
            {
                eMGame.HaveFire(cell_0) = false;

                eMGame.UnitStepC(cell_0).Steps -= StepValues.PUT_OUT_FIRE_PAWN;
            }

            else
            {
                eMGame.RpcPoolEs.SimpleMistake_ToGeneral(MistakeTypes.NeedMoreSteps, sender);
            }
        }
    }
}
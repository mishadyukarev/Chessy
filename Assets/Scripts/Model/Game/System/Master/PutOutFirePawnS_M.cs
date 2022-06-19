using Chessy.Game.Model.Entity;
using Chessy.Game.Values.Cell.Unit.Stats;
using Photon.Realtime;

namespace Chessy.Game.Model.System
{
    sealed partial class UnitAbilitiesSystems : SystemModel
    {
        public void TryPutOutFireWithSimplePawnM(in byte cell_0, in Player sender)
        {
            if (_eMG.StepUnitC(cell_0).Steps >= StepValues.PUT_OUT_FIRE_PAWN)
            {
                _eMG.HaveFire(cell_0) = false;

                _eMG.StepUnitC(cell_0).Steps -= StepValues.PUT_OUT_FIRE_PAWN;
            }

            else
            {
                _eMG.RpcPoolEs.SimpleMistake_ToGeneral(MistakeTypes.NeedMoreSteps, sender);
            }
        }
    }
}
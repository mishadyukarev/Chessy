using Chessy.Model.Values.Cell.Unit.Stats;
using Photon.Realtime;

namespace Chessy.Model.Model.System
{
    sealed partial class UnitAbilitiesSystems : SystemModel
    {
        public void TryPutOutFireForestWithSimplePawnM(in byte cell_0, in Player sender)
        {
            if (_e.StepUnitC(cell_0).Steps >= StepValues.PUT_OUT_FIRE_PAWN)
            {
                _e.HaveFire(cell_0) = false;

                _e.StepUnitC(cell_0).Steps -= StepValues.PUT_OUT_FIRE_PAWN;
            }

            else
            {
                _s.SimpleMistakeToGeneral(MistakeTypes.NeedMoreSteps, sender);
            }
        }
    }
}
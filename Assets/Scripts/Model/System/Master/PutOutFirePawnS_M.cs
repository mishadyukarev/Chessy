using Chessy.Model.Values;
using Photon.Realtime;
namespace Chessy.Model.System
{
    sealed partial class UnitAbilitiesSystems : SystemModel
    {
        public void TryPutOutFireForestWithSimplePawnM(in byte cell_0, in Player sender)
        {
            if (_e.EnergyUnitC(cell_0).Energy >= StepValues.PUT_OUT_FIRE_PAWN)
            {
                _e.HaveFire(cell_0) = false;

                _e.EnergyUnitC(cell_0).Energy -= StepValues.PUT_OUT_FIRE_PAWN;
            }

            else
            {
                _s.SimpleMistakeToGeneral(MistakeTypes.NeedMoreSteps, sender);
            }
        }
    }
}
using Chessy.Model.Values;
using Photon.Realtime;
namespace Chessy.Model.System
{
    sealed partial class UnitAbilitiesSystems : SystemModel
    {
        internal void TryChangeCornerArcher(in byte cell_0, in AbilityTypes abilityT, in Player sender)
        {
            if (_e.EnergyUnitC(cell_0).Energy >= StepValues.Need(abilityT))
            {
                _e.UnitMainC(cell_0).ToggleSide();

                _e.EnergyUnitC(cell_0).Energy -= StepValues.CHANGE_CORNER_ARCHER;

                _s.ExecuteSoundActionToGeneral(sender, ClipTypes.PickArcher);
            }

            else
            {
                _s.SimpleMistakeToGeneral(MistakeTypes.NeedMoreSteps, sender);
            }
        }
    }
}
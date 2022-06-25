using Chessy.Model.Values.Cell.Unit.Stats;
using Photon.Pun;
using Photon.Realtime;

namespace Chessy.Model
{
    sealed partial class UnitSystems
    {
        internal void TryDestroyBuildingWithSimplePawnM(in byte cell_0, in Player sender)
        {
            if (_e.EnergyUnitC(cell_0).HaveAnyEnergy)
            {
                _s.ExecuteSoundActionToGeneral(RpcTarget.All, ClipTypes.Destroy);

                _e.Attack(cell_0, 1f, _e.UnitPlayerT(cell_0));

                _e.EnergyUnitC(cell_0).Energy -= StepValues.DESTROY_BUILDING;
            }

            else
            {
                _s.SimpleMistakeToGeneral(MistakeTypes.NeedMoreSteps, sender);
            }
        }
    }
}
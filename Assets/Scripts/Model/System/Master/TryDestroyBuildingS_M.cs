using Chessy.Model.Values;
using Photon.Pun;
using Photon.Realtime;
namespace Chessy.Model.System
{
    sealed partial class UnitSystems : SystemModelAbstract
    {
        internal void TryDestroyBuildingWithSimplePawnM(in byte cell_0, in Player sender)
        {
            //if (_e.EnergyUnitC(cell_0).HaveAnyEnergy)
            //{
                _s.RpcSs.ExecuteSoundActionToGeneral(RpcTarget.All, ClipTypes.Destroy);

                _e.Attack(cell_0, 1f, _e.UnitPlayerT(cell_0));

                //_e.EnergyUnitC(cell_0).Energy -= StepValues.DESTROY_BUILDING;
            //}

            //else
            //{
            //    _s.RpcSs.SimpleMistakeToGeneral(MistakeTypes.NeedMoreSteps, sender);
            //}
        }
    }
}
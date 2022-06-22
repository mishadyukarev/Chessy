using Chessy.Game.Values.Cell.Unit.Stats;
using Photon.Pun;
using Photon.Realtime;

namespace Chessy.Game.Model.System
{
    sealed partial class UnitSystems
    {
        internal void TryDestroyBuildingWithSimplePawnM(in byte cell_0, in Player sender)
        {
            if (_e.StepUnitC(cell_0).HaveAnySteps)
            {
                _s.ExecuteSoundActionToGeneral(RpcTarget.All, ClipTypes.Destroy);

                _e.Attack(cell_0, 1f, _e.UnitPlayerT(cell_0));

                _e.StepUnitC(cell_0).Steps -= StepValues.DESTROY_BUILDING;
            }

            else
            {
                _s.SimpleMistakeToGeneral(MistakeTypes.NeedMoreSteps, sender);
            }
        }
    }
}
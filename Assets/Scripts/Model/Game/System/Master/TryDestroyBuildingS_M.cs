using Chessy.Game.Model.Entity;
using Chessy.Game.Values.Cell.Unit.Stats;
using Photon.Pun;
using Photon.Realtime;

namespace Chessy.Game
{
    sealed partial class BuildingSystems
    {
        internal void TryDestroyBuildingWithSimplePawnM(in byte cell_0, in Player sender)
        {
            if (_eMG.StepUnitC(cell_0).HaveAnySteps)
            {
                _sMG.ExecuteSoundActionToGeneral(RpcTarget.All, ClipTypes.Destroy);

                _sMG.BuildingSs.Attack(cell_0, 1f, _eMG.UnitPlayerTC(cell_0).PlayerT);

                _eMG.StepUnitC(cell_0).Steps -= StepValues.DESTROY_BUILDING;
            }

            else
            {
                _eMG.RpcPoolEs.SimpleMistake_ToGeneral(MistakeTypes.NeedMoreSteps, sender);
            }
        }
    }
}
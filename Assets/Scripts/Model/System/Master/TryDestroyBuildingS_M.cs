using Photon.Pun;
using Photon.Realtime;

namespace Chessy.Model.System
{
    sealed partial class UnitSystems : SystemModelAbstract
    {
        internal void TryDestroyBuildingWithSimplePawnM(in byte cell_0, in Player sender)
        {
            _s.RpcSs.ExecuteSoundActionToGeneral(RpcTarget.All, ClipTypes.Destroy);

            _e.Attack(cell_0, 1f, _unitCs[cell_0].PlayerT);
        }
    }
}
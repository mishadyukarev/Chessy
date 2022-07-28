using Photon.Pun;
using Photon.Realtime;

namespace Chessy.Model.System
{
    sealed partial class UnitSystems : SystemModelAbstract
    {
        internal void TryDestroyBuildingWithSimplePawnM(in byte cellIdx_0, in Player sender)
        {
            _s.RpcSs.ExecuteSoundActionToGeneral(RpcTarget.All, ClipTypes.Destroy);

            BuildingC(cellIdx_0).Dispose();
        }
    }
}
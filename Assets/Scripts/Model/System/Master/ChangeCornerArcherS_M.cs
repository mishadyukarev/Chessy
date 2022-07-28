using Photon.Realtime;
namespace Chessy.Model.System
{
    sealed partial class UnitAbilitiesSystems : SystemModelAbstract
    {
        internal void TryChangeCornerArcher(in byte cell_0, in AbilityTypes abilityT, in Player sender)
        {
            _unitCs[cell_0].ToggleSide();

            _s.RpcSs.ExecuteSoundActionToGeneral(sender, ClipTypes.PickArcher);
        }
    }
}
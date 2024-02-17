using Chessy.Model.Entity;
using Photon.Realtime;
namespace Chessy.Model.System
{
    sealed class ChangeCornerArcherS : SystemModelAbstract
    {
        public ChangeCornerArcherS(in SystemsModel sM, EntitiesModel eM) : base(sM, eM)
        {
        }

        internal void TryChangeCornerArcher(in byte cell_0, in AbilityTypes abilityT, in Player sender)
        {
            unitCs[cell_0].ToggleSide();

            s.RpcSs.ExecuteSoundActionToGeneral(sender, ClipTypes.PickArcher);
        }
    }
}
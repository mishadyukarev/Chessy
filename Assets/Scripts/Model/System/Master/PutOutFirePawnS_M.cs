using Photon.Realtime;
namespace Chessy.Model.System
{
    sealed partial class UnitAbilitiesSystems : SystemModelAbstract
    {
        public void TryPutOutFireForestWithSimplePawnM(in byte cell_0, in Player sender)
        {
            FireC(cell_0).HaveFire = false;
        }
    }
}
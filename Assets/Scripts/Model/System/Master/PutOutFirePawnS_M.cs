using Chessy.Model.Values;
using Photon.Realtime;
namespace Chessy.Model.System
{
    sealed partial class UnitAbilitiesSystems : SystemModelAbstract
    {
        public void TryPutOutFireForestWithSimplePawnM(in byte cell_0, in Player sender)
        {
            _fireCs[cell_0].HaveFire = false;
        }
    }
}
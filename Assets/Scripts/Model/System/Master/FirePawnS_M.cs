using Photon.Pun;
using Photon.Realtime;
namespace Chessy.Model.System
{
    sealed partial class UnitAbilitiesSystems : SystemModelAbstract
    {
        internal void TryFireForestWithSimplePawnM(in byte cellIdxForFire, in Player sender)
        {
            if (_environmentCs[cellIdxForFire].HaveEnvironment(EnvironmentTypes.AdultForest))
            {
                _s.RpcSs.SoundToGeneral(RpcTarget.All, AbilityTypes.FirePawn);

                _fireCs[cellIdxForFire].HaveFire = true;
            }
            else
            {
                _s.RpcSs.ExecuteSoundActionToGeneral(sender, ClipTypes.Mistake);
            }
        }
    }
}
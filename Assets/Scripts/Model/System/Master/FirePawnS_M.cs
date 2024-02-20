using Photon.Pun;
using Photon.Realtime;
namespace Chessy.Model.System
{
    sealed partial class UnitAbilitiesSystems : SystemModelAbstract
    {
        internal void TryFireForestWithSimplePawnM(in byte cellIdxForFire, in Player sender)
        {
            if (environmentCs[cellIdxForFire].HaveEnvironment(EnvironmentTypes.AdultForest))
            {
                s.RpcSs.SoundToGeneral(RpcTarget.All, AbilityTypes.FirePawn);

                fireCs[cellIdxForFire].HaveFire = true;
            }
            else
            {
                s.RpcSs.ExecuteSoundActionToGeneral(sender, ClipTypes.Mistake);
            }
        }
    }
}
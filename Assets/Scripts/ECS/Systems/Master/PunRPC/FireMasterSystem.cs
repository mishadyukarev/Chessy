using Assets.Scripts.Abstractions.Enums;
using Assets.Scripts.Workers.Game.Else.Fire;
using Assets.Scripts.Workers.Info;
using Photon.Pun;

namespace Assets.Scripts.ECS.Game.Master.Systems.PunRPC
{
    internal sealed class FireMasterSystem : RPCMasterSystemReduction
    {
        private PhotonMessageInfo InfoFrom => _eMM.FromInfoEnt_FromInfoCom.FromInfo;
        private int[] FromXyCopy => _eMM.FireEnt_FromToXyCom.FromXy;
        private int[] ToXy => _eMM.FireEnt_FromToXyCom.ToXy;

        public override void Run()
        {
            base.Run();

            if (CellUnitsDataWorker.HaveMinAmountSteps(FromXyCopy))
            {
                if (CellFireDataWorker.HaveFire(ToXy))
                {
                    CellFireDataWorker.ResetFire(ToXy);
                    CellFireDataWorker.ResetTimeSteps(ToXy);

                    CellUnitsDataWorker.TakeAmountSteps(ToXy);
                }
                else if (CellEnvirDataWorker.HaveEnvironment(EnvironmentTypes.AdultForest, ToXy))
                {
                    if (CellUnitsDataWorker.HaveOwner(FromXyCopy))

                        if (InfoResourcesDataWorker.CanFireSomething(CellUnitsDataWorker.Owner(FromXyCopy), CellUnitsDataWorker.UnitType(FromXyCopy), out bool[] haves))
                        {
                            PhotonPunRPC.SoundToGeneral(RpcTarget.All, SoundEffectTypes.Fire);

                            InfoResourcesDataWorker.BuyFire(CellUnitsDataWorker.Owner(FromXyCopy), CellUnitsDataWorker.UnitType(FromXyCopy));

                            CellFireDataWorker.EnableFire(ToXy);
                            CellUnitsDataWorker.TakeAmountSteps(ToXy);
                        }
                        else
                        {
                            PhotonPunRPC.SoundToGeneral(InfoFrom.Sender, SoundEffectTypes.Mistake);
                            PhotonPunRPC.MistakeEconomyToGeneral(InfoFrom.Sender, haves);
                        }
                }
                else
                {
                    PhotonPunRPC.SoundToGeneral(InfoFrom.Sender, SoundEffectTypes.Mistake);
                }
            }
            else
            {
                PhotonPunRPC.SoundToGeneral(InfoFrom.Sender, SoundEffectTypes.Mistake);
            }
        }
    }
}

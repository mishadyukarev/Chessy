using Assets.Scripts.Abstractions.Enums;
using Assets.Scripts.Workers.Game.Else.Fire;
using Assets.Scripts.Workers.Info;
using Photon.Pun;

namespace Assets.Scripts.ECS.Game.Master.Systems.PunRPC
{
    internal sealed class FireMasterSystem : RPCMasterSystemReduction
    {
        private PhotonMessageInfo InfoFrom => _eMM.FromInfoEnt_FromInfoCom.InfoFrom;
        private int[] FromXyCopy => _eMM.FireEnt_FromToXyCom.FromXy;
        private int[] ToXyCopy => _eMM.FireEnt_FromToXyCom.ToXy;

        public override void Run()
        {
            base.Run();

            if (CellUnitWorker.HaveMinAmountSteps(FromXyCopy))
            {


                if (CellFireWorker.HaveEffect(EffectTypes.Fire, ToXyCopy))
                {
                    CellFireWorker.ResetEffect(EffectTypes.Fire, ToXyCopy);
                    CellUnitWorker.TakeAmountSteps(ToXyCopy);
                }
                else if (CellEnvironmentWorker.HaveEnvironment(EnvironmentTypes.AdultForest, ToXyCopy))
                {
                    if (CellUnitWorker.HaveOwner(FromXyCopy))

                        if (InfoResourcesWorker.CanFireSomething(CellUnitWorker.Owner(FromXyCopy), CellUnitWorker.UnitType(FromXyCopy), out bool[] haves))
                        {
                            PhotonPunRPC.SoundToGeneral(RpcTarget.All, SoundEffectTypes.Fire);

                            InfoResourcesWorker.Fire(CellUnitWorker.Owner(FromXyCopy), CellUnitWorker.UnitType(FromXyCopy));

                            CellFireWorker.SetEffect(EffectTypes.Fire, ToXyCopy);
                            CellUnitWorker.TakeAmountSteps(ToXyCopy);
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

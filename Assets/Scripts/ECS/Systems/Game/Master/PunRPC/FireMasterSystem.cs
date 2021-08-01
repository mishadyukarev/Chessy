using Assets.Scripts.Abstractions.Enums;
using Assets.Scripts.Workers;
using Assets.Scripts.Workers.Cell;
using Assets.Scripts.Workers.Game.Else.Fire;
using Assets.Scripts.Workers.Info;
using Photon.Pun;
using System;

namespace Assets.Scripts.ECS.Game.Master.Systems.PunRPC
{
    internal sealed class FireMasterSystem : SystemMasterReduction
    {
        private int[] FromXy => _eMM.FireEnt_FromToXyCom.FromXy;
        private int[] ToXy => _eMM.FireEnt_FromToXyCom.ToXy;

        public override void Run()
        {
            base.Run();

            if (CellUnitsDataWorker.IsMelee(FromXy))
            {
                if (CellUnitsDataWorker.HaveMinAmountSteps(FromXy))
                {
                    if (CellFireDataWorker.HaveFire(ToXy))
                    {
                        CellFireDataWorker.ResetFire(ToXy);
                        CellFireDataWorker.ResetTimeSteps(ToXy);

                        CellUnitsDataWorker.TakeAmountSteps(ToXy);
                    }
                    else if (CellEnvirDataWorker.HaveEnvironment(EnvironmentTypes.AdultForest, ToXy))
                    {
                        if (CellUnitsDataWorker.HaveOwner(FromXy))
                        {
                            PhotonPunRPC.SoundToGeneral(RpcTarget.All, SoundEffectTypes.Fire);

                            CellFireDataWorker.EnableFire(ToXy);
                            CellUnitsDataWorker.TakeAmountSteps(ToXy);
                        }

                    }
                    else
                    {
                        throw new Exception();
                    }
                }

                else
                {
                    PhotonPunRPC.MistakeStepsUnitToGeneral(RpcWorker.InfoFrom.Sender);
                    PhotonPunRPC.SoundToGeneral(RpcWorker.InfoFrom.Sender, SoundEffectTypes.Mistake);
                }
            }

            else
            {
                if (CellUnitsDataWorker.HaveMaxAmountSteps(FromXy))
                {
                    if (!CellFireDataWorker.HaveFire(ToXy))
                    {
                        foreach (var xy1 in CellSpaceWorker.TryGetXyAround(FromXy))
                        {
                            if (CellEnvirDataWorker.HaveEnvironment(EnvironmentTypes.AdultForest, xy1))
                            {
                                if (xy1.Compare(ToXy))
                                {
                                    CellUnitsDataWorker.ResetAmountSteps(FromXy);
                                    CellFireDataWorker.EnableFire(ToXy);
                                }
                            }
                        }
                    }
                }

                else
                {
                    PhotonPunRPC.MistakeStepsUnitToGeneral(RpcWorker.InfoFrom.Sender);
                    PhotonPunRPC.SoundToGeneral(RpcWorker.InfoFrom.Sender, SoundEffectTypes.Mistake);
                }
            }
        }
    }
}

using Assets.Scripts.Abstractions.Enums;
using Assets.Scripts.Workers;
using Assets.Scripts.Workers.Cell;
using Assets.Scripts.Workers.Game.Else.Fire;
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

            if (CellUnitsDataContainer.IsMelee(FromXy))
            {
                if (CellUnitsDataContainer.HaveMinAmountSteps(FromXy))
                {
                    if (CellFireDataContainer.HaveFire(ToXy))
                    {
                        CellFireDataContainer.ResetFire(ToXy);

                        CellUnitsDataContainer.TakeAmountSteps(ToXy);
                    }
                    else if (CellEnvirDataContainer.HaveEnvironment(EnvironmentTypes.AdultForest, ToXy))
                    {
                        if (CellUnitsDataContainer.HaveOwner(FromXy))
                        {
                            PhotonPunRPC.SoundToGeneral(RpcTarget.All, SoundEffectTypes.Fire);

                            CellFireDataContainer.EnableFire(ToXy);
                            CellUnitsDataContainer.TakeAmountSteps(ToXy);
                        }

                    }
                    else
                    {
                        throw new Exception();
                    }
                }

                else
                {
                    PhotonPunRPC.MistakeStepsUnitToGeneral(RpcMasterDataContainer.InfoFrom.Sender);
                    PhotonPunRPC.SoundToGeneral(RpcMasterDataContainer.InfoFrom.Sender, SoundEffectTypes.Mistake);
                }
            }

            else
            {
                if (CellUnitsDataContainer.HaveMaxAmountSteps(FromXy))
                {
                    if (!CellFireDataContainer.HaveFire(ToXy))
                    {
                        foreach (var xy1 in CellSpaceWorker.TryGetXyAround(FromXy))
                        {
                            if (CellEnvirDataContainer.HaveEnvironment(EnvironmentTypes.AdultForest, xy1))
                            {
                                if (xy1.Compare(ToXy))
                                {
                                    CellUnitsDataContainer.ResetAmountSteps(FromXy);
                                    CellFireDataContainer.EnableFire(ToXy);
                                }
                            }
                        }
                    }
                }

                else
                {
                    PhotonPunRPC.MistakeStepsUnitToGeneral(RpcMasterDataContainer.InfoFrom.Sender);
                    PhotonPunRPC.SoundToGeneral(RpcMasterDataContainer.InfoFrom.Sender, SoundEffectTypes.Mistake);
                }
            }
        }
    }
}

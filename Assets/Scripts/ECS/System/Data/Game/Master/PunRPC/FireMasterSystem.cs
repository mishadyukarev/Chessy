using Assets.Scripts.Abstractions.Enums;
using Assets.Scripts.ECS.System.Data.Game.General.Cell;
using Assets.Scripts.Workers;
using Assets.Scripts.Workers.Cell;
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

            if (CellUnitsDataSystem.IsMelee(FromXy))
            {
                if (CellUnitsDataSystem.HaveMinAmountSteps(FromXy))
                {
                    if (CellFireDataSystem.HaveFireCom(ToXy).HaveFire)
                    {
                        CellFireDataSystem.HaveFireCom(ToXy).Disable();

                        CellUnitsDataSystem.TakeAmountSteps(ToXy);
                    }
                    else if (CellEnvrDataSystem.HaveEnvironment(EnvironmentTypes.AdultForest, ToXy))
                    {
                        if (CellUnitsDataSystem.HaveOwner(FromXy))
                        {
                            PhotonPunRPC.SoundToGeneral(RpcTarget.All, SoundEffectTypes.Fire);

                            CellFireDataSystem.HaveFireCom(ToXy).Enable();
                            CellUnitsDataSystem.TakeAmountSteps(ToXy);
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
                if (CellUnitsDataSystem.HaveMaxAmountSteps(FromXy))
                {
                    if (!CellFireDataSystem.HaveFireCom(ToXy).HaveFire)
                    {
                        foreach (var xy1 in CellSpaceWorker.TryGetXyAround(FromXy))
                        {
                            if (CellEnvrDataSystem.HaveEnvironment(EnvironmentTypes.AdultForest, xy1))
                            {
                                if (xy1.Compare(ToXy))
                                {
                                    CellUnitsDataSystem.ResetAmountSteps(FromXy);
                                    CellFireDataSystem.HaveFireCom(ToXy).Enable();
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

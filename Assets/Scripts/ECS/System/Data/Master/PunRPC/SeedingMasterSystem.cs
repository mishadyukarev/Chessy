using Assets.Scripts.Abstractions.Enums;
using Assets.Scripts.Workers;
using System;
using static Assets.Scripts.CellEnvirDataContainer;

namespace Assets.Scripts.ECS.Game.Master.Systems.PunRPC
{
    internal sealed class SeedingMasterSystem : SystemMasterReduction
    {
        private int[] XyCellForSeeding => _eMM.SeedingEnt_XyCellCom.XyCell;
        private EnvironmentTypes EnvTypeForSeeding => _eMM.SeedingEnt_EnvironmentTypesCom.EnvironmentType;

        public override void Run()
        {
            base.Run();


            switch (EnvTypeForSeeding)
            {
                case EnvironmentTypes.None:
                    throw new Exception();

                case EnvironmentTypes.Fertilizer:
                    throw new Exception();

                case EnvironmentTypes.YoungForest:
                    if (CellUnitsDataContainer.HaveMinAmountSteps(XyCellForSeeding))
                    {
                        if (CellBuildDataContainer.HaveAnyBuilding(XyCellForSeeding))
                        {
                            PhotonPunRPC.MistakeNeedOthePlaceToGeneral(RpcMasterDataContainer.InfoFrom.Sender);
                            PhotonPunRPC.SoundToGeneral(RpcMasterDataContainer.InfoFrom.Sender, SoundEffectTypes.Mistake);
                        }
                        else
                        {
                            if (!HaveEnvironment(EnvironmentTypes.Fertilizer, XyCellForSeeding))
                            {
                                if (!HaveEnvironment(EnvironmentTypes.AdultForest, XyCellForSeeding))

                                    if (!HaveEnvironment(EnvironmentTypes.YoungForest, XyCellForSeeding))
                                    {
                                        PhotonPunRPC.SoundToGeneral(RpcMasterDataContainer.InfoFrom.Sender, SoundEffectTypes.Seeding);
                                        SetNewEnvironment(EnvironmentTypes.YoungForest, XyCellForSeeding);

                                        CellUnitsDataContainer.TakeAmountSteps(XyCellForSeeding);
                                    }
                                    else
                                    {
                                        PhotonPunRPC.MistakeNeedOthePlaceToGeneral(RpcMasterDataContainer.InfoFrom.Sender);
                                        PhotonPunRPC.SoundToGeneral(RpcMasterDataContainer.InfoFrom.Sender, SoundEffectTypes.Mistake);
                                    }
                                else
                                {
                                    PhotonPunRPC.MistakeNeedOthePlaceToGeneral(RpcMasterDataContainer.InfoFrom.Sender);
                                    PhotonPunRPC.SoundToGeneral(RpcMasterDataContainer.InfoFrom.Sender, SoundEffectTypes.Mistake);
                                }
                            }
                            else
                            {
                                PhotonPunRPC.MistakeNeedOthePlaceToGeneral(RpcMasterDataContainer.InfoFrom.Sender);
                                PhotonPunRPC.SoundToGeneral(RpcMasterDataContainer.InfoFrom.Sender, SoundEffectTypes.Mistake);
                            }

                        }
                    }

                    else
                    {
                        PhotonPunRPC.MistakeStepsUnitToGeneral(RpcMasterDataContainer.InfoFrom.Sender);
                        PhotonPunRPC.SoundToGeneral(RpcMasterDataContainer.InfoFrom.Sender, SoundEffectTypes.Mistake);
                    }
                    break;

                case EnvironmentTypes.AdultForest:
                    throw new Exception();

                case EnvironmentTypes.Hill:
                    throw new Exception();

                case EnvironmentTypes.Mountain:
                    throw new Exception();

                default:
                    throw new Exception();
            }
        }
    }
}

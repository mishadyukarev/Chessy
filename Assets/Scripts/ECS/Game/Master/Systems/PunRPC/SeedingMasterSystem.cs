﻿using Assets.Scripts.Abstractions.Enums;
using Photon.Pun;
using System;
using static Assets.Scripts.CellEnvironmentWorker;

namespace Assets.Scripts.ECS.Game.Master.Systems.PunRPC
{
    internal sealed class SeedingMasterSystem : RPCMasterSystemReduction
    {
        private PhotonMessageInfo InfoFrom => _eMM.FromInfoEnt_FromInfoCom.InfoFrom;

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
                    if (_eGM.CellUnitEnt_CellUnitCom(XyCellForSeeding).HaveMinAmountSteps)
                    {
                        if (!_eGM.CellBuildEnt_BuilTypeCom(XyCellForSeeding).HaveBuilding)
                        {
                            if (!HaveEnvironment(EnvironmentTypes.Fertilizer, XyCellForSeeding))
                            {
                                if (!HaveEnvironment(EnvironmentTypes.AdultForest, XyCellForSeeding))

                                    if (!HaveEnvironment(EnvironmentTypes.YoungForest, XyCellForSeeding))
                                    {
                                        _photonPunRPC.SoundToGeneral(InfoFrom.Sender, SoundEffectTypes.Seeding);
                                        SetNewEnvironment(EnvironmentTypes.YoungForest, XyCellForSeeding);

                                        _eGM.CellUnitEnt_CellUnitCom(XyCellForSeeding).TakeAmountSteps();
                                    }
                            }
                            else
                            {
                                _photonPunRPC.SoundToGeneral(InfoFrom.Sender, SoundEffectTypes.Mistake);
                            }
                        }
                        else
                        {
                            _photonPunRPC.SoundToGeneral(InfoFrom.Sender, SoundEffectTypes.Mistake);
                        }
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

using Assets.Scripts.Abstractions.Enums;
using Assets.Scripts.ECS.Component.Data.Else.Game.General.Cell;
using Assets.Scripts.ECS.Component.Game.Master;
using Leopotam.Ecs;
using System;

namespace Assets.Scripts.ECS.Game.Master.Systems.PunRPC
{
    internal sealed class SeedingMasterSystem : IEcsRunSystem
    {
        private EcsFilter<InfoMasCom> _infoFilter = default;
        private EcsFilter<ForSeedingMasCom> _seedingFilter = default;

        private EcsFilter<CellUnitDataComponent> _cellUnitFilter = default;
        private EcsFilter<CellBuildDataComponent> _cellBuildFilter = default;
        private EcsFilter<CellEnvironDataCom> _cellEnvFilter = default;

        public void Run()
        {
            var sender = _infoFilter.Get1(0).FromInfo.Sender;
            var envTypeForSeeding = _seedingFilter.Get1(0).EnvTypeForSeeding;
            var idxCellForSeeding = _seedingFilter.Get1(0).IdxForSeeding;

            ref var curCellUnitDataCom = ref _cellUnitFilter.Get1(idxCellForSeeding);
            ref var curCellBuildDataCom = ref _cellBuildFilter.Get1(idxCellForSeeding);
            ref var curCellEnvDataCom = ref _cellEnvFilter.Get1(idxCellForSeeding);


            switch (envTypeForSeeding)
            {
                case EnvironmentTypes.None:
                    throw new Exception();

                case EnvironmentTypes.Fertilizer:
                    throw new Exception();

                case EnvironmentTypes.YoungForest:
                    if (curCellUnitDataCom.HaveMinAmountSteps)
                    {
                        if (curCellBuildDataCom.HaveBuild)
                        {
                            RPCGameSystem.MistakeNeedOthePlaceToGeneral(sender);
                            RPCGameSystem.SoundToGeneral(sender, SoundEffectTypes.Mistake);
                        }
                        else
                        {
                            if (!curCellEnvDataCom.HaveEnvironment(EnvironmentTypes.Fertilizer))
                            {
                                if (!curCellEnvDataCom.HaveEnvironment(EnvironmentTypes.AdultForest))

                                    if (!curCellEnvDataCom.HaveEnvironment(EnvironmentTypes.YoungForest))
                                    {
                                        RPCGameSystem.SoundToGeneral(sender, SoundEffectTypes.Seeding);
                                        curCellEnvDataCom.SetNewEnvironment(EnvironmentTypes.YoungForest);

                                        curCellUnitDataCom.TakeAmountSteps();
                                    }
                                    else
                                    {
                                        RPCGameSystem.MistakeNeedOthePlaceToGeneral(sender);
                                        RPCGameSystem.SoundToGeneral(sender, SoundEffectTypes.Mistake);
                                    }
                                else
                                {
                                    RPCGameSystem.MistakeNeedOthePlaceToGeneral(sender);
                                    RPCGameSystem.SoundToGeneral(sender, SoundEffectTypes.Mistake);
                                }
                            }
                            else
                            {
                                RPCGameSystem.MistakeNeedOthePlaceToGeneral(sender);
                                RPCGameSystem.SoundToGeneral(sender, SoundEffectTypes.Mistake);
                            }

                        }
                    }

                    else
                    {
                        RPCGameSystem.MistakeStepsUnitToGeneral(sender);
                        RPCGameSystem.SoundToGeneral(sender, SoundEffectTypes.Mistake);
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

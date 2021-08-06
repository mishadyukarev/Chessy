using Assets.Scripts.Abstractions.Enums;
using Assets.Scripts.ECS.Component.Game.Master;
using Assets.Scripts.ECS.System.Data.Game.General.Cell;
using Leopotam.Ecs;
using System;

namespace Assets.Scripts.ECS.Game.Master.Systems.PunRPC
{
    internal sealed class SeedingMasterSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsWorld _currentGameWorld;
        private EcsFilter<InfoMasCom> _infoFilter;
        private EcsFilter<ForSeedingMasCom, XyCellForDoingMasCom> _seedingFilter;

        public void Init()
        {
            _currentGameWorld.NewEntity()
                .Replace(new ForSeedingMasCom())
                .Replace(new XyCellForDoingMasCom(new int[2]));
        }

        public void Run()
        {
            var sender = _infoFilter.Get1(0).FromInfo.Sender;
            var envTypeForSeeding = _seedingFilter.Get1(0).EnvTypeForSeeding;
            var xyCellForSeeding = _seedingFilter.Get2(0).XyCellForDoing;


            switch (envTypeForSeeding)
            {
                case EnvironmentTypes.None:
                    throw new Exception();

                case EnvironmentTypes.Fertilizer:
                    throw new Exception();

                case EnvironmentTypes.YoungForest:
                    if (CellUnitsDataSystem.HaveMinAmountSteps(xyCellForSeeding))
                    {
                        if (CellBuildDataSystem.BuildTypeCom(xyCellForSeeding).HaveBuild)
                        {
                            RPCGameSystem.MistakeNeedOthePlaceToGeneral(sender);
                            RPCGameSystem.SoundToGeneral(sender, SoundEffectTypes.Mistake);
                        }
                        else
                        {
                            if (!CellEnvrDataSystem.HaveEnvironment(EnvironmentTypes.Fertilizer, xyCellForSeeding))
                            {
                                if (!CellEnvrDataSystem.HaveEnvironment(EnvironmentTypes.AdultForest, xyCellForSeeding))

                                    if (!CellEnvrDataSystem.HaveEnvironment(EnvironmentTypes.YoungForest, xyCellForSeeding))
                                    {
                                        RPCGameSystem.SoundToGeneral(sender, SoundEffectTypes.Seeding);
                                        CellEnvrDataSystem.SetNewEnvironment(EnvironmentTypes.YoungForest, xyCellForSeeding);

                                        CellUnitsDataSystem.TakeAmountSteps(xyCellForSeeding);
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

using Leopotam.Ecs;
using System;

namespace Scripts.Game
{
    public sealed class SeedingMasterSystem : IEcsRunSystem
    {
        private EcsFilter<ForSeedingMasCom> _seedingFilter = default;

        private EcsFilter<CellUnitDataCom, StepComponent> _cellUnitFilter = default;
        private EcsFilter<CellBuildDataC> _cellBuildFilter = default;
        private EcsFilter<CellEnvDataC> _cellEnvFilter = default;

        public void Run()
        {
            var sender = InfoC.Sender(MasGenOthTypes.Master);
            var envTypeForSeeding = _seedingFilter.Get1(0).EnvTypeForSeeding;
            var idxCellForSeeding = _seedingFilter.Get1(0).IdxForSeeding;

            ref var curUnitDatC = ref _cellUnitFilter.Get1(idxCellForSeeding);
            ref var stepUnitC = ref _cellUnitFilter.Get2(idxCellForSeeding);

            ref var curCellBuildDataCom = ref _cellBuildFilter.Get1(idxCellForSeeding);
            ref var curCellEnvDataCom = ref _cellEnvFilter.Get1(idxCellForSeeding);


            switch (envTypeForSeeding)
            {
                case EnvTypes.None:
                    throw new Exception();

                case EnvTypes.Fertilizer:
                    throw new Exception();

                case EnvTypes.YoungForest:
                    if (stepUnitC.HaveMinSteps)
                    {
                        if (curCellBuildDataCom.HaveBuild)
                        {
                            RpcSys.SimpleMistakeToGeneral(MistakeTypes.NeedOtherPlace, sender);
                        }
                        else
                        {
                            if (!curCellEnvDataCom.Have(EnvTypes.Fertilizer))
                            {
                                if (!curCellEnvDataCom.Have(EnvTypes.AdultForest))

                                    if (!curCellEnvDataCom.Have(EnvTypes.YoungForest))
                                    {
                                        RpcSys.SoundToGeneral(sender, SoundEffectTypes.Seeding);

                                        curCellEnvDataCom.SetNew(EnvTypes.YoungForest);
                                        WhereEnvC.Add(EnvTypes.YoungForest, idxCellForSeeding);

                                        stepUnitC.TakeSteps();
                                    }
                                    else
                                    {
                                        RpcSys.SimpleMistakeToGeneral(MistakeTypes.NeedOtherPlace, sender);
                                    }
                                else
                                {
                                    RpcSys.SimpleMistakeToGeneral(MistakeTypes.NeedOtherPlace, sender);
                                }
                            }
                            else
                            {
                                RpcSys.SimpleMistakeToGeneral(MistakeTypes.NeedOtherPlace, sender);
                            }

                        }
                    }

                    else
                    {
                        RpcSys.SimpleMistakeToGeneral(MistakeTypes.NeedMoreSteps, sender);
                    }
                    break;

                case EnvTypes.AdultForest:
                    throw new Exception();

                case EnvTypes.Hill:
                    throw new Exception();

                case EnvTypes.Mountain:
                    throw new Exception();

                default:
                    throw new Exception();
            }
        }
    }
}

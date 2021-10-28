using Leopotam.Ecs;
using System;

namespace Scripts.Game
{
    internal sealed class SeedingMasterSystem : IEcsRunSystem
    {
        private EcsFilter<ForSeedingMasCom> _seedingFilter = default;

        private EcsFilter<CellUnitDataCom> _cellUnitFilter = default;
        private EcsFilter<CellBuildDataCom> _cellBuildFilter = default;
        private EcsFilter<CellEnvironmentDataC> _cellEnvFilter = default;

        public void Run()
        {
            var sender = InfoC.Sender(MasGenOthTypes.Master);
            var envTypeForSeeding = _seedingFilter.Get1(0).EnvTypeForSeeding;
            var idxCellForSeeding = _seedingFilter.Get1(0).IdxForSeeding;

            ref var curCellUnitDataCom = ref _cellUnitFilter.Get1(idxCellForSeeding);
            ref var curCellBuildDataCom = ref _cellBuildFilter.Get1(idxCellForSeeding);
            ref var curCellEnvDataCom = ref _cellEnvFilter.Get1(idxCellForSeeding);


            switch (envTypeForSeeding)
            {
                case EnvirTypes.None:
                    throw new Exception();

                case EnvirTypes.Fertilizer:
                    throw new Exception();

                case EnvirTypes.YoungForest:
                    if (curCellUnitDataCom.HaveMinAmountSteps)
                    {
                        if (curCellBuildDataCom.HaveBuild)
                        {
                            RpcSys.SimpleMistakeToGeneral(MistakeTypes.NeedOtherPlace, sender);
                        }
                        else
                        {
                            if (!curCellEnvDataCom.Have(EnvirTypes.Fertilizer))
                            {
                                if (!curCellEnvDataCom.Have(EnvirTypes.AdultForest))

                                    if (!curCellEnvDataCom.Have(EnvirTypes.YoungForest))
                                    {
                                        RpcSys.SoundToGeneral(sender, SoundEffectTypes.Seeding);

                                        curCellEnvDataCom.SetNew(EnvirTypes.YoungForest);
                                        WhereEnvironmentC.Add(EnvirTypes.YoungForest, idxCellForSeeding);

                                        curCellUnitDataCom.TakeAmountSteps();
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

                case EnvirTypes.AdultForest:
                    throw new Exception();

                case EnvirTypes.Hill:
                    throw new Exception();

                case EnvirTypes.Mountain:
                    throw new Exception();

                default:
                    throw new Exception();
            }
        }
    }
}

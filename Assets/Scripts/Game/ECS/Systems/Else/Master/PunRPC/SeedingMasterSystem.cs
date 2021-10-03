using Leopotam.Ecs;
using System;

namespace Scripts.Game
{
    internal sealed class SeedingMasterSystem : IEcsRunSystem
    {
        private EcsFilter<InfoCom> _infoFilter = default;
        private EcsFilter<ForSeedingMasCom> _seedingFilter = default;

        private EcsFilter<CellUnitDataCom> _cellUnitFilter = default;
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
                            if (!curCellEnvDataCom.HaveEnvir(EnvirTypes.Fertilizer))
                            {
                                if (!curCellEnvDataCom.HaveEnvir(EnvirTypes.AdultForest))

                                    if (!curCellEnvDataCom.HaveEnvir(EnvirTypes.YoungForest))
                                    {
                                        RpcSys.SoundToGeneral(sender, SoundEffectTypes.Seeding);
                                        curCellEnvDataCom.SetNewEnvir(EnvirTypes.YoungForest);

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

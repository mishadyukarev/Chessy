using Leopotam.Ecs;
using Photon.Pun;

namespace Scripts.Game
{
    internal sealed class DestroyMasterSystem : IEcsRunSystem
    {
        private EcsFilter<ForDestroyMasCom> _destroyFilter = default;

        private EcsFilter<CellUnitDataCom, StepComponent, OwnerCom> _cellUnitFilter = default;
        private EcsFilter<CellBuildDataCom, OwnerCom> _cellBuildFilter = default;
        private EcsFilter<CellEnvironmentDataC> _cellEnvFilter = default;

        private EcsFilter<EndGameDataUIC> _endGameFilter = default;

        public void Run()
        {
            var sender = InfoC.Sender(MasGenOthTypes.Master);
            var idxCellForDestory = _destroyFilter.Get1(0).IdxForDestroy;

            ref var curUnitDataCom = ref _cellUnitFilter.Get1(idxCellForDestory);
            ref var curOwnUnitCom = ref _cellUnitFilter.Get3(idxCellForDestory);
            ref var curBuildDataCom = ref _cellBuildFilter.Get1(idxCellForDestory);
            ref var curOwnerBuildCom = ref _cellBuildFilter.Get2(idxCellForDestory);
            ref var curEnvDataCom = ref _cellEnvFilter.Get1(idxCellForDestory);


            if (_cellUnitFilter.Get2(idxCellForDestory).HaveMinSteps)
            {
                RpcSys.SoundToGeneral(RpcTarget.All, SoundEffectTypes.Destroy);

                if (curBuildDataCom.Is(BuildingTypes.City))
                {
                    EndGameDataUIC.PlayerWinner = curOwnUnitCom.PlayerType;
                }
                _cellUnitFilter.Get2(idxCellForDestory).TakeSteps();

                if (curBuildDataCom.Is(BuildingTypes.Farm))
                {
                    curEnvDataCom.Reset(EnvirTypes.Fertilizer);
                    WhereEnvironmentC.Remove(EnvirTypes.Fertilizer, idxCellForDestory);
                }

                curBuildDataCom.Def();
            }
            else
            {
                RpcSys.SimpleMistakeToGeneral(MistakeTypes.NeedMoreSteps, sender);
            }
        }
    }
}
using Leopotam.Ecs;
using Photon.Pun;

namespace Scripts.Game
{
    public sealed class DestroyMasterSystem : IEcsRunSystem
    {
        private EcsFilter<ForDestroyMasCom> _destroyFilter = default;

        private EcsFilter<CellUnitDataCom, StepComponent, OwnerCom> _cellUnitFilter = default;
        private EcsFilter<CellBuildDataC, OwnerCom> _cellBuildFilter = default;
        private EcsFilter<CellEnvDataC> _cellEnvFilter = default;

        private EcsFilter<EndGameDataUIC> _endGameFilter = default;

        public void Run()
        {
            var sender = InfoC.Sender(MasGenOthTypes.Master);
            var idxCellForDestory = _destroyFilter.Get1(0).IdxForDestroy;

            ref var curUnitDataCom = ref _cellUnitFilter.Get1(idxCellForDestory);
            ref var curOwnUnitCom = ref _cellUnitFilter.Get3(idxCellForDestory);
            ref var buildC_0 = ref _cellBuildFilter.Get1(idxCellForDestory);
            ref var ownBuildC_0 = ref _cellBuildFilter.Get2(idxCellForDestory);
            ref var curEnvDataCom = ref _cellEnvFilter.Get1(idxCellForDestory);


            if (_cellUnitFilter.Get2(idxCellForDestory).HaveMinSteps)
            {
                RpcSys.SoundToGeneral(RpcTarget.All, SoundEffectTypes.Destroy);

                if (buildC_0.Is(BuildTypes.City))
                {
                    EndGameDataUIC.PlayerWinner = curOwnUnitCom.Owner;
                }
                _cellUnitFilter.Get2(idxCellForDestory).TakeSteps();

                if (buildC_0.Is(BuildTypes.Farm))
                {
                    curEnvDataCom.Reset(EnvTypes.Fertilizer);
                    WhereEnvC.Remove(EnvTypes.Fertilizer, idxCellForDestory);
                }

                WhereBuildsC.Remove(ownBuildC_0.Owner, buildC_0.BuildType, idxCellForDestory);
                buildC_0.NoneBuild();
            }
            else
            {
                RpcSys.SimpleMistakeToGeneral(MistakeTypes.NeedMoreSteps, sender);
            }
        }
    }
}
using Leopotam.Ecs;
using Photon.Pun;
using Chessy.Common;

namespace Chessy.Game
{
    public sealed class DestroyMasterSystem : IEcsRunSystem
    {
        private EcsFilter<ForDestroyMasCom> _destroyFilter = default;

        private EcsFilter<CellUnitDataC, StepComponent, OwnerCom> _cellUnitFilter = default;
        private EcsFilter<CellBuildDataC, OwnerCom> _cellBuildFilter = default;
        private EcsFilter<CellEnvDataC> _cellEnvFilter = default;

        public void Run()
        {
            var sender = InfoC.Sender(MGOTypes.Master);
            var idxCellForDestory = _destroyFilter.Get1(0).IdxForDestroy;

            ref var curUnitDataCom = ref _cellUnitFilter.Get1(idxCellForDestory);
            ref var curOwnUnitCom = ref _cellUnitFilter.Get3(idxCellForDestory);
            ref var buildC_0 = ref _cellBuildFilter.Get1(idxCellForDestory);
            ref var ownBuildC_0 = ref _cellBuildFilter.Get2(idxCellForDestory);
            ref var curEnvDataCom = ref _cellEnvFilter.Get1(idxCellForDestory);


            if (_cellUnitFilter.Get2(idxCellForDestory).HaveMinSteps)
            {
                RpcSys.SoundToGeneral(RpcTarget.All, ClipGameTypes.Destroy);

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

                WhereBuildsC.Remove(ownBuildC_0.Owner, buildC_0.Build, idxCellForDestory);
                buildC_0.Reset();
            }
            else
            {
                RpcSys.SimpleMistakeToGeneral(MistakeTypes.NeedMoreSteps, sender);
            }
        }
    }
}
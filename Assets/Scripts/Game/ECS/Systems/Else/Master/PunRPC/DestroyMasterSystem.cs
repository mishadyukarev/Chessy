using Leopotam.Ecs;
using Photon.Pun;

namespace Scripts.Game
{
    internal sealed class DestroyMasterSystem : IEcsRunSystem
    {
        private EcsFilter<InfoCom> _infoFilter = default;
        private EcsFilter<ForDestroyMasCom> _destroyFilter = default;

        private EcsFilter<CellUnitDataCom, OwnerCom> _cellUnitFilter = default;
        private EcsFilter<CellBuildDataComponent, OwnerCom> _cellBuildFilter = default;
        private EcsFilter<CellEnvironDataCom> _cellEnvFilter = default;

        private EcsFilter<EndGameDataUIComponent> _endGameFilter = default;

        public void Run()
        {
            var sender = _infoFilter.Get1(0).FromInfo.Sender;
            var idxCellForDestory = _destroyFilter.Get1(0).IdxForDestroy;

            ref var curUnitDataCom = ref _cellUnitFilter.Get1(idxCellForDestory);
            ref var curOwnUnitCom = ref _cellUnitFilter.Get2(idxCellForDestory);
            ref var curBuildDataCom = ref _cellBuildFilter.Get1(idxCellForDestory);
            ref var curOwnerBuildCom = ref _cellBuildFilter.Get2(idxCellForDestory);
            ref var curEnvDataCom = ref _cellEnvFilter.Get1(idxCellForDestory);


            if (curUnitDataCom.HaveMinAmountSteps)
            {
                RpcSys.SoundToGeneral(RpcTarget.All, SoundEffectTypes.Destroy);

                if (curBuildDataCom.IsBuildType(BuildingTypes.City))
                {
                    _endGameFilter.Get1(0).PlayerWinner = curOwnUnitCom.PlayerType;
                }
                curUnitDataCom.TakeAmountSteps();

                if (curBuildDataCom.IsBuildType(BuildingTypes.Farm)) curEnvDataCom.ResetEnvironment(EnvirTypes.Fertilizer);

                curBuildDataCom.DefBuildType();
            }
            else
            {
                RpcSys.SimpleMistakeToGeneral(MistakeTypes.NeedMoreSteps, sender);
            }
        }
    }
}
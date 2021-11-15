using Leopotam.Ecs;
using Photon.Pun;
using Chessy.Common;

namespace Chessy.Game
{
    public sealed class DestroyMasterSystem : IEcsRunSystem
    {
        private EcsFilter<ForDestroyMasCom> _destroyFilter = default;

        private EcsFilter<BuildC, OwnerC> _cellBuildFilter = default;
        private EcsFilter<EnvC> _cellEnvFilter = default;

        private EcsFilter<UnitC, OwnerC> _unitF = default;
        private EcsFilter<StepC> _statUnitF = default;



        public void Run()
        {
            var sender = InfoC.Sender(MGOTypes.Master);
            var idxCellForDestory = _destroyFilter.Get1(0).IdxForDestroy;

            ref var curUnitDataCom = ref _unitF.Get1(idxCellForDestory);
            ref var curOwnUnitCom = ref _unitF.Get2(idxCellForDestory);
            ref var buildC_0 = ref _cellBuildFilter.Get1(idxCellForDestory);
            ref var ownBuildC_0 = ref _cellBuildFilter.Get2(idxCellForDestory);
            ref var curEnvDataCom = ref _cellEnvFilter.Get1(idxCellForDestory);


            if (_statUnitF.Get1(idxCellForDestory).HaveMinSteps)
            {
                RpcSys.SoundToGeneral(RpcTarget.All, ClipTypes.Destroy);

                if (buildC_0.Is(BuildTypes.City))
                {
                    PlyerWinnerC.PlayerWinner = curOwnUnitCom.Owner;
                }
                _statUnitF.Get1(idxCellForDestory).TakeSteps();

                if (buildC_0.Is(BuildTypes.Farm))
                {
                    curEnvDataCom.Remove(EnvTypes.Fertilizer);
                    WhereEnvC.Remove(EnvTypes.Fertilizer, idxCellForDestory);
                }

                WhereBuildsC.Remove(ownBuildC_0.Owner, buildC_0.Build, idxCellForDestory);
                buildC_0.Remove();
            }
            else
            {
                RpcSys.SimpleMistakeToGeneral(MistakeTypes.NeedMoreSteps, sender);
            }
        }
    }
}
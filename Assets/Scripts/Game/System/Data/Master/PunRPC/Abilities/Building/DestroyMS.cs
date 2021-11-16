using Leopotam.Ecs;
using Photon.Pun;

namespace Chessy.Game
{
    public sealed class DestroyMS : IEcsRunSystem
    {
        private EcsFilter<BuildC, OwnerC> _buildF = default;
        private EcsFilter<EnvC> _envF = default;

        private EcsFilter<UnitC, OwnerC> _unitF = default;
        private EcsFilter<StepC> _statUnitF = default;


        public void Run()
        {
            var sender = InfoC.Sender(MGOTypes.Master);
            IdxDoingMC.Get(out var idx_0);

            ref var curUnitDataCom = ref _unitF.Get1(idx_0);
            ref var curOwnUnitCom = ref _unitF.Get2(idx_0);
            ref var buildC_0 = ref _buildF.Get1(idx_0);
            ref var ownBuildC_0 = ref _buildF.Get2(idx_0);
            ref var curEnvDataCom = ref _envF.Get1(idx_0);


            if (_statUnitF.Get1(idx_0).HaveMinSteps)
            {
                RpcSys.SoundToGeneral(RpcTarget.All, ClipTypes.Destroy);

                if (buildC_0.Is(BuildTypes.City))
                {
                    PlyerWinnerC.PlayerWinner = curOwnUnitCom.Owner;
                }
                _statUnitF.Get1(idx_0).TakeSteps();

                if (buildC_0.Is(BuildTypes.Farm))
                {
                    curEnvDataCom.Remove(EnvTypes.Fertilizer);
                    WhereEnvC.Remove(EnvTypes.Fertilizer, idx_0);
                }

                WhereBuildsC.Remove(ownBuildC_0.Owner, buildC_0.Build, idx_0);
                buildC_0.Remove();
            }
            else
            {
                RpcSys.SimpleMistakeToGeneral(MistakeTypes.NeedMoreSteps, sender);
            }
        }
    }
}
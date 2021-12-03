using Leopotam.Ecs;
using Photon.Pun;

namespace Game.Game
{
    public sealed class DestroyMS : IEcsRunSystem
    {
        private EcsFilter<EnvC> _envF = default;

        private EcsFilter<UnitC, OwnerC> _unitF = default;
        private EcsFilter<StepC> _statUnitF = default;


        public void Run()
        {
            var sender = InfoC.Sender(MGOTypes.Master);
            IdxDoingMC.Get(out var idx_0);

            ref var unit_0 = ref _unitF.Get1(idx_0);
            ref var ownUnit_0 = ref _unitF.Get2(idx_0);
            ref var buildC_0 = ref EntityPool.Build<BuildC>(idx_0);
            ref var ownBuildC_0 = ref EntityPool.Build<OwnerC>(idx_0);
            ref var env_0 = ref _envF.Get1(idx_0);


            if (_statUnitF.Get1(idx_0).HaveMin)
            {
                RpcSys.SoundToGeneral(RpcTarget.All, ClipTypes.Destroy);

                if (buildC_0.Is(BuildTypes.City))
                {
                    PlyerWinnerC.PlayerWinner = ownUnit_0.Owner;
                }
                _statUnitF.Get1(idx_0).Take();

                if (buildC_0.Is(BuildTypes.Farm))
                {
                    env_0.Remove(EnvTypes.Fertilizer);
                }

                buildC_0.Remove();
            }
            else
            {
                RpcSys.SimpleMistakeToGeneral(MistakeTypes.NeedMoreSteps, sender);
            }
        }
    }
}
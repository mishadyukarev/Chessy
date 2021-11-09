using Chessy.Common;
using Leopotam.Ecs;

namespace Chessy.Game
{
    public sealed class GrowAdultForestMS : IEcsRunSystem
    {
        private EcsFilter<CellUnitDataC, StepComponent> _unitStatFilt = default;
        private EcsFilter<CellEnvDataC, CellEnvResC> _envFilt = default;

        public void Run()
        {
            ForGrowAdultForestMC.Get(out var idx_0);

            var sender = InfoC.Sender(MGOTypes.Master);

            ref var unitStep_0 = ref _unitStatFilt.Get2(idx_0);
            ref var env_0 = ref _envFilt.Get1(idx_0);
            ref var envRes_0 = ref _envFilt.Get2(idx_0);

            if (unitStep_0.HaveMinSteps)
            {
                if (env_0.Have(EnvTypes.YoungForest))
                {
                    WhereEnvC.Remove(EnvTypes.YoungForest, idx_0);
                    env_0.Reset(EnvTypes.YoungForest);

                    env_0.Set(EnvTypes.AdultForest);
                    envRes_0.SetNew(EnvTypes.AdultForest);
                    WhereEnvC.Add(EnvTypes.AdultForest, idx_0);

                    unitStep_0.TakeSteps();

                    RpcSys.SoundToGeneral(sender, ClipGameTypes.Seeding);
                }

                else RpcSys.SimpleMistakeToGeneral(MistakeTypes.NeedOtherPlace, sender);
            }
            else RpcSys.SimpleMistakeToGeneral(MistakeTypes.NeedMoreSteps, sender);
        }
    }
}
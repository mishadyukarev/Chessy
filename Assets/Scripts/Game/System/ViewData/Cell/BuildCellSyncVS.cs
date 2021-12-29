using Leopotam.Ecs;

namespace Game.Game
{
    public sealed class BuildCellSyncVS : IEcsRunSystem
    {
        public void Run()
        {
            foreach (byte idx_0 in EntityCellPool.Idxs)
            {
                ref var build_0 = ref EntityCellPool.Build<BuildC>(idx_0);
                ref var ownBuild_0 = ref EntityCellPool.Build<OwnerC>(idx_0);
                ref var visBuild_0 = ref EntityCellPool.Build<VisibleC>(idx_0);

                ref var buildV_0 = ref EntityCellVPool.BuildCellVC<BuildVC>(idx_0);
                ref var buildBackV_0 = ref EntityCellVPool.BuildCellVC<BuildBackVC>(idx_0);


                var build = build_0.Build;
                var isVisForMe = visBuild_0.IsVisibled(WhoseMoveC.CurPlayerI);
                var isVisForNext = visBuild_0.IsVisibled(WhoseMoveC.NextPlayerFrom(WhoseMoveC.CurPlayerI));

                buildV_0.Set(build, isVisForMe, isVisForNext);
                buildBackV_0.Set(build_0.Build, ownBuild_0.Owner, isVisForMe, isVisForNext);
            }
        }
    }
}

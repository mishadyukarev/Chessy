using static Game.Game.CellE;
using static Game.Game.CellBuildE;

namespace Game.Game
{
    struct BuildCellSyncVS : IEcsRunSystem
    {
        public void Run()
        {
            foreach (byte idx_0 in Idxs)
            {
                ref var build_0 = ref Build<BuildingC>(idx_0);
                ref var ownBuild_0 = ref Build<PlayerTC>(idx_0);



                ref var buildV_0 = ref CellVEs.BuildCellVC<BuildVC>(idx_0);
                ref var buildBackV_0 = ref CellVEs.BuildCellVC<BuildBackVC>(idx_0);


                var build = build_0.Build;
                var isVisForMe = Build<IsVisibledC>(WhoseMoveE.CurPlayerI, idx_0).IsVisibled;
                var isVisForNext = Build<IsVisibledC>(WhoseMoveE.NextPlayerFrom(WhoseMoveE.CurPlayerI), idx_0).IsVisibled;

                buildV_0.Set(build, isVisForMe, isVisForNext);
                buildBackV_0.Set(build_0.Build, ownBuild_0.Player, isVisForMe, isVisForNext);
            }
        }
    }
}

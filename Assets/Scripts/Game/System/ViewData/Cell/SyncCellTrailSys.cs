using Leopotam.Ecs;

namespace Game.Game
{
    public sealed class SyncCellTrailSys : IEcsRunSystem
    {
        private EcsFilter<TrailVC> _trailVF = default;

        public void Run()
        {
            foreach (byte idx_0 in EntityPool.Idxs)
            {
                ref var trailData_0 = ref EntityPool.GetTrailCellC<TrailC>(idx_0);
                ref var trailVisData_0 = ref EntityPool.GetTrailCellC<VisibleC>(idx_0);
                ref var trailView_0 = ref _trailVF.Get1(idx_0);

                foreach (var item in trailData_0.DictTrail)
                {
                    if (trailVisData_0.IsVisibled(WhoseMoveC.CurPlayerI))
                    {
                        trailView_0.SetActive(item.Key, trailData_0.Have(item.Key));
                    }
                    else trailView_0.SetActive(item.Key, false);
                }
            }
        }
    }
}
using Leopotam.Ecs;

namespace Scripts.Game
{
    public sealed class SyncCellTrailSys : IEcsRunSystem
    {
        private EcsFilter<CellTrailDataC, VisibleC, CellTrailViewC> _cellTrailFilt = default;

        public void Run()
        {
            foreach (var idx_0 in _cellTrailFilt)
            {
                ref var trailData_0 = ref _cellTrailFilt.Get1(idx_0);
                ref var trailVisData_0 = ref _cellTrailFilt.Get2(idx_0);
                ref var trailView_0 = ref _cellTrailFilt.Get3(idx_0);

                foreach (var item in trailData_0.DictTrail)
                {
                    if (trailVisData_0.IsVisibled(WhoseMoveC.CurPlayer))
                    {
                        trailView_0.SetActive(item.Key, trailData_0.Have(item.Key));
                    }
                    else trailView_0.SetActive(item.Key, false);
                }
            }
        }
    }
}
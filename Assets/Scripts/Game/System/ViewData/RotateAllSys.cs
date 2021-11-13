using Leopotam.Ecs;
using Chessy.Common;

namespace Chessy.Game
{
    public sealed class RotateAllSys : IEcsRunSystem
    {
        private EcsFilter<CellVC> _cellViewFilter = default;
        private EcsFilter<RiverVC> _cellRiverFilt = default;
        private EcsFilter<TrailVC> _cellTrailFilt = default;

        public void Run()
        {
            var curPlayer = WhoseMoveC.CurPlayerI;

            foreach (byte idx_0 in _cellViewFilter)
            {
                _cellViewFilter.Get1(idx_0).SetRotForClient(curPlayer);
                _cellRiverFilt.Get1(idx_0).Rotate(curPlayer);
                _cellTrailFilt.Get1(idx_0).Rotate(curPlayer);
            }

            CameraVC.SetPosRotClient(curPlayer, MainGoVC.Main_GO.transform.position);
        }
    }
}
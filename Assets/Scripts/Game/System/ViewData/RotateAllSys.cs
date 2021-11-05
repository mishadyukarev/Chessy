using Leopotam.Ecs;
using Scripts.Common;

namespace Scripts.Game
{
    public sealed class RotateAllSys : IEcsRunSystem
    {
        private EcsFilter<CellViewC> _cellViewFilter = default;
        private EcsFilter<CellRiverViewC> _cellRiverFilt = default;
        private EcsFilter<CellTrailViewC> _cellTrailFilt = default;

        public void Run()
        {
            var curPlayer = WhoseMoveC.CurPlayerI;

            foreach (byte idx_0 in _cellViewFilter)
            {
                _cellViewFilter.Get1(idx_0).SetRotForClient(curPlayer);
                _cellRiverFilt.Get1(idx_0).Rotate(curPlayer);
                _cellTrailFilt.Get1(idx_0).Rotate(curPlayer);
            }

            CameraC.SetPosRotClient(curPlayer, MainGoVC.Main_GO.transform.position);
        }
    }
}
using Leopotam.Ecs;
using Game.Common;

namespace Game.Game
{
    public sealed class RotateAllSys : IEcsRunSystem
    {
        private EcsFilter<CellVC> _cellVF = default;
        private EcsFilter<RiverVC> _riverF = default;
        private EcsFilter<TrailVC> _trailF = default;

        public void Run()
        {
            var curPlayer = WhoseMoveC.CurPlayerI;

            foreach (byte idx_0 in _cellVF)
            {
                _cellVF.Get1(idx_0).SetRotForClient(curPlayer);
                _riverF.Get1(idx_0).Rotate(curPlayer);
                _trailF.Get1(idx_0).Rotate(curPlayer);
            }

            CameraVC.SetPosRotClient(curPlayer, MainGoVC.Pos);
        }
    }
}
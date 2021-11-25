using Leopotam.Ecs;
using Game.Common;

namespace Game.Game
{
    public sealed class RotateAllSys : IEcsRunSystem
    {
        private EcsFilter<RiverVC> _riverF = default;
        private EcsFilter<TrailVC> _trailF = default;

        public void Run()
        {
            var curPlayer = WhoseMoveC.CurPlayerI;

            for (byte idx_0 = 0; idx_0 < EntityDataPool.AmountAllCells; idx_0++)
            {
                EntityViewPool.GetCellVC<CellVC>(idx_0).SetRotForClient(curPlayer);
                _riverF.Get1(idx_0).Rotate(curPlayer);
                _trailF.Get1(idx_0).Rotate(curPlayer);
            }

            CameraVC.SetPosRotClient(curPlayer, MainGoVC.Pos);
        }
    }
}
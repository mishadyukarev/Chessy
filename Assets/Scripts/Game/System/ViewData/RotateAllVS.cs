using Leopotam.Ecs;
using Game.Common;

namespace Game.Game
{
    public sealed class RotateAllVS : IEcsRunSystem
    {
        public void Run()
        {
            var curPlayer = WhoseMoveC.CurPlayerI;

            foreach (byte idx_0 in EntityPool.Idxs)
            {
                EntityVPool.CellVC<CellVC>(idx_0).SetRotForClient(curPlayer);
                EntityVPool.TrailCellVC<TrailVC>(idx_0).Rotate(curPlayer);
            }

            CameraVC.SetPosRotClient(curPlayer, MainGoVC.Pos);
        }
    }
}
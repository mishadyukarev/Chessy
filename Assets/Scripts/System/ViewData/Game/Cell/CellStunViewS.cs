using static Game.Game.EntityCellPool;
using static Game.Game.EntityCellVPool;

namespace Game.Game
{
    public sealed class CellStunViewS : IEcsRunSystem
    {
        public void Run()
        {
            foreach (byte idx_0 in Idxs)
            {
                ref var stunView_0 = ref ElseCellVE<StunVC>(idx_0);
                ref var stun_0 = ref Unit<StunC>(idx_0);
                ref var visUnit_0 = ref Unit<VisibleC>(idx_0);

                if (visUnit_0.IsVisibled(WhoseMoveC.CurPlayerI))
                {
                    stunView_0.SetEnabled(stun_0.IsStunned);
                }
                else
                {
                    stunView_0.SetEnabled(false);
                }
            }
        }
    }
}
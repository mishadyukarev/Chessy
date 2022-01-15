using static Game.Game.CellUnitE;
using static Game.Game.CellE;
using static Game.Game.CellVEs;

namespace Game.Game
{
    struct CellStunVS : IEcsRunSystem
    {
        public void Run()
        {
            foreach (byte idx_0 in Idxs)
            {
                ref var stunView_0 = ref ElseCellVE<StunVC>(idx_0);
                ref var stun_0 = ref Unit<NeedStepsForExitStunC>(idx_0);

                if (Unit<IsVisibledC>(WhoseMoveE.CurPlayerI, idx_0).IsVisibled)
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
using static Game.Game.CellUnitEs;
using static Game.Game.CellEs;
using static Game.Game.StunCellVEs;

namespace Game.Game
{
    struct CellStunVS : IEcsRunSystem
    {
        public void Run()
        {
            foreach (byte idx_0 in Idxs)
            {
                ref var stun_0 = ref Unit<NeedStepsForExitStunC>(idx_0);

                if (Unit<IsVisibledC>(WhoseMoveE.CurPlayerI, idx_0).IsVisibled)
                {
                    Stun<SpriteRendererVC>(idx_0).SetActive(stun_0.IsStunned);
                }
                else
                {
                    Stun<SpriteRendererVC>(idx_0).Disable();
                }
            }
        }
    }
}
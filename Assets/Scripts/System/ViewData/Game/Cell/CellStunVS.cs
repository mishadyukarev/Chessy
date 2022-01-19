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
                ref var stun_0 = ref CellUnitStunEs.StepsForExitStun(idx_0);

                if (CellUnitVisibleEs.Visible<IsVisibleC>(WhoseMoveE.CurPlayerI, idx_0).IsVisible)
                {
                    Stun<SpriteRendererVC>(idx_0).SetActive(stun_0.Have);
                }
                else
                {
                    Stun<SpriteRendererVC>(idx_0).Disable();
                }
            }
        }
    }
}
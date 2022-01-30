using static Game.Game.StunCellVEs;

namespace Game.Game
{
    sealed class CellStunVS : SystemViewAbstract, IEcsRunSystem
    {
        public CellStunVS(in Entities ents, in EntitiesView entsView) : base(ents, entsView)
        {
        }

        public void Run()
        {
            foreach (byte idx_0 in Es.CellEs.Idxs)
            {
                ref var stun_0 = ref Es.CellEs.UnitEs.Stun(idx_0).ForExitStun;

                if (Es.CellEs.UnitEs.VisibleE(Es.WhoseMove.CurPlayerI, idx_0).VisibleC.IsVisible)
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
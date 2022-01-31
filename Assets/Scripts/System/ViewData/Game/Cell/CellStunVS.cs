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
            foreach (byte idx_0 in CellEs.Idxs)
            {
                if (UnitEs.VisibleE(Es.WhoseMove.CurPlayerI, idx_0).IsVisibleC.IsVisible)
                {
                    Stun<SpriteRendererVC>(idx_0).SetActive(UnitEs.Stun(idx_0).IsStunned);
                }
                else
                {
                    Stun<SpriteRendererVC>(idx_0).Disable();
                }
            }
        }
    }
}
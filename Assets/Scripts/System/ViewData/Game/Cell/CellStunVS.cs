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
            foreach (byte idx_0 in CellEsWorker.Idxs)
            {
                if (UnitEs(idx_0).VisibleE(Es.WhoseMove.CurPlayerI).IsVisibleC.IsVisible)
                {
                    Stun<SpriteRendererVC>(idx_0).SetActive(UnitEffectEs(idx_0).StunE.IsStunned);
                }
                else
                {
                    Stun<SpriteRendererVC>(idx_0).Disable();
                }
            }
        }
    }
}
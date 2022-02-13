using static Game.Game.CellUnitEffectStunVE;

namespace Game.Game
{
    sealed class CellUnitEffectStunVS : SystemViewAbstract, IEcsRunSystem
    {
        internal CellUnitEffectStunVS(in Entities ents, in EntitiesView entsView) : base(ents, entsView)
        {
        }

        public void Run()
        {
            for (byte idx_0 = 0; idx_0 < Es.LengthCells; idx_0++)
            {
                if (Es.UnitEs(idx_0).VisibleE(Es.WhoseMoveE.CurPlayerI).IsVisibleC.IsVisible)
                {
                    VEs.UnitEffectVEs(idx_0).StunVE.Stun.SetActive(Es.UnitE(idx_0).IsStunned);
                }
                else
                {
                    VEs.UnitEffectVEs(idx_0).StunVE.Stun.Disable();
                }
            }
        }
    }
}
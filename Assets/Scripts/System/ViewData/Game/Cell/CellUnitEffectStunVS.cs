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
                if (UnitEs(idx_0).VisibleE(Es.WhoseMove.CurPlayerI).IsVisibleC.IsVisible)
                {
                    UnitEffectVEs(idx_0).StunVE.Stun.SetActive(UnitEffectEs(idx_0).StunE.IsStunned);
                }
                else
                {
                    UnitEffectVEs(idx_0).StunVE.Stun.Disable();
                }
            }
        }
    }
}
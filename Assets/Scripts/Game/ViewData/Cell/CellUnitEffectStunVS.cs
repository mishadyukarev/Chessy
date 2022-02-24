namespace Game.Game
{
    sealed class CellUnitEffectStunVS : SystemViewAbstract, IEcsRunSystem
    {
        internal CellUnitEffectStunVS(in EntitiesModel ents, in EntitiesView entsView) : base(ents, entsView)
        {
        }

        public void Run()
        {
            for (byte idx_0 = 0; idx_0 < E.LengthCells; idx_0++)
            {
                if (E.UnitEs(idx_0).ForPlayer(E.CurPlayerITC.Player).IsVisible)
                {
                    VEs.UnitEffectVEs(idx_0).StunVE.Stun.SetActive(E.UnitEffectStunC(idx_0).IsStunned);
                }
                else
                {
                    VEs.UnitEffectVEs(idx_0).StunVE.Stun.Disable();
                }
            }
        }
    }
}
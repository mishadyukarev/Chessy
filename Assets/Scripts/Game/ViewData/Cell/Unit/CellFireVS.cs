namespace Game.Game
{
    sealed class CellFireVS : SystemViewAbstract, IEcsRunSystem
    {
        internal CellFireVS(in EntitiesModel ents, in EntitiesView entsView) : base(ents, entsView)
        {
        }

        public void Run()
        {
            for (byte idx_0 = 0; idx_0 < E.LengthCells; idx_0++)
            {
                if (E.EffectEs(idx_0).HaveFire)
                {
                    CellVEs(idx_0).FireVE.SR.Enable();
                }

                else
                {
                    CellVEs(idx_0).FireVE.SR.Disable();
                }
            }
        }
    }
}
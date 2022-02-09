namespace Game.Game
{
    sealed class CellFireVS : SystemViewAbstract, IEcsRunSystem
    {
        internal CellFireVS(in Entities ents, in EntitiesView entsView) : base(ents, entsView)
        {
        }

        public void Run()
        {
            for (byte idx_0 = 0; idx_0 < Es.LengthCells; idx_0++)
            {
                if (EffectEs(idx_0).FireE.HaveFireC.Have)
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
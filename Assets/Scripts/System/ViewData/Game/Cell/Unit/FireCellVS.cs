namespace Game.Game
{
    sealed class FireCellVS : SystemViewAbstract, IEcsRunSystem
    {
        public FireCellVS(in Entities ents, in EntitiesView entsView) : base(ents, entsView)
        {
        }

        public void Run()
        {
            foreach (var idx in CellWorker.Idxs)
            {
                if (EffectEs(idx).FireE.HaveFireC.Have)
                {
                    CellVEs(idx).FireVE.SR.Enable();
                }

                else
                {
                    CellVEs(idx).FireVE.SR.Disable();
                }
            }
        }
    }
}
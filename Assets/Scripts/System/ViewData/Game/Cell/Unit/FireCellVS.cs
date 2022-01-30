namespace Game.Game
{
    sealed class FireCellVS : SystemViewAbstract, IEcsRunSystem
    {
        public FireCellVS(in Entities ents, in EntitiesView entsView) : base(ents, entsView)
        {
        }

        public void Run()
        {
            foreach (var idx in Es.CellEs.Idxs)
            {
                if (Es.CellEs.FireEs.Fire(idx).Fire.Have)
                {
                    CellFireVEs.FireCellVC<SpriteRendererVC>(idx).Enable();
                }

                else
                {
                    CellFireVEs.FireCellVC<SpriteRendererVC>(idx).Disable();
                }
            }
        }
    }
}
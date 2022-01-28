namespace Game.Game
{
    struct FireCellVS : IEcsRunSystem
    {
        public void Run()
        {
            foreach (var idx in Entities.CellEs.Idxs)
            {
                if (Entities.CellEs.FireEs.Fire(idx).Fire.Have)
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
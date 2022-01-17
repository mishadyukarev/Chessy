using static Game.Game.CellFireEs;

namespace Game.Game
{
    struct FireCellVS : IEcsRunSystem
    {
        public void Run()
        {
            foreach (var idx in CellEs.Idxs)
            {
                if (Fire<HaveEffectC>(idx).Have)
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
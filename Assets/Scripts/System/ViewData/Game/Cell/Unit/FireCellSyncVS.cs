using static Game.Game.EntityCellFirePool;

namespace Game.Game
{
    struct FireCellSyncVS : IEcsRunSystem
    {
        public void Run()
        {
            foreach (var idx in EntityCellPool.Idxs)
            {
                if (Fire<HaveEffectC>(idx).Have)
                {
                    EntityCellVPool.FireCellVC<FireVC>(idx).EnableSR();
                }

                else
                {
                    EntityCellVPool.FireCellVC<FireVC>(idx).DisableSR();
                }
            }
        }
    }
}
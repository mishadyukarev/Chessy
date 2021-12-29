using Leopotam.Ecs;

namespace Game.Game
{
    public sealed class FireCellSyncVS : IEcsRunSystem
    {
        public void Run()
        {
            foreach (var idx in EntityCellPool.Idxs)
            {
                if (EntityCellPool.Fire<HaveEffectC>(idx).Have)
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
using Leopotam.Ecs;

namespace Game.Game
{
    public sealed class FireCellSyncVS : IEcsRunSystem
    {
        public void Run()
        {
            foreach (var idx in EntityPool.Idxs)
            {
                if (EntityPool.Fire<FireC>(idx).Have)
                {
                    EntityVPool.FireCellVC<FireVC>(idx).EnableSR();
                }

                else
                {
                    EntityVPool.FireCellVC<FireVC>(idx).DisableSR();
                }
            }
        }
    }
}
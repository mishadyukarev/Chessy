using static Game.Game.EntityCellCloudPool;

namespace Game.Game
{
    struct CloudCellVS : IEcsRunSystem
    {
        public void Run()
        {
            foreach (var idx_0 in CellE.Idxs)
            {
                CellCloudVEs.CloudCellVC<SpriteRendererVC>(idx_0).SetActive(Cloud<HaveEffectC>(idx_0).Have);
            }
        }
    }
}
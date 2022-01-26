namespace Game.Game
{
    struct CloudCellVS : IEcsRunSystem
    {
        public void Run()
        {
            foreach (var idx_0 in CellEs.Idxs)
            {
                CellCloudVEs.CloudCellVC<SpriteRendererVC>(idx_0).SetActive(false);
            }

            var centerCloud = Entities.WindE.CenterCloud.Idx;

            foreach (var idx in CellSpaceSupport.GetIdxsAround(centerCloud))
            {
                CellCloudVEs.CloudCellVC<SpriteRendererVC>(idx).SetActive(true);
            }

            CellCloudVEs.CloudCellVC<SpriteRendererVC>(centerCloud).SetActive(true);
        }
    }
}
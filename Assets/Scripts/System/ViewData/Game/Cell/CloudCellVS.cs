namespace Game.Game
{
    sealed class CloudCellVS : SystemViewAbstract, IEcsRunSystem
    {
        public CloudCellVS(in Entities ents, in EntitiesView entsView) : base(ents, entsView)
        {
        }

        public void Run()
        {
            foreach (var idx_0 in CellEs.Idxs)
            {
                CellCloudVEs.CloudCellVC<SpriteRendererVC>(idx_0).SetActive(false);
            }

            var centerCloud = Es.WindE.CenterCloud.Idx;

            foreach (var idx in CellEs.GetIdxsAround(centerCloud))
            {
                CellCloudVEs.CloudCellVC<SpriteRendererVC>(idx).SetActive(true);
            }

            CellCloudVEs.CloudCellVC<SpriteRendererVC>(centerCloud).SetActive(true);
        }
    }
}